using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.Setup;
using IDIMWorkBranchProject.Services.Setup;

namespace IDIMWorkBranchProject.Controllers.Setup
{
    public class QuarterController : Controller
    {
        protected IQuarterService QuarterService { get; set; }

        public QuarterController(IQuarterService quarterService)
        {
            QuarterService = quarterService;
        }

        public async Task<ActionResult> List()
        {
            var list = await QuarterService.GetAllAsync();

            return View(list);
        }

        public ActionResult Create()
        {
            var model = new QuarterVm
            {
                MonthFromDropdown = DateExtention.GetMonthDropdown(),
                MonthToDropdown = DateExtention.GetMonthDropdown()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(QuarterVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await QuarterService.InsertAsync(model);

                    ModelState.Clear();

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
            var model = await QuarterService.GetByIdAsync(id);
            model.MonthFromDropdown = DateExtention.GetMonthDropdown(model.MonthFrom);
            model.MonthToDropdown = DateExtention.GetMonthDropdown(model.MonthTo);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(QuarterVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await QuarterService.UpdateAsync(model);

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
            int quarterId = 0;
            int.TryParse(id, out quarterId);
            var model = await QuarterService.GetByIdAsync(quarterId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await QuarterService.DeleteAsync(id);
            }
            catch (Exception exception)
            {
                var model = await QuarterService.GetByIdAsync(id);
                ViewBag.Message = Messages.Failed(MessageType.Delete.ToString(), exception.Message);

                return View(model);
            }
            return RedirectToAction("List");
        }
    }
}
