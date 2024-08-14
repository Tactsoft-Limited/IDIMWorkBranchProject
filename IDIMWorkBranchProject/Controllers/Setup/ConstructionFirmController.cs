using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.Setup;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Setup;

namespace IDIMWorkBranchProject.Controllers.Setup
{
    public class ConstructionFirmController : BaseController
    {
        protected IConstructionFirmService ConstructionFirmService { get; set; }
        public ConstructionFirmController(IActivityLogService activityLogService, IConstructionFirmService constructionFirmService) : base(activityLogService)
        {
            ConstructionFirmService = constructionFirmService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public async Task<ActionResult> List()
        {
            var list = await ConstructionFirmService.GetAllAsync();

            return View(list);
        }

        public ActionResult Create()
        {
            var model = new ConstructionFirmVm();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ConstructionFirmVm model)
        {
            Message message;
            try
            {
                if (ModelState.IsValid)
                {
                    await ConstructionFirmService.InsertAsync(model);

                    ModelState.Clear();
                    model = new ConstructionFirmVm();
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
            var model = await ConstructionFirmService.GetByIdAsync(id);

            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ConstructionFirmVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await ConstructionFirmService.UpdateAsync(model);

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

        public async Task<ActionResult> Delete(string id)
        {
            int supplierId;
            int.TryParse(id, out supplierId);

            var model = await ConstructionFirmService.GetByIdAsync(supplierId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await ConstructionFirmService.DeleteAsync(id);
            }
            catch (Exception exception)
            {
                var model = await ConstructionFirmService.GetByIdAsync(id);

                ViewBag.Message = Messages.Failed(MessageType.Delete.ToString(), exception.Message);

                return View(model);
            }

            return RedirectToAction("List");
        }

        /// <summary>
        /// get all json data for supplier
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Get(string search)
        {
            var suppliers = await ConstructionFirmService.GetAllAsync();
            var data = new
            {
                data = suppliers
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
