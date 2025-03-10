using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class VatTaxCollateralService : BaseService<VatTaxCollateral>, IVatTaxCollateralService
    {
        public VatTaxCollateralService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<VatTaxCollateral> GetByADPPaymentReceiveIdAsync(int id)
        {
            return await _context.VatTaxCollaterals.Where(x=>x.ADPReceivePaymentId == id).FirstOrDefaultAsync();
        }
    }
}