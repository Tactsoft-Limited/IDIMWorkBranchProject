using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

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
    }
}