using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IDIMWorkBranchProject.Entity;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Extentions.Url;
using IDIMWorkBranchProject.Models.User;

namespace IDIMWorkBranchProject.Services.User
{
    public class UserService : IUserService
    {
        protected IMapper Mapper { get; set; }
        protected IDIMDBEntities Context { get; set; }

        public UserService(IMapper mapper)
        {
            Context = new IDIMDBEntities();
            Mapper = mapper;
        }

        public async Task<List<UserVm>> GetAllAsync(bool excludeNotActive = true)
        {
            var query = Context.Users.AsQueryable();

            if (excludeNotActive)
            {
                query = query.Where(e => e.IsActive);
            }

            var users = await query.ToListAsync();

            return Mapper.Map<List<UserVm>>(users);
        }

        public async Task<List<UserVm>> GetUserByFilterAsync(string username = null, string regiment = null, int? applicationId = null, int? deviceId = null, Active active = Active.All)
        {
            var query = (from u in Context.Users
                         join up in Context.UserApplications on u.UserId equals up.UserId into upg
                         from up in upg.DefaultIfEmpty()
                         join ud in Context.UserDevices on u.UserId equals ud.UserId into udg
                         from ud in udg.DefaultIfEmpty()
                         select new
                         {
                             u,
                             up,
                             ud
                         }).AsQueryable();

            if (!string.IsNullOrEmpty(username))
                query = query.Where(e => e.u.Username.Contains(username));

            if (!string.IsNullOrEmpty(regiment))
                query = query.Where(e => e.u.GeneralInformation.RegimentNo.Contains(regiment));

            if (applicationId.HasValue)
                query = query.Where(e => e.up.ApplicationId == applicationId);

            if (deviceId.HasValue)
                query = query.Where(e => e.ud.DeviceId == deviceId);

            bool? activeValue = null;
            if (active == Active.Active)
                activeValue = true;

            if (active == Active.NotActive)
                activeValue = false;

            if (activeValue.HasValue)
                query = query.Where(e => e.u.IsActive == activeValue);

            var users = await query.Select(e => e.u).ToListAsync();

            return Mapper.Map<List<UserVm>>(users);
        }

        public async Task<UserVm> GetByIdAsync(int id, bool checkActive = false)
        {
            var user = await Context.Users.FindAsync(id);

            if (checkActive)
            {
                user = user?.IsActive == true ? user : null;
            }

            return Mapper.Map<UserVm>(user);
        }

        public async Task<UserVm> InsertAsync(RegisterVm model)
        {
            var existing = await Context.Users.FirstOrDefaultAsync(m => m.Username == model.Username);
            if (existing != null)
                throw new ArgumentException($"{nameof(model.Username)} already exists ");

            var entity = Mapper.Map<Entity.User>(model);
            entity.IsActive = true;
            entity.CreatedDateTime = DateTime.Now;
            entity.CreatedUser = UserExtention.GetUserId();

            var added = Context.Users.Add(entity);
            await Context.SaveChangesAsync();

            return Mapper.Map<UserVm>(added);
        }

        public async Task<UserVm> UpdateAsync(UserVm model)
        {
            var existing = await Context.Users.FindAsync(model.UserId);
            if (existing == null)
                throw new ArgumentException($"{nameof(model.Username)} not found.");

            var duplicate = await Context.Users.Where(e => e.UserId != model.UserId)
                .FirstOrDefaultAsync(d => d.Username == model.Username);

            if (duplicate != null)
                throw new ArgumentException($"{nameof(model.Username)} alreay exists.");

            var entity = Mapper.Map<Entity.User>(model);
            entity.ArmyId = model.ArmyId;
            entity.Username = model.Username;
            entity.ResourceType = model.ResourceType;
            entity.UserType = model.UserType;
            entity.PersonnelCode = model.PersonnelCode;
            entity.Phone = model.Phone;
            entity.IsActive = model.IsActive;
            entity.UpdatedDateTime = DateTime.Now;
            entity.UpdatedUser = UserExtention.GetUserId();
            entity.UpdateNo += 1;
            await Context.SaveChangesAsync();

            return Mapper.Map<UserVm>(entity);
        }

