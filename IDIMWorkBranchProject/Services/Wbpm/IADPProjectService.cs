using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IADPProjectService : IBaseService<ADPProject>
    {
        Task<string> GetAdpProjectTitle(int aDPProjectId);
        Task<object> GetPagedAsync(ADPProjectSearchVm model);
    }
}
