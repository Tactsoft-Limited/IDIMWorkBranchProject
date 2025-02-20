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
    public class ProjectSignatoryAuthorityController : BaseController
    {

        private IADPProjectService _aDPProjectService;
        private IProjectSignatoryAuthorityService _ProjectsignatoryAuthorityService;
        private ISignatoryAuthorityService _signatureAuthorityService;
        private IMapper _mapper;
        public ProjectSignatoryAuthorityController(IActivityLogService activityLogService, IADPProjectService aDPProjectService, ISignatoryAuthorityService signatoryAuthorityService, IMapper mapper, IProjectSignatoryAuthorityService projectsignatoryAuthorityService, ISignatoryAuthorityService signatureAuthorityService) : base(activityLogService)
        {
            _aDPProjectService = aDPProjectService;

            _mapper = mapper;
            _ProjectsignatoryAuthorityService = projectsignatoryAuthorityService;
            _signatureAuthorityService = signatureAuthorityService;
        }

        // GET: ProjectSignatoryAuthority
        public ActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> Create(int id)
        {
            var aDPProject = await _aDPProjectService.GetByIdAsync(id);
            var tec = await _ProjectsignatoryAuthorityService.GetByAdpProjectIdAsync(aDPProject.ADPProjectId);

            var model = new ProjectSignatoryAuthorityVm
            {
                ADPProjectId = aDPProject.ADPProjectId,
                ADPProjectTitle = aDPProject.ProjectTitle,
                ProjectSignatoryAuthorityId = tec?.ProjectSignatoryAuthorityId ?? 0, // Null check for tec
            };

            // Populate dropdowns
            await PopulateDropdownsAsync(model, tec);

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
                    await _ProjectsignatoryAuthorityService.UpdateAsync(entity);
                    TempData["Message"] = Messages.Success(MessageType.Update.ToString());
                }
                else
                {
                    await _ProjectsignatoryAuthorityService.CreateAsync(entity);
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

        private async Task PopulateDropdownsAsync(ProjectSignatoryAuthorityVm model, ProjectSignatoryAuthority tec)
        {
            if (tec != null)
            {
                model.HeadAssistantDropdown = await _signatureAuthorityService.GetDropdownAsync(tec.HeadAssistant);
                model.ConcernedEngineerDropdown = await _signatureAuthorityService.GetDropdownAsync(tec.ConcernedEngineer);
                model.SectionICTDropdown = await _signatureAuthorityService.GetDropdownAsync(tec.SectionICT);
                model.BranchClerkDropdown = await _signatureAuthorityService.GetDropdownAsync(tec.BranchClerk);
            }
            else
            {
                model.HeadAssistantDropdown = await _signatureAuthorityService.GetDropdownAsync(model.HeadAssistant);
                model.ConcernedEngineerDropdown = await _signatureAuthorityService.GetDropdownAsync(model.ConcernedEngineer);
                model.SectionICTDropdown = await _signatureAuthorityService.GetDropdownAsync(model.SectionICT);
                model.BranchClerkDropdown = await _signatureAuthorityService.GetDropdownAsync(model.BranchClerk);
            }
        }

    }
}