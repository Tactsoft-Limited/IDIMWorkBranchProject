using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IConstructionCompanyService : IBaseService<ConstructionCompany>
    {
        Task<object> GetPagedAsync(ConstructionCompanySearchVm model);
    }
}
