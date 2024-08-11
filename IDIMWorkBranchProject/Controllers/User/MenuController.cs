using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.User;
using IDIMWorkBranchProject.Services.User;

namespace IDIMWorkBranchProject.Controllers.User
{
    public class MenuController : Controller
    {
        protected IApplicationService ApplicationService { get; set; }
        protected IMenuService MenuService { get; set; }

        public MenuController(IApplicationService applicationService,
            IMenuService menuService)
        {
            ApplicationService = applicationService;
            MenuService = menuService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public async Task<ActionResult> List()
        {
            var menus = await MenuService.GetAllAsync();

            return View(menus);
        }

        public async Task<ActionResult> Create()
        {
            var model = new MenuVm
            {
                ApplicationDropdown = await ApplicationService.GetDropDownAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MenuVm model)
        {
            Message message;
            if (ModelState.IsValid)
            {
                try
                {
                    await MenuService.InsertAsync(model);
                    ModelState.Clear();
                    model = new MenuVm();
                    message = Messages.Success(MessageType.Create.ToString());
                }
                catch (Exception exception)
                {
                    message = Messages.Failed(MessageType.Create.ToString(), exception.Message);
                }
            }
            else
            {
                message = Messages.InvalidInput(MessageType.Create.ToString());
            }

            model.ApplicationDropdown = await ApplicationService.GetDropDownAsync(model.ApplicationId);

            ViewBag.Message = message;

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await MenuService.GetByIdAsync(id);
            if (model == null)
                return HttpNotFound();

            model.ApplicationDropdown = await ApplicationService.GetDropDownAsync(model.ApplicationId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MenuVm model)
        {
            Message message;
            if (ModelState.IsValid)
            {
                try
                {
                    await MenuService.UpdateAsync(model);
                    message = Messages.Success(MessageType.Update.ToString());
                }
                catch (Exception exception)
                {
                    message = Messages.Failed(MessageType.Update.ToString(), exception.Message);
                }
            }
            else
            {
                message = Messages.InvalidInput(MessageType.Update.ToString());
            }

            model.ApplicationDropdown = await ApplicationService.GetDropDownAsync(model.ApplicationId);

            ViewBag.Message = message;

            return View(model);
        }

        public async Task<ActionResult> Delete(string id)
        {
            int.TryParse(id, out int menuId);

            var model = await MenuService.GetByIdAsync(menuId);
            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await MenuService.DeleteAsync(id);
            }
            catch (Exception exception)
            {
                var model = await MenuService.GetByIdAsync(id);
                ViewBag.Message = Messages.Failed(MessageType.Delete.ToString(), exception.Message);

                return View(model);
            }

            return RedirectToAction("List");
        }

        public ActionResult Generate()
        {
            return View();
        }   

        /// <summary>
        /// All menu data
        /// </summary>
        /// <returns>All menu data in JSON format</returns>
        public async Task<ActionResult> Get()
        {
            var leaveTypes = await MenuService.GetAllAsync();
            var data = new
            {
                data = leaveTypes
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
