using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class BGBMiscellaneousFundService : BaseService<BGBMiscellaneousFund>, IBGBMiscellaneousFundService
    {
        public BGBMiscellaneousFundService(IDIMDBEntities context) : base(context)
        {
        }


    }
}