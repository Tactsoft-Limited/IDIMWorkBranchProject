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
    public class ProjectStatusController : Controller
    {
        protected IBillTypeService BillTypeService { get; set; }
        protected IProjectStatusService ProjectStatusService { get; set; }
        protected IFiscalYearService FiscalYearService { get; set; }
        protected ISubProjectService SubProjectService { get; set; }
        protected IUnitService UnitService { get; set; }

        public ProjectStatusController(
            IBillTypeService billTypeService,
            IProjectStatusService projectStatusService,
            IFiscalYearService fiscalYearService,
            ISubProjectService subProjectService,
            IUnitService unitService)
        {
            BillTypeService = billTypeService;
            ProjectStatusService = projectStatusService;
            FiscalYearService = fiscalYearService;
            SubProjectService = subProjectService;
            UnitService = unitService;
        }

        public async Task<ActionResult> List()
        {
            var model = new ProjectStatusSearchVm
            {
                ProjectStatus = await ProjectStatusService.GetByAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> List(ProjectStatusSearchVm model)
        {
            model.ProjectStatus = await ProjectStatusService.GetByAsync(model);

            return View(model);
        }

        public async Task<ActionResult> Create(int id)
        {
            var subproject = await SubProjectService.GetByIdAsync(id);
            if (subproject == null)
                return HttpNotFound();

            var model = new ProjectStatusVm
            {
                SubProjectId = subproject.SubProjectId,
                SubProjectTitle = subproject.SubProjectTitle
            };

            return View(model);
        }


        [HttpPost]
        public async Task<ActionResult> Create(ProjectStatusVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await ProjectStatusService.InsertAsync(model);

                    ModelState.Clear();
                    model = new ProjectStatusVm();
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
            var model = await ProjectStatusService.GetByIdAsync(id);

            if (model == null)
                return HttpNotFound();


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ProjectStatusVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await ProjectStatusService.UpdateAsync(model);

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

            int projectStatusId;
            int.TryParse(id, out projectStatusId);

            var model = await ProjectStatusService.GetByIdAsync(projectStatusId);


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await ProjectStatusService.DeleteAsync(id);
            }
            catch (Exception exception)
            {
                var message = exception.Message;
                var sqlException = exception.GetBaseException() as SqlException;
                if (sqlException?.Number == 547)
                {
                    message = "Can not delete due to reference table.";
                }

                var model = await ProjectStatusService.GetByIdAsync(id);


                ViewBag.Message = Messages.Failed(MessageType.Delete.ToString(), message);

                return View(model);
            }

            return RedirectToAction("List");
        }
    }
}
