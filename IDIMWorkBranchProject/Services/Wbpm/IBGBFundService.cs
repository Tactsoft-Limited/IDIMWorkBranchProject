using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
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
