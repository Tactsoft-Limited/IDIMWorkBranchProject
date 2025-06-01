using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class BGBFundService : BaseService<BGBFund>, IBGBFundService
    {
        public BGBFundService(IDIMDBEntities context) : base(context)
        {
        }
        public async Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0)
        {
            var query = from BF in _context.BGBFunds
                        join p in _context.ProjectWorks on BF.ProjectWorkId equals p.ProjectWorkId                        
                        select new SelectListItem
                        {
                            Text = p.ProjectWorkTitleB + " - " + BF.AmountDeposited,
                            Value = BF.BGBFundId.ToString(),
                            Selected = BF.BGBFundId == selected
                        };

            return await query.ToListAsync();
        }
        public async Task<BGBFund> GetByFinalBillPaymentIdAsync(int id)
        {
            return await _context.BGBFunds.Where(x => x.ProjectWorkId == id).FirstOrDefaultAsync();
        }

        public async Task<List<BGBFund>> GetByProjectWorkIdAsync(int projectWorkId)
        {
            return await _context.BGBFunds.Where(x => x.ProjectWorkId == projectWorkId).ToListAsync();
        }


    }
}