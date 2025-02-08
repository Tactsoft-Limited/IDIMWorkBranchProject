using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class ADPReceivePaymentService : BaseService<ADPReceivePayment>, IADPReceivePaymentService
    {

        public ADPReceivePaymentService(IDIMDBEntities context) : base(context)
        {
        }


    }
}