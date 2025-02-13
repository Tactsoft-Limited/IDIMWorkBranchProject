using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IContractAgreementService : IBaseService<ContractAgreement>
    {
        Task<List<ContractAgreement>> GetByProjectWorkIdAsync(int id);
        Task<List<ContractAgreement>> GetByContructionFirmIdAsync(int id);
        Task<string> GetContactDayAsync(int? contracAgreementId);


    }
}
