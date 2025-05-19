using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IVatTaxCollateralService : IBaseService<VatTaxCollateral>
    {
        Task<VatTaxCollateral> GetByADPPaymentReceiveIdAsync(int id);
    }
}
