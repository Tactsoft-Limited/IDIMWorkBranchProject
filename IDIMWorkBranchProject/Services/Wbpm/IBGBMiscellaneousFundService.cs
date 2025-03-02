using BGB.Data.Entities.Wbpm;
using BGB.Data.SqlViews.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IBGBMiscellaneousFundService : IBaseService<BGBMiscellaneousFund>
    {        
        Task<BGBMiscellaneousFund> GetByADPPaymentReceiveIdAsync(int id);
        Task<List<BGBMiscellaneousFund>> GetByProjectWorkIdAsync(int projectWorkId);

    }
}
