using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class MasterplanService : BaseService<Masterplan>, IMasterplanService
    {
        public MasterplanService(IDIMDBEntities context) : base(context)
        {
        }
    }
}