using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.WBP;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.WBP;

namespace IDIMWorkBranchProject.Controllers.WBP
{
    public class ProjectProblemController : BaseController
    {
        protected IBillTypeService BillTypeService { get; set; }
        protected IProjectProblemService ProjectProblemService { get; set; }
        protected IFiscalYearService FiscalYearService { get; set; }
        protected ISubProjectService SubProjectService { get; set; }
        protected IUnitService UnitService { get; set; }

        public ProjectProblemController(IActivityLogService activityLogService, IBillTypeService billTypeService, IProjectProblemService projectProblemService, IFiscalYearService fiscalYearService, ISubProjectService subProjectService, IUnitService unitService) : base(activityLogService)
        {
            BillTypeService = billTypeService;
            ProjectProblemService = projectProblemService;
            FiscalYearService = fiscalYearService;
            SubProjectService = subProjectService;
            UnitService = unitService;
        }
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public async Task<ActionResult> List()
        {
            var model = new ProjectProblemSearchVm
            {
                ProjectProblems = await ProjectProblemService.GetByAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> List(ProjectProblemSearchVm model)
        {
            model.ProjectProblems = await ProjectProblemService.GetByAsync(model);

            return View(model);
        }

        public async Task<ActionResult> Create(int id)
        {
            var subproject = await SubProjectService.GetByIdAsync(id);
            if (subproject == null)
                return HttpNotFound();

            var model = new ProjectProblemVm
            {
                SubProjectId = subproject.SubProjectId,
                SubProjectTitle = subproject.SubProjectTitle
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProjectProblemVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await ProjectProblemService.InsertAsync(model);

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

            TempData["Message"] = message;

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await ProjectProblemService.GetByIdAsync(id);

            if (model == null)
                return HttpNotFound();


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ProjectProblemVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await ProjectProblemService.UpdateAsync(model);

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
            if (id == null)
                return HttpNotFound();

            int projectProblemId;
            int.TryParse(id, out projectProblemId);

            var model = await ProjectProblemService.GetByIdAsync(projectProblemId);


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await ProjectProblemService.DeleteAsync(id);
            }
            catch (Exception exception)
            {
                var message = exception.Message;
                var sqlException = exception.GetBaseException() as SqlException;
                if (sqlException?.Number == 547)
                {
                    message = "Can not delete due to reference table.";
                }

                var model = await ProjectProblemService.GetByIdAsync(id);


                TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), message);

                return View(model);
            }

            return RedirectToAction("List");
        }
    }
}
