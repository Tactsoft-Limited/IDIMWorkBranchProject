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
    public class ConsultancyFeesController : BaseController
    {
        protected IConsultancyFeesService ConsultancyFeesService { get; set; }
        protected ISubProjectService SubProjectService { get; set; }
        protected IConsultantService ConsultantService { get; set; }

        public ConsultancyFeesController(IActivityLogService activityLogService, IConsultancyFeesService consultancyFeesService, ISubProjectService subProjectService, IConsultantService consultantService) : base(activityLogService)
        {
            ConsultancyFeesService = consultancyFeesService;
            SubProjectService = subProjectService;
            ConsultantService = consultantService;
        }
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public async Task<ActionResult> List()
        {

            var model = new ConsultancyFeesSearchVm
            {
                SubProjectDropdown = await SubProjectService.GetDropdownAsync(),
                ConsultantDropdown = await ConsultantService.GetDropdownAsync(),
                ConsultancyFees = await ConsultancyFeesService.GetByAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> List(ConsultancyFeesSearchVm model)
        {
            model.SubProjectDropdown = await SubProjectService.GetDropdownAsync();
            model.ConsultantDropdown = await ConsultantService.GetDropdownAsync();
            model.ConsultancyFees = await ConsultancyFeesService.GetByAsync(model);

            return View(model);
        }

        public async Task<ActionResult> Create()
        {
            var model = new ConsultancyFeesVm
            {
                SubProjectDropdown = await SubProjectService.GetDropdownAsync(),
                ConsultantDropdown = await ConsultantService.GetDropdownAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ConsultancyFeesVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await ConsultancyFeesService.InsertAsync(model);

                    ModelState.Clear();
                    model = new ConsultancyFeesVm();
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
            model.SubProjectDropdown = await SubProjectService.GetDropdownAsync();
            model.ConsultantDropdown = await ConsultantService.GetDropdownAsync();
            ViewBag.Message = message;

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await ConsultancyFeesService.GetByIdAsync(id);

            if (model == null)
                return HttpNotFound();
            model.SubProjectDropdown = await SubProjectService.GetDropdownAsync(model.SubProjectId);
            model.ConsultantDropdown = await ConsultantService.GetDropdownAsync(model.ConsultantId);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ConsultancyFeesVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await ConsultancyFeesService.UpdateAsync(model);

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

            model.SubProjectDropdown = await SubProjectService.GetDropdownAsync(model.SubProjectId);
            model.ConsultantDropdown = await ConsultantService.GetDropdownAsync(model.ConsultantId);
            ViewBag.Message = message;

            return View(model);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
                return HttpNotFound();

            int subProjectDetailsId;
            int.TryParse(id, out subProjectDetailsId);

            var model = await ConsultancyFeesService.GetByIdAsync(subProjectDetailsId);


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await ConsultancyFeesService.DeleteAsync(id);
            }
            catch (Exception exception)
            {
                var message = exception.Message;
                var sqlException = exception.GetBaseException() as SqlException;
                if (sqlException?.Number == 547)
                {
                    message = "Can not delete due to reference table.";
                }

                var model = await ConsultancyFeesService.GetByIdAsync(id);


                ViewBag.Message = Messages.Failed(MessageType.Delete.ToString(), message);

                return View(model);
            }

            return RedirectToAction("List");
        }
    }
}