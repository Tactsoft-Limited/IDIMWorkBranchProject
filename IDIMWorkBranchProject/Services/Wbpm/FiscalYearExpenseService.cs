using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using IDIMWorkBranchProject.Models.Wbpm;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class FiscalYearExpenseService : BaseService<FiscalYearExpense>, IFiscalYearExpenseService
    {
        public FiscalYearExpenseService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<List<FiscalYearExpense>> GetAllByProjectId(int id)
        {
            return await _context.FiscalYearExpenses.Where(x => x.ADPProjectId == id).ToListAsync();
        }
        public async Task<object> GetPagedAsync(FiscalYearExpenseSearchVm model)
        {
            var query = _context.FiscalYearExpenses.Where(x =>
                string.IsNullOrEmpty(model.SearchValue) ||
                x.ADPProject.ProjectTitle.Contains(model.SearchValue));

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
                data = pagedData.Select(x => new FiscalYearExpenseVm
                {
                    ProjectTitle=x.ADPProject.ProjectTitle,
                    FiscalYearId=x.FiscalYearId,
                    FiscalYearDescription=x.FiscalYear.FiscalYearDescription,
                    TotalExpense=x.TotalExpense
                    
                })
            };

            return result;
        }
    }
}