        public async Task<UserVm> DeleteAsync(int id)
        {
            var existing = await Context.Users.FindAsync(id);

            if (existing == null)
                throw new ArgumentException($"{nameof(existing.UserId)} not found.");

            Context.Entry(existing).State = EntityState.Deleted;
            await Context.SaveChangesAsync();

            return Mapper.Map<UserVm>(existing);
        }

        #region user menu

        /// <summary>
        /// Get all menus with assigned menu list
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns></returns>
        public async Task<IList<MenuGroupByVm>> GetUserMenuAsync(int userId)
        {
            var menuGroups = await (from m in Context.Menus
                                    join a in Context.Applications on m.ApplicationId equals a.ApplicationId
                                    group m by new { a.ApplicationId, a.ApplicationName } into g
                                    select new MenuGroupByVm
                                    {
                                        ApplicationId = g.Key.ApplicationId,
                                        ApplicationName = g.Key.ApplicationName,
                                        Menus = g.Select(e => new MenuAssignVm
                                        {
                                            Menu = new MenuVm
                                            {
                                                MenuId = e.MenuId,
                                                Title = e.Title,
                                                ControllerName = e.ControllerName,
                                                ApplicationId = e.ApplicationId
                                            }
                                        }).ToList()
                                    }).ToListAsync();


            var userApplications = await Context.UserApplications.Where(e => e.UserId == userId)
                .Select(e => e.ApplicationId).ToListAsync();

            menuGroups = menuGroups.Where(e => userApplications.Contains(e.ApplicationId)).ToList();

            var userPriviledges = await Context.UserPriviledges
                .Where(e => e.UserId == userId)
                .ToListAsync();

            if (!userPriviledges.Any())
                return menuGroups;

            foreach (var userPriviledge in userPriviledges)
            {
                foreach (var menuGroup in menuGroups)
                {
                    foreach (var userMenu in menuGroup.Menus)
                    {
                        if (userPriviledge.MenuId == userMenu.Menu.MenuId)
                            userMenu.IsAssigned = true;
                    }
                }
            }

            return menuGroups;
        }
        #endregion

        #region user application
        /// <summary>
        /// Get all applications with assigned application list
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns></returns>
        public async Task<IList<ApplicationAssignVm>> GetUserApplicationAsync(int userId)
        {
            var applications = await Context.Applications.Select(e => new ApplicationAssignVm
            {
                Application = new ApplicationVm
                {
                    ApplicationId = e.ApplicationId,
                    ApplicationName = e.ApplicationName
                }
            }).ToListAsync();

            var userApplications = await Context.UserApplications
                .Where(e => e.UserId == userId)
                .ToListAsync();

            if (!userApplications.Any())
                return applications;

            applications = applications.Select(e => new ApplicationAssignVm
            {
                Application = e.Application,
                IsAssigned = userApplications.Any(m => m.ApplicationId == e.Application.ApplicationId)
            }).ToList();

            return applications;
        }
        #endregion

        #region login
        public async Task<UserInformation> LoginAsync(string username, string password, bool? rememberMe)
        {
            var user = await Context.Users.FirstOrDefaultAsync(e => e.Username == username && e.IsActive);

            if (user == null)
                return null;

            if (!string.Equals(user.Password, password))
                return null;

            var i = new UserInformation
            {
                UserId = user.UserId,
                ArmyId = user.ArmyId,
                Username = user.Username,
                DeviceId = 1,
                ApplicationId = 1,
                Avatar = DefaultData.DefaultAvatar
            };

            if (i.ArmyId != null)
            {
                var armyInfo = await Context.GeneralInformations.FindAsync((int)i.ArmyId);
                i.Username = armyInfo?.Name ?? i.Username;
                if (!string.IsNullOrEmpty(armyInfo?.Picture))
                    i.Avatar = UrlExtention.GetAvatar(armyInfo.Picture);
            }

            // TODO: menu information

            UserExtention.Save(nameof(UserInformation), i);

            return i;
        }
        #endregion
    }
}