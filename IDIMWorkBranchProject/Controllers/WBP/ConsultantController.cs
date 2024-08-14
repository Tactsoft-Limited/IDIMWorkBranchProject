using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.WBP;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.WBP;

namespace IDIMWorkBranchProject.Controllers.WBP
{
    public class ConsultantController : BaseController
    {
        protected IConsultantService ConsultantService { get; set; }
        public ConsultantController(IActivityLogService activityLogService, IConsultantService consultantService) : base(activityLogService)
        {
            ConsultantService = consultantService;
        }
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public async Task<ActionResult> List()
        {

            var model = new ConsultantSearchVm
            {
                Consultant = await ConsultantService.GetByAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> List(ConsultantSearchVm model)
        {
            model.Consultant = await ConsultantService.GetByAsync(model);

            return View(model);
        }

        public ActionResult Create()
        {
            var model = new ConsultantVm
            {
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ConsultantVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await ConsultantService.InsertAsync(model);

                    ModelState.Clear();
                    model = new ConsultantVm();
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
            var model = await ConsultantService.GetByIdAsync(id);

            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ConsultantVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await ConsultantService.UpdateAsync(model);

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
            if (id == null)
                return HttpNotFound();

            int consultantId;
            int.TryParse(id, out consultantId);

            var model = await ConsultantService.GetByIdAsync(consultantId);


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await ConsultantService.DeleteAsync(id);
            }
            catch (Exception exception)
            {
                var message = exception.Message;
                var sqlException = exception.GetBaseException() as SqlException;
                if (sqlException?.Number == 547)
                {
                    message = "Can not delete due to reference table.";
                }

                var model = await ConsultantService.GetByIdAsync(id);


                ViewBag.Message = Messages.Failed(MessageType.Delete.ToString(), message);

                return View(model);
            }

            return RedirectToAction("List");
        }
    }
}