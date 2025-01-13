using AutoMapper;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Extentions.Ad;
using IDIMWorkBranchProject.Extentions.File;
using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Extentions.Url;
using IDIMWorkBranchProject.Models;
using IDIMWorkBranchProject.Models.User;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

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

        private async Task<IList<MenuInformation>> GetRoleMenuAsync(List<int> roleList, int applicationId)
        {
            var menus = (from r in Context.Roles
                         join rp in Context.RolePrivileges on r.RoleId equals rp.RoleId
                         join m in Context.Menus on rp.MenuId equals m.MenuId
                         where roleList.Contains(r.RoleId) && m.IsPublished == true
                         select m).GroupBy(e => e.MenuType);


            return await menus.Select(
                e => new MenuInformation
                {
                    MenuType = (MenuType)e.Key,
                    Menus =
                        e.Select(
                                m => new MenuVm
                                {
                                    Title = m.Title,
                                    ControllerName = m.ControllerName,
                                    MenuId = m.MenuId,
                                    Icon = m.Icon,
                                    MenuType = (MenuType)m.MenuType,
                                    Priority = m.Priority
                                })
                                .OrderBy(m => m.Priority)
                                .Distinct()
                                .ToList()
                })
                .ToListAsync();
        }

        private async Task<IList<ApplicationVm>> GetUserApplicationAsync(List<int> roleList)
        {
            if (roleList.Count != 0)
            {
                int roleId = roleList[0];
                var menus = (from r in Context.Roles
                             join a in Context.Applications on r.ApplicationId equals a.ApplicationId
                             where r.RoleId == roleId
                             select a);

                return await menus.Select(
                    e => new ApplicationVm
                    {
                        ApplicationName = e.ApplicationName,
                        ApplicationCode = e.ApplicationCode,
                        ApplicationShortName = e.ApplicationShortName,
                        ApplicationId = e.ApplicationId,
                        Color = e.Color,
                        Icon = e.Icon,
                        Url = e.Url
                    })
                    .ToListAsync();
            }
            else
            {
                return null;
            }
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

        public async Task<UserVm> GetByIdAsync(int id, bool checkActive = false)
        {
            var user = await Context.Users.FindAsync(id);

            if (checkActive)
            {
                user = user?.IsActive == true ? user : null;
            }

            return Mapper.Map<UserVm>(user);
        }

        public async Task<UserVm> InsertAsync(UserVm model)
        {
            var existing = await Context.Users.FirstOrDefaultAsync(m => m.Username == model.Username);
            if (existing != null)
                throw new ArgumentException($"{nameof(model.Username)} already exists ");

            var entity = Mapper.Map<BGB.Data.Entities.Admin.User>(model);
            entity.CreatedDateTime = DateTime.Now;
            entity.CreatedUser = UserExtention.GetUserId();

            var added = Context.Users.Add(entity);
            await Context.SaveChangesAsync();

            return Mapper.Map<UserVm>(added);
        }

        public Task<UserVm> UpdateAsync(UserVm model) { throw new NotImplementedException(); }

        public async Task<UserVm> UpdatePasswordAsync(UserVm model)
        {
            var user = await Context.Users.FirstOrDefaultAsync(e => e.Username == model.Username);

            if (user == null)
                return null;

            user.Password = model.Password;
            await Context.SaveChangesAsync();

            return Mapper.Map<UserVm>(user);
        }

        public async Task<UserVm> ChangePasswordAsync(ChangePasswordViewModel model)
        {
            var user = await Context.Users.FirstOrDefaultAsync(e => e.Username == model.Username);

            if (user == null)
                throw new ArgumentException($"User or password not correct, try again.");

            if (!string.Equals(model.OldPassword, user.Password) && model.OldPassword != null)
                throw new ArgumentException($"User or password not correct, try again.");

            user.Password = model.NewPassword;

            await Context.SaveChangesAsync();

            return Mapper.Map<UserVm>(user);
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

        public async Task<UserInformation> LoginAsync(string username, string password, bool? rememberMe)
        {
            var user = await Context.Users
                .Include(e => e.GeneralInformation)
                .FirstOrDefaultAsync(e => e.Username == username && e.IsActive);

            if (user == null)
                return null;

            if (!string.Equals(user.Password, password))
                return null;

            var roleList = await (from r in Context.Roles
                                  join up in Context.UserPrivileges on r.RoleId equals up.RoleId
                                  where r.ApplicationId == DefaultData.ApplicationId && up.UserId == user.UserId
                                  select r.RoleId).ToListAsync();

            var userInformation = new UserInformation
            {
                UserId = user.UserId,
                ArmyId = user.ArmyId,
                Name = user.GeneralInformation?.Name,
                Username = user.Username,
                UnitId = user.GeneralInformation?.UnitId,
                ActiveStatus = user.GeneralInformation.IsActiveStatus,
                DeviceId = 1,
                ApplicationId = DefaultData.ApplicationId,
                Avatar = user.GeneralInformation?.Picture.ToThumbnail(),
                Menus = await GetRoleMenuAsync(roleList, DefaultData.ApplicationId),
                Applications = await GetUserApplicationAsync(roleList)
            };

            // TODO: menu information

            UserExtention.Save(nameof(UserInformation), userInformation);

            if (DefaultData.OtpEnable)
            {
                await SaveOtpAndSendEmail(user.UserId);
            }

            return userInformation;
        }

        public async Task SaveOtpAndSendEmail(int userId)
        {
            string otp = GenerateOTP();
            var user = await Context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null)
                throw new ArgumentException("User can not be found!");

            string emailBody = string.Format(DefaultData.EmailBody, otp, DefaultData.OTPExpiredTime);

            user.OtpCode = otp;
            user.OtpGeneratedDate = DateTime.Now;
            await Context.SaveChangesAsync();

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(DefaultData.EmailForm));
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject = DefaultData.EmailSubject;
            email.Body = new TextPart(TextFormat.Html) { Text = emailBody };

            using (var smtp = new SmtpClient())
            {
                //smtp.Connect(DefaultData.EmailServer, DefaultData.EmailPort, SecureSocketOptions.StartTls);
                smtp.Connect(DefaultData.EmailServer, DefaultData.EmailPort, false);
                smtp.Authenticate(DefaultData.EmailUserName, DefaultData.EmailPassword);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }

        public async Task<bool> IsOtpValid(int userId, string otp)
        {
            var user = await Context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null)
                throw new ArgumentException("User can not be found!");

            bool isWithinTimePeriod = false;
            if (user.OtpGeneratedDate.HasValue)
            {
                DateTime currentTime = DateTime.Now;
                TimeSpan difference = currentTime - user.OtpGeneratedDate.Value;

                isWithinTimePeriod = difference.TotalMinutes <= 5;
            }

            if (user.OtpCode == otp && isWithinTimePeriod)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Login process with active diretory user check and sql server database user check. First check ad username &
        /// password, then check database table user, if ok then permit to login If ad user exists but db table user not
        /// exists then create or update
        /// </summary>
        /// <param name="username">ad username</param>
        /// <param name="password">ad password</param>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        public async Task<UserInformation> AdLoginAsync(string username, string password, bool? rememberMe)
        {
            var adConnection = new LdapExtention(DefaultData.AdServer, username, password);

            if (adConnection?.Entry.NativeObject == null)
                throw new ArgumentException("User not found.");

            var user = await Context.Users.FirstOrDefaultAsync(e => e.Username == username);
            var newUser = new UserVm { Username = username, Password = password, IsActive = true };

            if (user == null)
            {
                var addeduser = await InsertAsync(newUser);
                user = Mapper.Map<BGB.Data.Entities.Admin.User>(addeduser);
                //throw new ArgumentException("User not valid to access.");
            }
            else
            {
                if (!string.Equals(user.Password, password))
                {
                    var updatedUser = await UpdatePasswordAsync(newUser);
                    user = Mapper.Map<BGB.Data.Entities.Admin.User>(updatedUser);
                    // throw new ArgumentException("User not valid to access.");
                }

                if (!user.IsActive)
                    throw new ArgumentException("User is not active, contact with your administrator");
            }

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
                if (armyInfo != null && !string.IsNullOrEmpty(armyInfo.Picture))
                    i.Avatar = UrlExtention.GetAvatar(armyInfo.Picture);
            }

            // TODO: menu information

            UserExtention.Save(nameof(UserInformation), i);

            //TODO : for test purpose, remove after test, implement edited army selection
            //if (i.ArmyId != null)
            //{
            //    await UpdateEditInformation((int)i.ArmyId);
            //}

            return i;
        }

        private static string GenerateOTP()
        {
            Random random = new Random();
            int otp = random.Next(1000, 9999);

            return otp.ToString();
        }
    }
}