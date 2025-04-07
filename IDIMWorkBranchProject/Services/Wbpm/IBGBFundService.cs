using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IBGBFundService :IBaseService<BGBFund>
    {
        Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0);
        Task<List<BGBFund>> GetByProjectWorkIdAsync(int projectWorkId);
        Task<BGBFund> GetByFinalBillPaymentIdAsync(int id);
    }
}
