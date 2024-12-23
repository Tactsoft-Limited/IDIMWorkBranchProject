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
    public class ProjectController : BaseController
    {
        protected IFiscalYearService _fiscalYearService;
        protected IGeneralInformationService _generalInformationService;
        protected IProjectService _projectService;
        protected ISubProjectService _subProjectService;
        protected IUnitService _unitService;

        public ProjectController(IActivityLogService activityLogService, IFiscalYearService fiscalYearService, IGeneralInformationService generalInformationService, IProjectService projectService, IUnitService unitService) : base(activityLogService)
        {
            _fiscalYearService = fiscalYearService;
            _generalInformationService = generalInformationService;
            _projectService = projectService;
            _unitService = unitService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public async Task<ActionResult> List()
        {

            var model = new ProjectSearchVm
            {
                ProjectTypeDropdown = await _projectService.GetProductTypeDropdown(),
                FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LoadData(ProjectSearchVm model)
        {
            // Fetch filter parameters from the request

            try
            {
                var data = await _projectService.GetByAsync(model);

                // Return the JSON result
                return Json(data);
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during data fetching
                return Json(new { error = ex.Message });
            }
        }
        public async Task<ActionResult> Create()
        {
            var model = new ProjectVm
            {
                ProjectTypeDropdown = await _projectService.GetProductTypeDropdown(),
                FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProjectVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await _projectService.InsertAsync(model);

                    ModelState.Clear();
                    model = new ProjectVm();
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

            model.ProjectTypeDropdown = await _projectService.GetProductTypeDropdown();
            model.FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync();
            TempData["Message"] = message;

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await _projectService.GetByIdAsync(id);

            if (model == null)
                return HttpNotFound();

            model.ProjectTypeDropdown = await _projectService.GetProductTypeDropdown();
            model.FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync(model.FiscalYearId);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ProjectVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await _projectService.UpdateAsync(model);

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

            model.ProjectTypeDropdown = await _projectService.GetProductTypeDropdown();
            model.FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync(model.FiscalYearId);
            TempData["Message"] = message;

            return View(model);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
                return HttpNotFound();

            int projectId;
            int.TryParse(id, out projectId);

            var model = await _projectService.GetByIdAsync(projectId);


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _projectService.DeleteAsync(id);
            }
            catch (Exception exception)
            {
                var message = exception.Message;
                var sqlException = exception.GetBaseException() as SqlException;
                if (sqlException?.Number == 547)
                {
                    message = "Can not delete due to reference table.";
                }

                var model = await _projectService.GetByIdAsync(id);


                TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), message);

                return View(model);
            }

            return RedirectToAction("List");
        }
    }
}