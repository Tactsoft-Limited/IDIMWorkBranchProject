using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Extentions;
using System.Web.Routing;
using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Models.User;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.User;

namespace IDIMWorkBranchProject.Controllers
{
    public class LoginController : AuthBaseController
    {
        protected IUserService UserService { get; set; }

        public LoginController(IUserService userService, IActivityLogService activityLogService) : base(
            activityLogService)
        { UserService = userService; }

        public ActionResult Index() { return RedirectToAction("Login"); }

        public ActionResult Login() { return View(); }

        public ActionResult RedirectDashboard(UserInformation userInformation, string r = null)
        {
            if (!string.IsNullOrEmpty(r))
                return Redirect(r);

            if (userInformation.Menus.Count <= 0)
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "no permission provide.");

            var userRoute = userInformation.Menus
                .SelectMany(e => e.Menus.Select(m => m))
                .OrderBy(e => (int)e.MenuType)
                .ThenBy(e => e.Priority)
                .FirstOrDefault();

            return new RedirectToRouteResult(
                new RouteValueDictionary(new { controller = userRoute?.ControllerName, action = "index" }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginVm model)
        {
            var message = "Incorrect username & password.";
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await UserService.LoginAsync(model.Username, model.Password, model.RememberMe);

                    if (user != null)
                    {
                        return RedirectToAction("Pin", "Login");
                    }

                    ModelState.AddModelError(string.Empty, message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, message);
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }

            return View(model);
        }

        public ActionResult Pin(string r)
        {
            Message message;
            var user = UserExtention.Get<UserInformation>(nameof(UserInformation));
            if (user == null)
                return RedirectToAction("Login", "Login", new { r });

            if (DefaultData.OtpEnable)
            {
                if (user.PinCodeValidate)
                {
                    return RedirectDashboard(user, r);
                }
            }
            else
            {
                return RedirectDashboard(user, r);
            }

            message = new Message
            {
                Text = $"A One-Time-Password(OTP) has been sent to your registered email",
                Type = "success",
                Icon = "fa-check"
            };
            ViewBag.Text = message;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Pin(LoginPinVm model, string r)
        {
            Message message;
            if (ModelState.IsValid)
            {
                var user = UserExtention.Get<UserInformation>(nameof(UserInformation));
                string combinedPinCode = string.Join(string.Empty, model.PinCode);

                if (user == null)
                    return RedirectToAction("Login", "Login", new { r });

                bool isValidOtp = await UserService.IsOtpValid(user.UserId, combinedPinCode);

                if (isValidOtp)
                {
                    user.PinCodeValidate = isValidOtp;
                    user.RememberMe = model.RememberMe;

                    UserExtention.Save(user);

                    return RedirectDashboard(user, r);
                }
                else
                {
                    message = new Message { Text = $"Invalid OTP", Type = "danger", Icon = "fa-times" };
                    ViewBag.Text = message;

                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<ActionResult> Resend()
        {
            var user = UserExtention.Get<UserInformation>(nameof(UserInformation));

            if (user == null)
                return RedirectToAction("Login");

            await UserService.SaveOtpAndSendEmail(user.UserId);

            return RedirectToAction("Pin");
        }

        public ActionResult Lock()
        {
            var user = UserExtention.Get<UserInformation>(nameof(UserInformation));
            if (user == null)
                return RedirectToAction("Login");

            var model = new LoginVm
            {
                Name = string.IsNullOrEmpty(user.Name) ? user.Username : user.Name,
                Username = user.Username,
                Avatar = user.Avatar
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Lock(LoginVm model)
        {
            var message = "Incorrect username & password.";
            try
            {
                if (ModelState.IsValid)
                {
                    //database check
                    var user = await UserService.LoginAsync(model.Username, model.Password, model.RememberMe);

                    if (user != null)
                    {
                        return RedirectDashboard(user);
                    }

                    ModelState.AddModelError(string.Empty, message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, message);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Remove(nameof(UserInformation));
            Session.Clear();
            Session.RemoveAll();

            return RedirectToAction("Login");
        }
    }
}