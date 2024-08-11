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
    public class ProjectController : Controller
    {
        protected IFiscalYearService FiscalYearService { get; set; }
        protected IGeneralInformationService GeneralInformationService { get; set; }
        protected IProjectService ProjectService { get; set; }
        protected IUnitService UnitService { get; set; }

        public ProjectController(
            IFiscalYearService fiscalYearService,
            IGeneralInformationService generalInformationService,
            IProjectService projectService,
            IUnitService unitService)
        {
            FiscalYearService = fiscalYearService;
            GeneralInformationService = generalInformationService;
            ProjectService = projectService;
            UnitService = unitService;
        }

        public async Task<ActionResult> List()
        {

            var model = new ProjectSearchVm
            {
                AuthorizeUnitDropdown = await UnitService.GetDropDownAsync(),
                FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(),
                Projects = await ProjectService.GetByAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> List(ProjectSearchVm model)
        {
            model.AuthorizeUnitDropdown = await UnitService.GetDropDownAsync(model.AuthorizeUnitId);
            model.FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(model.FiscalYearId);
            model.Projects = await ProjectService.GetByAsync(model);

            return View(model);
        }

        public async Task<ActionResult> Create()
        {
            var model = new ProjectVm
            {
                AuthorizeUnitDropdown = await UnitService.GetDropDownAsync(),
                FiscalYearDropdown = await FiscalYearService.GetDropdownAsync()
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
                    await ProjectService.InsertAsync(model);

                    ModelState.Clear();
                    model=new ProjectVm();
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

            model.AuthorizeUnitDropdown = await UnitService.GetDropDownAsync();
            model.FiscalYearDropdown = await FiscalYearService.GetDropdownAsync();
            ViewBag.Message = message;

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await ProjectService.GetByIdAsync(id);

            if (model == null)
                return HttpNotFound();

            model.AuthorizeUnitDropdown = await UnitService.GetDropDownAsync(model.AuthorizeUnitId);
            model.FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(model.FiscalYearId);

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
                    await ProjectService.UpdateAsync(model);

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

            model.AuthorizeUnitDropdown = await UnitService.GetDropDownAsync(model.AuthorizeUnitId);
            model.FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(model.FiscalYearId);
            ViewBag.Message = message;

            return View(model);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
                return HttpNotFound();

            int projectId;
            int.TryParse(id, out projectId);

            var model = await ProjectService.GetByIdAsync(projectId);


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await ProjectService.DeleteAsync(id);
            }
            catch (Exception exception)
            {
                var message = exception.Message;
                var sqlException = exception.GetBaseException() as SqlException;
                if (sqlException?.Number == 547)
                {
                    message = "Can not delete due to reference table.";
                }

                var model = await ProjectService.GetByIdAsync(id);


                ViewBag.Message = Messages.Failed(MessageType.Delete.ToString(), message);

                return View(model);
            }

            return RedirectToAction("List");
        }
    }
}