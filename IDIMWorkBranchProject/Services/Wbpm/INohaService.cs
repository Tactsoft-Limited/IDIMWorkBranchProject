using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface INohaService : IBaseService<Noha>
    {
        Task<Noha> GetByProjectWorkIdAsync(int id);
    }
}
