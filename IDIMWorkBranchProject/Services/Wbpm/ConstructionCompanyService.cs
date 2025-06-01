using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System;
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

        public async Task<string> GetConstructionFirm(int constructionCompanyId)
        {
            return await _context.ConstructionCompanies.Where(x => x.ConstructionCompanyId == constructionCompanyId).Select(x => x.FirmNameB).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0)
        {
            var today = DateTime.Today;

            var companies = await _context.ConstructionCompanies.ToListAsync();

            return companies.Select(s =>
            {
                string label = s.FirmNameB;

                if (s.ExpiryDate.HasValue)
                {
                    var daysToExpire = (s.ExpiryDate.Value - today).Days;

                    if (daysToExpire < 0)
                    {
                        label = $"{label} (মেয়াদ শেষ)";
                    }
                    else if (daysToExpire <= 30)
                    {
                        label = $"{label} ({daysToExpire} দিনে মেয়াদ শেষ) ";
                    }
                }

                return new SelectListItem
                {
                    Text = label,
                    Value = s.ConstructionCompanyId.ToString(),
                    Selected = s.ConstructionCompanyId == selected
                };
            });
        }


        public async Task<(IList<ConstructionCompany> data, int total, int totalDisplay)> GetPagedAsync(ConstructionCompanySearchVm model)
        {
            if (model == null)
                model = new ConstructionCompanySearchVm();

            var query = _context.Set<ConstructionCompany>().Where(x =>
            (string.IsNullOrEmpty(model.SearchValue) || x.FirmName.Contains(model.SearchValue) ||
            x.OwnerName.Contains(model.SearchValue) || x.OwnerPhone.Contains(model.SearchValue) ||
            x.OwnerEmail.Contains(model.SearchValue) || x.AuthorizedPersonName.Contains(model.SearchValue) ||
            x.AuthorizedPersonNamePhone.Contains(model.SearchValue) || x.AuthorizedPersonNameDesignation.Contains(model.SearchValue)

            ));

            query = !string.IsNullOrEmpty(model.SortColumn) && !string.IsNullOrEmpty(model.SortDirection)
            ? query.OrderBy($"{model.SortColumn} {model.SortDirection}")
                : query.OrderBy(x => x.ConstructionCompanyId);  // Default ordering by SubProjectId

            var totalRecords = await query.CountAsync();
            var filteredRecords = await query.CountAsync();
            var pagedData = await query.Skip(model.PageIndex * model.PageSize).Take(model.PageSize).ToListAsync();

            return (pagedData, totalRecords, filteredRecords);
        }

        public bool IsDuplicateConstructionCompany(string name, int? id = null)
        {
            return GetCount(x => x.FirmName == name && (!id.HasValue || x.ConstructionCompanyId != id)) > 0;
        }
    }
}