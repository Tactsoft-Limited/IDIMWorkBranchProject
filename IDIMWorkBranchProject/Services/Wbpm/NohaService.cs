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
    public class NohaService : BaseService<Noha>, INohaService
    {
        public NohaService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<Noha> GetByProjectWorkIdAsync(int id)
        {
            return await _context.Nohas.Where(x => x.ProjectWorkId == id).FirstOrDefaultAsync();
        }
        public async Task<object> GetPagedAsync(NohaSearchVm model)
        {
            var query = _context.Nohas.Where(x =>
            (string.IsNullOrEmpty(model.SearchValue) ||
            x.ProjectWork.ProjectWorkTitle.Contains(model.SearchValue) || x.NohaDate.HasValue));

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
                data = pagedData.Select(x => new NohaVm
                {
                    ProjectWorkTitleB=x.ProjectWork.ProjectWorkTitleB,
                    NohaDate=x.NohaDate,
                    ScanDocument=x.ScanDocument
                })
            };

            return result;
        }
    }
}