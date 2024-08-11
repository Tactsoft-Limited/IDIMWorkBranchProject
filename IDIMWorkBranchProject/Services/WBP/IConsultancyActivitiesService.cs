using System.Collections.Generic;
using System.Threading.Tasks;
using IDIMWorkBranchProject.Models.WBP;

namespace IDIMWorkBranchProject.Services.WBP
{
    public interface IConsultancyActivitiesService
    {
        Task<List<ConsultancyActivitiesVm>> GetAllAsync();
        Task<ConsultancyActivitiesVm> GetByIdAsync(int id);
        Task<ConsultancyActivitiesVm> InsertAsync(ConsultancyActivitiesVm model);
        Task<ConsultancyActivitiesVm> UpdateAsync(ConsultancyActivitiesVm model);
        Task<ConsultancyActivitiesVm> DeleteAsync(int id);

        Task<List<ConsultancyActivitiesVm>> GetByAsync(ConsultancyActivitiesSearchVm filter = null);
    }
}
