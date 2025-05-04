using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class RevenueNohaService : BaseService<RevenueNoha>, IRevenueNohaService
    {
        public RevenueNohaService(IDIMDBEntities context) : base(context)
        {
        }
        public async Task<RevenueNoha> GetByProjectWorkIdAsync(int id)
        {
            return await _context.RevenueNohas.Where(x => x.ProjectWorkId == id).FirstOrDefaultAsync();
        }
    }
}