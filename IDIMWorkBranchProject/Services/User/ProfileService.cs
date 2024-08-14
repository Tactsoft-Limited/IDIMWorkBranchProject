using AutoMapper;
using BGB.Data.Database;
using BGB.Data.Entities.Admin;
using IDIMWorkBranchProject.Models.Setup;
using IDIMWorkBranchProject.Models.User;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.User
{
    public class ProfileService : IProfileService
    {
        protected IMapper Mapper { get; set; }

        protected IDIMDBEntities Context { get; set; }

        public ProfileService(IMapper mapper)
        {
            Context = new IDIMDBEntities();
            Mapper = mapper;
        }

        public async Task<IList<MenuGroupByVm>> GetMenuByUserIdAsync(int userId)
        {
            var menus = await MenuByApplication().ToListAsync();
            var userMenus = await UserMenuByUserId(userId).ToListAsync();

            if (userMenus.Any())
                menus = menus.Select(
                    mg =>
                    {
                        mg.Menus = mg.Menus
                            .Select(
                                l =>
                                {
                                    l.IsAssigned = userMenus.Select(e => e.MenuId).Contains(l.MenuId);
                                    return l;
                                })
                            .ToList();

                        return mg;
                    })
                    .ToList();

            return menus;
        }

        private IQueryable<MenuGroupByVm> MenuByApplication()
        {
            var menuGroups = Context.Applications
                .Include(e => e.Menus)
                .GroupBy(e => new { e.ApplicationId, e.ApplicationName })
                .Select(
                    g => new MenuGroupByVm
                    {
                        ApplicationId = g.Key.ApplicationId,
                        ApplicationName = g.Key.ApplicationName,
                        Menus =
                            g.SelectMany(
                                    m => m.Menus
                                        .Select(
                                            l => new MenuAssignVm
                                            {
                                                MenuId = l.MenuId,
                                                Title = l.Title,
                                                ControllerName = l.ControllerName,
                                                ApplicationId = l.ApplicationId
                                            }))
                                    .ToList()
                    })
                .OrderBy(e => e.ApplicationName);

            return menuGroups;
        }

        private IQueryable<RolePrivilege> UserMenuByUserId(int userId)
        {
            return (from up in Context.UserPrivileges
                    join rp in Context.RolePrivileges on up.RoleId equals rp.RoleId
                    where up.UserId == userId
                    select rp);
        }

        public async Task<GeneralInformationVm> GetRegimentInfoByUserId(int? id)
        {
            var generalInformation = await Context.GeneralInformations.FirstOrDefaultAsync(e => e.ArmyId == id);

            return Mapper.Map<GeneralInformationVm>(generalInformation);
        }

        public async Task<IList<ApplicationVm>> GetApplicationsByUserId(int userId)
        {
            var list = await (from up in Context.UserPrivileges
                              join r in Context.Roles on up.RoleId equals r.RoleId
                              where up.UserId == userId
                              select r.Application).ToListAsync();

            return Mapper.Map<IList<ApplicationVm>>(list);
        }
    }
}