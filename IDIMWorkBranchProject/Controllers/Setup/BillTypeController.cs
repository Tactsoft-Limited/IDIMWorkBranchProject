using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.Setup;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Setup;

namespace IDIMWorkBranchProject.Controllers.Setup
{
    public class BillTypeController : BaseController
    {
        protected IBillTypeService BillTypeService { get; set; }
        public BillTypeController(IActivityLogService activityLogService, IBillTypeService billTypeService) : base(activityLogService)
        {
            BillTypeService = billTypeService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public async Task<ActionResult> List()
        {
            var list = await BillTypeService.GetAllAsync();

            return View(list);
        }

        public ActionResult Create()
        {
            var model = new BillTypeVm();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BillTypeVm model)
        {
            Message message;
            try
            {
                if (ModelState.IsValid)
                {
                    await BillTypeService.InsertAsync(model);

                    ModelState.Clear();
                    message = Messages.Success(MessageType.Create.ToString());
                    model = new BillTypeVm();
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

            TempData["Message"] = message;

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await BillTypeService.GetByIdAsync(id);

            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BillTypeVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await BillTypeService.UpdateAsync(model);

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

            TempData["Message"] = message;

            return View(model);
        }

        public async Task<ActionResult> Delete(string id)
        {
            int.TryParse(id, out int productTypeId);
            var model = await BillTypeService.GetByIdAsync(productTypeId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await BillTypeService.DeleteAsync(id);
            }
            catch (Exception exception)
            {
                var model = await BillTypeService.GetByIdAsync(id);

                TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), exception.Message);

                return View(model);
            }

            return RedirectToAction("List");
        }

        /// <summary>
        /// get all json data for productType
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Get()
        {
            var productTypes = await BillTypeService.GetAllAsync();
            var data = new
            {
                data = productTypes
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}