using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.WBP;
using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.WBP;

namespace IDIMWorkBranchProject.Controllers.WBP
{
    public class ConsultancyActivitiesController : Controller
    {
        protected IConsultancyActivitiesService ConsultancyActivitiesService { get; set; }
        protected ISubProjectService SubProjectService { get; set; }
        protected IConsultantService ConsultantService { get; set; }

        public ConsultancyActivitiesController(
            IConsultancyActivitiesService consultancyActivitiesService,
            ISubProjectService subProjectService,
            IConsultantService consultantService
            )
        {
            ConsultancyActivitiesService = consultancyActivitiesService;
            SubProjectService = subProjectService;
            ConsultantService = consultantService;
        }

        public async Task<ActionResult> List()
        {

            var model = new ConsultancyActivitiesSearchVm
            {
                SubProjectDropdown = await SubProjectService.GetDropdownAsync(),
                ConsultantDropdown = await ConsultantService.GetDropdownAsync(),
                ConsultancyActivities = await ConsultancyActivitiesService.GetByAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> List(ConsultancyActivitiesSearchVm model)
        {
            model.SubProjectDropdown = await SubProjectService.GetDropdownAsync();
            model.ConsultantDropdown = await ConsultantService.GetDropdownAsync();
            model.ConsultancyActivities = await ConsultancyActivitiesService.GetByAsync(model);

            return View(model);
        }

        public async Task<ActionResult> Create()
        {
            var model = new ConsultancyActivitiesVm
            {
                SubProjectDropdown = await SubProjectService.GetDropdownAsync(),
                ConsultantDropdown = await ConsultantService.GetDropdownAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ConsultancyActivitiesVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await ConsultancyActivitiesService.InsertAsync(model);

                    ModelState.Clear();
                    model = new ConsultancyActivitiesVm();
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
            var model = await ConsultancyActivitiesService.GetByIdAsync(id);

            if (model == null)
                return HttpNotFound();
            model.SubProjectDropdown = await SubProjectService.GetDropdownAsync(model.SubProjectId);
            model.ConsultantDropdown = await ConsultantService.GetDropdownAsync(model.ConsultantId);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ConsultancyActivitiesVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await ConsultancyActivitiesService.UpdateAsync(model);

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

            var model = await ConsultancyActivitiesService.GetByIdAsync(subProjectDetailsId);


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await ConsultancyActivitiesService.DeleteAsync(id);
            }
            catch (Exception exception)
            {
                var message = exception.Message;
                var sqlException = exception.GetBaseException() as SqlException;
                if (sqlException?.Number == 547)
                {
                    message = "Can not delete due to reference table.";
                }

                var model = await ConsultancyActivitiesService.GetByIdAsync(id);


                ViewBag.Message = Messages.Failed(MessageType.Delete.ToString(), message);

                return View(model);
            }

            return RedirectToAction("List");
        }
    }
}