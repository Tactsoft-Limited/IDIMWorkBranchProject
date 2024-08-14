using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.Setup;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Setup;

namespace IDIMWorkBranchProject.Controllers.Setup
{
    public class FiscalYearController : BaseController
    {
        protected IFiscalYearService FiscalYearService { get; set; }
        public FiscalYearController(IActivityLogService activityLogService, IFiscalYearService fiscalYearService) : base(activityLogService)
        {
            FiscalYearService = fiscalYearService;
        }
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public async Task<ActionResult> List()
        {
            var list = await FiscalYearService.GetAllAsync();

            return View(list);
        }

        public ActionResult Create()
        {
            var model = new FiscalYearVm
            {
                StartDate = new DateTime(DateTime.Now.Year, 7, 1),
                EndDate = new DateTime(DateTime.Now.Year + 1, 6, 30)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FiscalYearVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await FiscalYearService.InsertAsync(model);

                    ModelState.Clear();

                    model = new FiscalYearVm
                    {
                        StartDate = new DateTime(DateTime.Now.Year, 7, 1),
                        EndDate = new DateTime(DateTime.Now.Year + 1, 6, 30)
                    };

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
            var model = await FiscalYearService.GetByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(FiscalYearVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await FiscalYearService.UpdateAsync(model);

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
            int fiscalYearId = 0;
            int.TryParse(id, out fiscalYearId);
            var model = await FiscalYearService.GetByIdAsync(fiscalYearId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await FiscalYearService.DeleteAsync(id);
            }
            catch (Exception exception)
            {
                var model = await FiscalYearService.GetByIdAsync(id);
                ViewBag.Message = Messages.Failed(MessageType.Delete.ToString(), exception.Message);

                return View(model);
            }
            return RedirectToAction("List");
        }
    }
}