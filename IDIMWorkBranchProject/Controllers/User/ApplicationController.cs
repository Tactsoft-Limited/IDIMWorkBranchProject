using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.User;
using IDIMWorkBranchProject.Services.User;

namespace IDIMWorkBranchProject.Controllers.User
{
    public class ApplicationController : Controller
    {
        protected IApplicationService ApplicationService { get; set; }
        protected IMenuService MenuService { get; set; }

        public ApplicationController(IApplicationService applicationService,
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
            var applications = await ApplicationService.GetAllAsync();

            return View(applications);
        }

        public async Task<ActionResult> Details(int id)
        {
            var application = await ApplicationService.GetByIdAsync(id);
            var menus = await MenuService.GetByApplicationIdAsync(id);
            var model = new ApplicationDetailVm
            {
                Application =  application,
                Menus =  menus
            };

            return View(model);
        }

        public ActionResult Create()
        {
            return View(new ApplicationVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ApplicationVm model)
        {
            Message message;
            if (ModelState.IsValid)
            {
                try
                {
                    await ApplicationService.InsertAsync(model);
                    ModelState.Clear();
                    model = new ApplicationVm();
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

            TempData["Message"] = message;

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await ApplicationService.GetByIdAsync(id);
            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ApplicationVm model)
        {
            Message message;
            if (ModelState.IsValid)
            {
                try
                {
                    await ApplicationService.UpdateAsync(model);
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

            TempData["Message"] = message;

            return View(model);
        }

        public async Task<ActionResult> Delete(string id)
        {
            int.TryParse(id, out int applicationId);

            var model = await ApplicationService.GetByIdAsync(applicationId);
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
                await ApplicationService.DeleteAsync(id);
            }
            catch (Exception exception)
            {
                var model = await ApplicationService.GetByIdAsync(id);
                TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), exception.Message);

                return View(model);
            }

            return RedirectToAction("List");
        }

        /// <summary>
        /// All application data 
        /// </summary>
        /// <returns>All application list in JSON format</returns>
        public async Task<ActionResult> Get()
        {
            var applications = await ApplicationService.GetAllAsync();

            var data = new
            {
                data = applications
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}