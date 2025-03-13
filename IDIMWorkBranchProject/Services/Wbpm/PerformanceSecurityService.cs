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
    public class PerformanceSecurityService : BaseService<PerformanceSecurity>, IPerformanceSecurityService
    {
        public PerformanceSecurityService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<PerformanceSecurity> GetByProjectWorkIdAsync(int id)
        {
            return await _context.PerformanceSecurities.Where(x => x.ProjectWorkId == id).FirstOrDefaultAsync();
        }

        public async Task<object> GetPagedAsync(PerformanceSecuritySearchVm model)
        {
            var query = _context.PerformanceSecurities.Where(x =>
                string.IsNullOrEmpty(model.SearchValue) ||
                x.ProjectWork.ProjectWorkTitle.Contains(model.SearchValue) ||
                x.SubmissionDate.ToString().Contains(model.SearchValue) ||
                x.ExpiryDate.ToString().Contains(model.SearchValue)
            );



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
                data = pagedData.Select(x => new PerformanceSecurityVm
                {
                    ProjectWorkTitleB = x.ProjectWork.ProjectWorkTitleB,
                    SubmissionDate = x.SubmissionDate,
                    ExpiryDate = x.ExpiryDate,
                    ScanDocument = x.ScanDocument
                })
            };

            return result;
        }

    }
}