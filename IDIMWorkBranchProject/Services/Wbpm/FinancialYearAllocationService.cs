using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;


namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class FinancialYearAllocationService : BaseService<FinancialYearAllocation>, IFinancialYearAllocationService
    {
        public FinancialYearAllocationService(IDIMDBEntities context) : base(context)
        {
        }


        public async Task<List<FinancialYearAllocation>> GetAllByProjectId(int id)
        {
            return await _context.FinancialYearAllocations.Include(x=>x.FiscalYear).Where(x => x.ADPProjectId == id).ToListAsync();

        }

        public async Task<object> GetPagedAsync(FinancialYearAllocationSearchVm model)
        {
            var query = _context.FinancialYearAllocations.Where(x =>
           (string.IsNullOrEmpty(model.SearchValue) ||
           x.ADPProject.ProjectTitle.Contains(model.SearchValue)));

            query = !string.IsNullOrEmpty(model.SortColumn) && !string.IsNullOrEmpty(model.SortDirection)
            ? query.OrderBy($"{model.SortColumn} {model.SortDirection}")
                : query.OrderBy(x => x.FinancialYearAllocationId);  // Default ordering by SubProjectId

            var totalRecords = await query.CountAsync();
            var filteredRecords = await query.CountAsync();
            var pagedData = await query.Skip(model.PageIndex * model.PageSize).Take(model.PageSize).ToListAsync();

            // Return the response in DataTables format
            var result = new
            {
                draw = model.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords,
                data = pagedData.Select(x => new FinancialYearAllocationVm
                {
                    ADPProjectId = x.ADPProjectId,
                    ProjectTitle = x.ADPProject.ProjectTitle,
                    FiscalYearId = x.FiscalYearId,
                    FiscalYearDescription = x.FiscalYear.FiscalYearDescription,
                    TotalAllocation = x.TotalAllocation,
                    ADPAllocation = x.ADPAllocation,
                    RADPAllocation = x.RADPAllocation,
                    
                })
            };

            return result;

        }
    }
}