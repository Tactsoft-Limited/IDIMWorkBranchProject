using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IFurnitureBillPaymentService : IBaseService<FurnitureBillPayment>
    {
        Task<FurnitureBillPayment> GetByProjectWorkIdAsync(int id);
        Task<List<FurnitureBillPayment>> GetAllByProjectWorkIdAsync(int id);
    }
}
