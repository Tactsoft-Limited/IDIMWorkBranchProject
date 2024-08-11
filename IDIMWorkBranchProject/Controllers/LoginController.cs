using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Models.User;
using IDIMWorkBranchProject.Services.User;

namespace IDIMWorkBranchProject.Controllers
{
    public class LoginController : Controller
    {
        protected IUserService UserService { get; set; }

        public LoginController(IUserService userService)
        {
            UserService = userService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View(new LoginVm());
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

                    ModelState.AddModelError("", message);
                }
                else
                {
                    ModelState.AddModelError("", message);
                }
            }
            catch (Exception exception)
            {
                throw;
            }

            return View(model);
        }

        public ActionResult Pin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pin(LoginPinVm model)
        {
            if (ModelState.IsValid)
            {
                var session = UserExtention.Get<UserInformation>(nameof(UserInformation));

                if (session == null)
                    return Redirect("/");

                session.PinCodeValidate = true;
                session.RememberMe = model.RememberMe;

                UserExtention.Save(session);

                if (string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                return Redirect(model.ReturnUrl);
            }

            return View(model);
        }
    }
}