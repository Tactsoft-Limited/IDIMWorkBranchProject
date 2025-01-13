using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class ContractorCompanyPaymentService : BaseService<ContractorCompanyPayment>, IContractorCompanyPaymentService
    {
        public ContractorCompanyPaymentService(IDIMDBEntities context) : base(context)
        {
        }


    }
}