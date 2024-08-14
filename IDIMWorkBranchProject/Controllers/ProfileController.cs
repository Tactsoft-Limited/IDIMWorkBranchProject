using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Models.User;
using IDIMWorkBranchProject.Services.User;
using IDIMWorkBranchProject.Services;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;

namespace IDIMWorkBranchProject.Controllers
{
    public class ProfileController : BaseController
    {
        protected IUserService UserService { get; set; }

        protected IProfileService ProfileService { get; set; }

        public ProfileController(
            IUserService userService,
            IProfileService profileService,
            IActivityLogService activityLogService) : base(activityLogService)
        {
            UserService = userService;
            ProfileService = profileService;
            ActivityLogService = activityLogService;
        }

        public async Task<ActionResult> Index()
        {
            var userData = UserExtention.Get<UserInformation>(nameof(UserInformation));

            if (userData == null)
                return Redirect("~/");

            var user = await UserService.GetByIdAsync(userData.UserId);
            if (user == null)
                return HttpNotFound();

            var menus = await ProfileService.GetMenuByUserIdAsync(user.UserId);
            menus = menus.Where(e => e.Menus.Any(m => m.IsAssigned))
                .Select(
                    e => new MenuGroupByVm
                    {
                        ApplicationId = e.ApplicationId,
                        ApplicationName = e.ApplicationName,
                        Menus = e.Menus.Where(m => m.IsAssigned).Select(l => l).ToList()
                    })
                .ToList();

            var profile = new UserProfile
            {
                User = user,
                Regiment = await ProfileService.GetRegimentInfoByUserId(user.ArmyId),
                Applications = await ProfileService.GetApplicationsByUserId(user.UserId),
                Menus = menus,
                //Devices = await DeviceService.GetByUserIdAsync(user.UserId)
            };

            return View(profile);
        }
    }
}