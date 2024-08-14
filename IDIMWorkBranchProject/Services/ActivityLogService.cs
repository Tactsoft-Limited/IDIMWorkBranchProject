using AutoMapper;
using BGB.Data.Database;
using BGB.Data.Entities.Admin;
using IDIMWorkBranchProject.Models.User;

namespace IDIMWorkBranchProject.Services
{
    public class ActivityLogService : IActivityLogService
    {
        protected IMapper Mapper { get; set; }

        protected IDIMDBEntities Context { get; set; }

        public ActivityLogService(IMapper mapper)
        {
            Mapper = mapper;
            Context = new IDIMDBEntities();
        }

        public void InsertAsync(ActivityLogVm model)
        {
            var entity = Mapper.Map<ActivityLog>(model);
            Context.ActivityLogs.Add(entity);
            Context.SaveChangesAsync();
        }
    }
}