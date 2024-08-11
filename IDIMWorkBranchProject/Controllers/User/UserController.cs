using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.User;
using IDIMWorkBranchProject.Services.User;

namespace IDIMWorkBranchProject.Controllers.User
{
    public class UserController : Controller
    {
        protected IApplicationService ApplicationService { get; set; }
        protected IDeviceService DeviceService { get; set; }
        protected IMenuService MenuService { get; set; }
        protected IUserService UserService { get; set; }
        protected IUserApplicationService UserApplicationService { get; set; }
        protected IUserPriviledgeService UserPriviledgeService { get; set; }

        public UserController(IApplicationService applicationService,
            IDeviceService deviceService,
            IMenuService menuService,
            IUserService userService,
            IUserApplicationService userApplicationService,
            IUserPriviledgeService userPriviledgeService)
        {
            ApplicationService = applicationService;
            DeviceService = deviceService;
            MenuService = menuService;
            UserService = userService;
            UserApplicationService = userApplicationService;
            UserPriviledgeService = userPriviledgeService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public async Task<ActionResult> List()
        {
            var model = new UserSearchVm
            {
                ApplicationDropdown =  await ApplicationService.GetDropDownAsync(),
                DeviceDropdown = await DeviceService.GetDropDownAsync(),
                Users = await UserService.GetUserByFilterAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> List(UserSearchVm model)
        {
            model.ApplicationDropdown = await ApplicationService.GetDropDownAsync(model.ApplicationId);
            model.DeviceDropdown = await DeviceService.GetDropDownAsync();
            model.Users = await UserService.GetUserByFilterAsync(model.Username, model.Regiment, model.ApplicationId, model.DeviceId, model.IsActive);

            return View(model);
        }

        public async Task<ActionResult> Details(int id)
        {
            var menus = await UserService.GetUserMenuAsync(id);
            menus = menus.Where(e => e.Menus.Any(m => m.IsAssigned)).ToList();

            var model = new UserDetailVm
            {
                User = await UserService.GetByIdAsync(id),
                Applications = await ApplicationService.GetByUserIdAsync(id),
                Devices = await DeviceService.GetByUserIdAsync(id),
                Menus = menus
            };

            return View(model);
        }
        
        public ActionResult Create()
        {
            return View(new RegisterVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await UserService.InsertAsync(model);

                    ModelState.Clear();
                    model = new RegisterVm();

                    message = Messages.Success(MessageType.Create.ToString());

                }
                else
                {
                    message = Messages.InvalidInput(MessageType.Create.ToString());
                }
            }
            catch (Exception exception)
            {
                message = Messages.Failed(MessageType.Create.ToString(), exception.Message);
            }

            ViewBag.Message = message;

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var user = await UserService.GetByIdAsync(id);

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await UserService.UpdateAsync(model);

                    message = Messages.Success(MessageType.Update.ToString());
                }
                else
                {
                    message = Messages.InvalidInput(MessageType.Update.ToString());
                }
            }
            catch (Exception exception)
            {
                message = Messages.Failed(MessageType.Update.ToString(), exception.Message);
            }

            ViewBag.Message = message;

            return View(model);
        }
        
        public async Task<ActionResult> ChangePassword(int id)
        {
            var user = await UserService.GetByIdAsync(id);
            if (user == null)
                return HttpNotFound();

            var model = new ChangePasswordVm
            {
                UserId    = user.UserId,
                Username = user.Username
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordVm model)
        {
            return View();
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Route("user/{id:int}/application")]
        public async Task<ActionResult> Application(int id)
        {
            var user = await UserService.GetByIdAsync(id);
            var model = new UserApplicationAssignVm 
            {
                UserId = user.UserId,
                RegimentNo = user.RegimentNo,
                Username = user.Username,
                Applications = await UserService.GetUserApplicationAsync(id),
            };

            return View(model);
        }

        [HttpPost]
        [Route("user/{id:int}/application")]
        public async Task<ActionResult> Application(UserApplicationAssignVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await UserApplicationService.InsertDeleteAsync(model.UserId, model.Applications);

                    message = Messages.Success("Applications assigne");
                }
                else
                {
                    message = Messages.InvalidInput(MessageType.Create.ToString());
                }
            }
            catch (Exception exception)
            {
                message = Messages.Failed(MessageType.Create.ToString(), exception.Message);
            }

            ViewBag.Message = message;

            return View(model);
        }

        public async Task<ActionResult> Device(int id)
        {
            var user = await UserService.GetByIdAsync(id);
            var model = new UserDeviceAssignVm
            {
                UserId = user.UserId,
                RegimentNo = user.RegimentNo,
                Username = user.Username
            };
            return View(model);
        }

        public async Task<ActionResult> Menu(int id)
        {
            var user = await UserService.GetByIdAsync(id);
            var model = new UserMenuAssignVm
            {
                UserId = user.UserId,
                RegimentNo = user.RegimentNo,
                Username = user.Username,
                Menus = await UserService.GetUserMenuAsync(id),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Menu(UserMenuAssignVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await UserPriviledgeService.InsertDeleteAsync(model.UserId, model.Menus);

                    message = Messages.Success("Menus assigne");
                }
                else
                {
                    message = Messages.InvalidInput(MessageType.Create.ToString());
                }
            }
            catch (Exception exception)
            {
                message = Messages.Failed(MessageType.Create.ToString(), exception.Message);
            }

            ViewBag.Message = message;

            return View(model);
        }
    }
}
