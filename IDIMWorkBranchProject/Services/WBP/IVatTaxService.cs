using IDIMWorkBranchProject.Models.WBP;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.WBP
{
    public interface IVatTaxService
    {
        Task<List<VatTaxVm>> GetAllAsync();
        Task<VatTaxVm> GetByIdAsync(int id);
        Task<VatTaxVm> InsertAsync(VatTaxVm model);
        Task<VatTaxVm> UpdateAsync(VatTaxVm model);
        Task<VatTaxVm> DeleteAsync(int id);

        Task<object> GetByAsync(VatTaxSearchVm filter = null);
        Task<int> GetByProjectAndPaymentAsync(int projectId, int receivePaymentId);
    }
}
