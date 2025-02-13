using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class ContractAgreementService : BaseService<ContractAgreement>, IContractAgreementService
    {
        public ContractAgreementService(IDIMDBEntities context) : base(context)
        {
        }


    }
}