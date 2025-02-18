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
    public class ADPProjectService : BaseService<ADPProject>, IADPProjectService
    {
        public ADPProjectService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<string> GetAdpProjectTitle(int? aDPProjectId)
        {
            return await _context.ADPProjects.Where(x => x.ADPProjectId == aDPProjectId).Select(x=>x.ProjectTitle).FirstOrDefaultAsync();
        }

        public async Task<object> GetPagedAsync(ADPProjectSearchVm model)
        {
            var query = _context.ADPProjects.Where(x =>
            (string.IsNullOrEmpty(model.SearchValue) ||
            x.ProjectTitle.Contains(model.SearchValue) || x.MinistryDepartment.Contains(model.SearchValue)) &&
            (!model.StartingDate.HasValue || x.StartingDate >= model.StartingDate.Value) &&
            (!model.EndingDate.HasValue || x.EndingDate <= model.EndingDate.Value));

            query = !string.IsNullOrEmpty(model.SortColumn) && !string.IsNullOrEmpty(model.SortDirection)
            ? query.OrderBy($"{model.SortColumn} {model.SortDirection}")
                : query.OrderBy(x => x.ADPProjectId);  // Default ordering by SubProjectId

            var totalRecords = await query.CountAsync();
            var filteredRecords = await query.CountAsync();
            var pagedData = await query.Skip(model.PageIndex * model.PageSize).Take(model.PageSize).ToListAsync();

            // Return the response in DataTables format
            var result = new
            {
                draw = model.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords,
                data = pagedData.Select(x => new ADPProjectVm
                {
                    ADPProjectId = x.ADPProjectId,
                    ProjectTitle = x.ProjectTitle,
                    MinistryDepartment = x.MinistryDepartment,
                    EstimatedExpenses = x.EstimatedExpenses,
                    StartingDate = x.StartingDate,
                    EndingDate = x.EndingDate,
                    NoOfWork = x.NoOfWork,
                    FinancialProgress = x.FinancialProgress,
                    PhysicalProgress = x.PhysicalProgress,
                    Remarks = x.Remarks,
                })
            };

            return result;
        }
    }
}