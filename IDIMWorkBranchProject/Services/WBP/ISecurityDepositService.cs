using System.Collections.Generic;
using System.Threading.Tasks;
using IDIMWorkBranchProject.Models.WBP;


namespace IDIMWorkBranchProject.Services.WBP
{
    public interface ISecurityDepositService
    {
        Task<List<SecurityDepositVm>> GetAllAsync();
        Task<SecurityDepositVm> GetByIdAsync(int id);
        Task<SecurityDepositVm> InsertAsync(SecurityDepositVm model);
        Task<SecurityDepositVm> UpdateAsync(SecurityDepositVm model);
        Task<SecurityDepositVm> DeleteAsync(int id);

        Task<List<SecurityDepositVm>> GetByAsync(SecurityDepositSearchVm filter = null);
    }
}
