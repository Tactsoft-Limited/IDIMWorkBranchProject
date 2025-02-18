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
    public class RecruitmentCommitteeService : BaseService<RecruitmentCommittee>, IRecruitmentCommitteeService
    {
        public RecruitmentCommitteeService(IDIMDBEntities context) : base(context)
        {
        }
        public async Task<IEnumerable<SelectListItem>>GetDropdownAsync(int? selected = 0)
        {
            return await _context.RecruitmentCommittees.Select(d => new SelectListItem
            {   
                Text = $"{d.DesignationB}  {d.NameB}",
                Value = d.RecruitmentCommitteeId.ToString(),
                Selected = d.RecruitmentCommitteeId == selected
            }).ToListAsync();
        }

        public async Task<object> GetPagedAsync(RecruitmentCommitteeSearchVm model)
        {
            var query = _context.RecruitmentCommittees.Where(x =>
            (string.IsNullOrEmpty(model.SearchValue) ||
            x.Name.Contains(model.SearchValue) || x.NameB.Contains(model.SearchValue) ||
            x.Title.Contains(model.SearchValue) || x.TitleB.Contains(model.SearchValue) ||
            x.Designation.Contains(model.SearchValue) || x.DesignationB.Contains(model.SearchValue) ||
            x.Address.Contains(model.SearchValue) || x.AddressB.Contains(model.SearchValue)));

            query = !string.IsNullOrEmpty(model.SortColumn) && !string.IsNullOrEmpty(model.SortDirection)
                ? query.OrderBy($"{model.SortColumn} {model.SortDirection}")
                : query.OrderBy(x => x.RecruitmentCommitteeId);
            // Default ordering by SubProjectId

            var totalRecords = await query.CountAsync();
            var filteredRecords = await query.CountAsync();
            var pagedData = await query.Skip(model.PageIndex * model.PageSize).Take(model.PageSize).ToListAsync();

            // Return the response in DataTables format
            var result = new
            {
                draw = model.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords,
                data = pagedData.Select(x => new RecruitmentCommitteeVm
                {
                    RecruitmentCommitteeId = x.RecruitmentCommitteeId,
                    Name = x.Name,
                    NameB = x.NameB,
                    Title = x.Title,
                    TitleB = x.TitleB,
                    Designation = x.Designation,
                    DesignationB = x.DesignationB,
                    Address = x.Address,
                    AddressB = x.AddressB,
                })
            };

            return result;
        }
    }
}