using System.Collections.Generic;
using System.Threading.Tasks;
using IDIMWorkBranchProject.Models.WBP;

namespace IDIMWorkBranchProject.Services.WBP
{
    public interface IConsultancyFeesService
    {
        Task<List<ConsultancyFeesVm>> GetAllAsync();
        Task<ConsultancyFeesVm> GetByIdAsync(int id);
        Task<ConsultancyFeesVm> InsertAsync(ConsultancyFeesVm model);
        Task<ConsultancyFeesVm> UpdateAsync(ConsultancyFeesVm model);
        Task<ConsultancyFeesVm> DeleteAsync(int id);

        Task<List<ConsultancyFeesVm>> GetByAsync(ConsultancyFeesSearchVm filter = null);
    }
}
