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
    public class SubProjectController : BaseController
    {
        protected IConstructionFirmService ConstructionFirmService { get; set; }
        protected IFiscalYearService FiscalYearService { get; set; }
        protected IGeneralInformationService GeneralInformationService { get; set; }
        protected IProjectService ProjectService { get; set; }
        protected ISubProjectService SubProjectService { get; set; }
        protected IUnitService UnitService { get; set; }
        public int ApplicationId { get; set; }
        public SubProjectController(IActivityLogService activityLogService, IConstructionFirmService constructionFirmService, IFiscalYearService fiscalYearService, IGeneralInformationService generalInformationService, IProjectService projectService, ISubProjectService subProjectService, IUnitService unitService, int applicationId) : base(activityLogService)
        {
            ConstructionFirmService = constructionFirmService;
            FiscalYearService = fiscalYearService;
            GeneralInformationService = generalInformationService;
            ProjectService = projectService;
            SubProjectService = subProjectService;
            UnitService = unitService;
            ApplicationId = applicationId;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public async Task<ActionResult> List()
        {
            var model = new SubProjectSearchVm
            {
                UnitDropdown = await UnitService.GetDropDownAsync(),
                ConstructionFirmDropdown = await ConstructionFirmService.GetDropdownAsync(),
                SubProjects = await SubProjectService.GetByAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> List(SubProjectSearchVm model)
        {
            model.UnitDropdown = await UnitService.GetDropDownAsync(model.UnitId);
            model.ConstructionFirmDropdown = await ConstructionFirmService.GetDropdownAsync(model.ConstructionFirmId);
            model.SubProjects = await SubProjectService.GetByAsync(model);

            return View(model);
        }

        public async Task<ActionResult> Create(int id)
        {
            var project = await ProjectService.GetByIdAsync(id);
            if (project == null)
                return HttpNotFound();

            var model = new SubProjectVm
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                UnitDropdown = await UnitService.GetDropDownAsync(),
                ConstructionFirmDropdown = await ConstructionFirmService.GetDropdownAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(SubProjectVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await SubProjectService.InsertAsync(model);

                    ModelState.Clear();
                    model = new SubProjectVm
                    {
                        ProjectId = model.ProjectId,
                        ProjectName = model.ProjectName
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

            model.UnitDropdown = await UnitService.GetDropDownAsync(model.UnitId);
            model.ConstructionFirmDropdown = await ConstructionFirmService.GetDropdownAsync(model.ConstructionFirmId);
            ViewBag.Message = message;

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await SubProjectService.GetByIdAsync(id);

            if (model == null)
                return HttpNotFound();

            model.UnitDropdown = await UnitService.GetDropDownAsync(model.UnitId);
            model.ConstructionFirmDropdown = await ConstructionFirmService.GetDropdownAsync(model.ConstructionFirmId);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(SubProjectVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await SubProjectService.UpdateAsync(model);

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

            model.UnitDropdown = await UnitService.GetDropDownAsync(model.UnitId);
            model.ConstructionFirmDropdown = await ConstructionFirmService.GetDropdownAsync(model.ConstructionFirmId);

            ViewBag.Message = message;

            return View(model);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
                return HttpNotFound();

            int projectId;
            int.TryParse(id, out projectId);

            var model = await SubProjectService.GetByIdAsync(projectId);


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await SubProjectService.DeleteAsync(id);
            }
            catch (Exception exception)
            {
                var message = exception.Message;
                var sqlException = exception.GetBaseException() as SqlException;
                if (sqlException?.Number == 547)
                {
                    message = "Can not delete due to reference table.";
                }

                var model = await SubProjectService.GetByIdAsync(id);


                ViewBag.Message = Messages.Failed(MessageType.Delete.ToString(), message);

                return View(model);
            }

            return RedirectToAction("List");
        }
    }
}
