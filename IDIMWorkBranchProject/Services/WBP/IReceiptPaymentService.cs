using System.Collections.Generic;
using System.Threading.Tasks;
using IDIMWorkBranchProject.Models.WBP;

namespace IDIMWorkBranchProject.Services.WBP
{
    public interface IReceiptPaymentService
    {
        Task<List<ReceiptPaymentVm>> GetAllAsync();
        Task<ReceiptPaymentVm> GetByIdAsync(int id);
        Task<ReceiptPaymentVm> InsertAsync(ReceiptPaymentVm model);
        Task<ReceiptPaymentVm> UpdateAsync(ReceiptPaymentVm model);
        Task<ReceiptPaymentVm> DeleteAsync(int id);

        Task<List<ReceiptPaymentVm>> GetByAsync(ReceiptPaymentSearchVm filter = null);
    }
}