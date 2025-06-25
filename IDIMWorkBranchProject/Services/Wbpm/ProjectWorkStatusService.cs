using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class ProjectWorkStatusService : BaseService<ProjectWorkStatus>, IProjectWorkStatusService
    {
        public ProjectWorkStatusService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<ProjectWorkStatus> GetByProjectWorkIdAsync(int id)
        {
            return await _context.ProjectWorkStatuses.Where(x => x.ProjectWorkId == id).FirstOrDefaultAsync();
        }

        public async Task<object> GetPagedAsync(ProjectWorkStatusSearchVm model)
        {
            var query = _context.ProjectWorkStatuses.Where(x =>
            (string.IsNullOrEmpty(model.SearchValue) || x.ProjectWork.ProjectWorkTitleB.Contains(model.SearchValue)));

            query = !string.IsNullOrEmpty(model.SortColumn) && !string.IsNullOrEmpty(model.SortDirection)
            ? query.OrderBy($"{model.SortColumn} {model.SortDirection}")
                : query.OrderBy(x => x.ProjectWorkStatusId);  // Default ordering by SubProjectId

            var totalRecords = await query.CountAsync();
            var filteredRecords = await query.CountAsync();
            var pagedData = await query.Skip(model.PageIndex * model.PageSize).Take(model.PageSize).ToListAsync();

            // Return the response in DataTables format
            var result = new
            {
                draw = model.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords,
                data = pagedData.Select(x => new ProjectWorkStatusVm
                {
                    ProjectWorkStatusId = x.ProjectWorkStatusId,
                    ProjectWorkId = x.ProjectWorkId,
                    ProjectWorkTitleB = x.ProjectWork.ProjectWorkTitleB,
                    StatusTypeId = (StatusType?)x.StatusTypeId,
                    WorkStatus = x.StatusTypeId.HasValue ? ((StatusType)x.StatusTypeId.Value).ToString() : null,
                })
            };

            return result;
        }
    }
}