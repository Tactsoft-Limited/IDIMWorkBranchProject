using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class ProjectWorkStatusController : BaseController
    {
        private readonly IProjectWorkStatusService _projectWorkStatusService;
        private readonly IProjectWorkService _projectWorkService;
        private readonly IMapper _mapper;

        public ProjectWorkStatusController(IActivityLogService activityLogService, IProjectWorkStatusService projectWorkStatusService, IProjectWorkService projectWorkService, IMapper mapper) : base(activityLogService)
        {
            _projectWorkStatusService = projectWorkStatusService;
            _projectWorkService = projectWorkService;
            _mapper = mapper;
        }

        // GET: ProjectWorkStatus
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new ProjectWorkStatusSearchVm();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LoadData(ProjectWorkStatusSearchVm model)
        {
            try
            {
                var data = await _projectWorkStatusService.GetPagedAsync(model);
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        public async Task<ActionResult> Create(int id)
        {
            var projectWork = await _projectWorkService.GetByIdAsync(id);
            var projectWorkStatus = await _projectWorkStatusService.GetByProjectWorkIdAsync(id);

            var model = new ProjectWorkStatusVm
            {
                ProjectWorkId = projectWork.ProjectWorkId,
                ProjectWorkTitleB = projectWork.ProjectWorkTitleB,
            };
            if (projectWorkStatus != null)
            {
                model.ProjectWorkStatusId = projectWorkStatus.ProjectWorkStatusId;
                model.StatusTypeId = (StatusType?)projectWorkStatus.StatusTypeId;
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProjectWorkStatusVm model)
        {
            try
            {
                if (model.ProjectWorkStatusId > 0)
                {
                    await _projectWorkStatusService.UpdateAsync(_mapper.Map<ProjectWorkStatus>(model));
                    TempData["Message"] = Messages.Success(MessageType.Update.ToString());
                }
                else
                {

                    await _projectWorkStatusService.CreateAsync(_mapper.Map<ProjectWorkStatus>(model));
                    TempData["Message"] = Messages.Success(MessageType.Create.ToString());

                }
                return RedirectToAction(nameof(ProjectWorkController.Details), nameof(ProjectWork), new { id = model.ProjectWorkId });
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), $"An error occurred while processing your request.{exception.InnerException.Message}");
                return View(model);
            }

        }
    }
}