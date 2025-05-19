using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IFinalBillPaymentService:IBaseService<FinalBillPayment>
    {
        Task<List<FinalBillPayment>> GetAllByProjectWorkIdAsync(int id);
        Task<FinalBillPayment> GetByProjectWorkIdAsync(int id);
    }
}
