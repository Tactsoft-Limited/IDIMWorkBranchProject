using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IFinalBillPaymentService:IBaseService<FinalBillPayment>
    {
        Task<List<FinalBillPayment>> GetAllByProjectWorkIdAsync(int id);
        Task<FinalBillPayment> GetByProjectWorkIdAsync(int id);
    }
}
