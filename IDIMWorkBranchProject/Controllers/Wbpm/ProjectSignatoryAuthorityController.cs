using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Wbpm;
using System.Threading.Tasks;
using System;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class ProjectSignatoryAuthorityController : Controller
    {
        private IProjectSignatoryAuthorityService _projectSignatoryAuthorityService;
        private IADPProjectService _aDPProjectService;
        private ISignatoryAuthorityService _signatoryAuthorityService;
        private IMapper _mapper;

        public ProjectSignatoryAuthorityController(IProjectSignatoryAuthorityService projectSignatoryAuthorityService, IADPProjectService aDPProjectService, ISignatoryAuthorityService signatoryAuthorityService, IMapper mapper)
        {
            _projectSignatoryAuthorityService = projectSignatoryAuthorityService;
            _aDPProjectService = aDPProjectService;
            _signatoryAuthorityService = signatoryAuthorityService;
            _mapper = mapper;
        }

        // GET: ProjectSignatoryAuthority
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            var model = new ProjectSignatoryAuthoritySearchVm();
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> LoadData(ProjectSignatoryAuthoritySearchVm model)
        {
            try
            {
                var data = await _projectSignatoryAuthorityService.GetPagedAsync(model);
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        public async Task<ActionResult> Create(int id)
        {
            var aDPProject = await _aDPProjectService.GetByIdAsync(id);
            var projectSignatory = await _projectSignatoryAuthorityService.GetByAdpProjectIdAsync(aDPProject.ADPProjectId);

            var model = new ProjectSignatoryAuthorityVm
            {
                ADPProjectId = aDPProject.ADPProjectId,
                ADPProjectTitle = aDPProject.ProjectTitle,
                ProjectSignatoryAuthorityId = projectSignatory?.ProjectSignatoryAuthorityId ?? 0, // Null check for tec
            };

            // Populate dropdowns
            await PopulateDropdownsAsync(model, projectSignatory);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProjectSignatoryAuthorityVm model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
                    await PopulateDropdownsAsync(model, null); // Populate dropdowns with null check
                    return View(model);
                }

                var entity = _mapper.Map<ProjectSignatoryAuthority>(model);

                // Handle creation or update based on the TenderEvaluationCommitteeId
                if (model.ProjectSignatoryAuthorityId > 0)
                {
                    await _projectSignatoryAuthorityService.UpdateAsync(entity);
                    TempData["Message"] = Messages.Success(MessageType.Update.ToString());
                }
                else
                {
                    await _projectSignatoryAuthorityService.CreateAsync(entity);
                    TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                }

                // Populate dropdowns before redirect
                await PopulateDropdownsAsync(model, null);

                return RedirectToAction("details/" + model.ADPProjectId, "ADPProject");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), $"An error occurred while processing your request.{exception.InnerException.Message}");

                // Populate dropdowns even if an error occurs
                await PopulateDropdownsAsync(model, null);

                return View(model);
            }
        }

        private async Task PopulateDropdownsAsync(ProjectSignatoryAuthorityVm model, ProjectSignatoryAuthority projectSignatory)
        {
            if (projectSignatory != null)
            {
                model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(projectSignatory.HeadAssistant);
                model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(projectSignatory.ConcernedEngineer);
                model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(projectSignatory.SectionICT);
                model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(projectSignatory.BranchClerk);
            }
            else
            {
                model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.HeadAssistant);
                model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.ConcernedEngineer);
                model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.SectionICT);
                model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.BranchClerk);
            }
        }


    }
}