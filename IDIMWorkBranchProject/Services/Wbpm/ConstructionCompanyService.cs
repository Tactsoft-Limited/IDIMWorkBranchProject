using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class ConstructionCompanyService : BaseService<ConstructionCompany>, IConstructionCompanyService
    {
        public ConstructionCompanyService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected=0)
        {
            return await _context.ConstructionCompanies.Select(s => new SelectListItem
            {
                Text = s.FirmName,
                Value = s.ConstructionCompanyId.ToString(),
                Selected = s.ConstructionCompanyId == selected
            }).ToListAsync();
        }

        public async Task<object> GetPagedAsync(ConstructionCompanySearchVm model)
        {
            if (model == null)
                model = new ConstructionCompanySearchVm();

            var query = _context.Set<ConstructionCompany>().Where(x =>
            (string.IsNullOrEmpty(model.SearchValue) || x.FirmName.Contains(model.SearchValue) ||
            x.ContactPerson.Contains(model.SearchValue) || x.ContactPhone.Contains(model.SearchValue) ||
            x.Email.Contains(model.SearchValue)));

            query = !string.IsNullOrEmpty(model.SortColumn) && !string.IsNullOrEmpty(model.SortDirection)
            ? query.OrderBy($"{model.SortColumn} {model.SortDirection}")
                : query.OrderBy(x => x.ConstructionCompanyId);  // Default ordering by SubProjectId

            var totalRecords = await query.CountAsync();
            var filteredRecords = await query.CountAsync();
            var pagedData = await query.Skip(model.PageIndex * model.PageSize).Take(model.PageSize).ToListAsync();

            // Return the response in DataTables format
            var result = new
            {
                draw = model.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords,
                data = pagedData.Select(x => new ConstructionCompanyVm
                {
                    ConstructionCompanyId = x.ConstructionCompanyId,
                    FirmName = x.FirmName,
                    FirmNameB = x.FirmNameB,
                    ContactPerson = x.ContactPerson,
                    ContactPersonB = x.ContactPersonB,
                    ContactPhone = x.ContactPhone,
                    Email = x.Email,
                    FirmAddress = x.FirmAddress,
                    FirmAddressB = x.FirmAddressB
                })
            };

            return result;
        }
    }
}