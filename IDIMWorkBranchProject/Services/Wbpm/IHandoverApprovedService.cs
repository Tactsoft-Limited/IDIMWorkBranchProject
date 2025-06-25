using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IHandoverApprovedService : IBaseService<HandoverApproved>
    {
        Task<List<HandoverApproved>> GetAllByProjectWorkIdAsync(int id);
        Task<HandoverApproved> GetByProjectWorkIdAsync(int id);
    }
}
