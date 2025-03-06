using BGB.Data.Entities.Wbpm;

using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class ProjectWorkService : BaseService<ProjectWork>, IProjectWorkService
    {
        public ProjectWorkService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<List<ProjectWork>> GetAllByProjectId(int id)
        {
            return await _context.ProjectWorks.Where(x => x.ADPProjectId == id).ToListAsync();
        }

        public async Task<object> GetPagedAsync(ProjectWorkSearchVm model)
        {
            var query = _context.ProjectWorks.Where(x =>
            (string.IsNullOrEmpty(model.SearchValue) || x.ProjectWorkTitle.Contains(model.SearchValue)));

            query = !string.IsNullOrEmpty(model.SortColumn) && !string.IsNullOrEmpty(model.SortDirection)
            ? query.OrderBy($"{model.SortColumn} {model.SortDirection}")
                : query.OrderBy(x => x.ProjectWorkId);  // Default ordering by SubProjectId

            var totalRecords = await query.CountAsync();
            var filteredRecords = await query.CountAsync();
            var pagedData = await query.Skip(model.PageIndex * model.PageSize).Take(model.PageSize).ToListAsync();

            // Return the response in DataTables format
            var result = new
            {
                draw = model.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords,
                data = pagedData.Select(x => new ProjectWorkVm
                {
                    ADPProjectId = x.ADPProjectId,
                    ProjectTitle = x.ADPProject.ProjectTitle,
                    ProjectWorkTitleB = x.ProjectWorkTitleB,
                    ProjectWorkTitle = x.ProjectWorkTitle,
                    EstimatedCost = x.EstimatedCost,
                    AgreementDate = x.AgreementDate,
                    WorkStartDate = x.WorkEndDate,
                    BankGuaranteeEndDate = x.BankGuaranteeEndDate,
                    HandOverDate = x.HandOverDate,
                    WorkStatus = x.WorkStatus,
                    Remarks = x.Remarks,
                })
            };

            return result;
        }

        public async Task<string> GetProjectWorkTitle(int? ProjectWorkId)
        {
            return await _context.ProjectWorks.Where(x => x.ProjectWorkId == ProjectWorkId).Select(x => x.ProjectWorkTitle).FirstOrDefaultAsync();
        }
    }
}