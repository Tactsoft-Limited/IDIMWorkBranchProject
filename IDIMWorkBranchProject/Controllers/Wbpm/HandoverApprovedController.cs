using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class HandoverApprovedController : BaseController
    {
        private readonly IHandoverApprovedService _handoverApprovedService;
        private readonly IProjectWorkService _projectWorkService;
        private readonly ISignatoryAuthorityService _signatoryAuthorityService;
        private readonly IMapper _mapper;

        public HandoverApprovedController(
            IActivityLogService activityLogService,
            IHandoverApprovedService handoverApprovedService,
            IMapper mapper,
            IProjectWorkService projectWorkService,
            ISignatoryAuthorityService signatoryAuthorityService
        ) : base(activityLogService)
        {
            _handoverApprovedService = handoverApprovedService;
            _mapper = mapper;
            _projectWorkService = projectWorkService;
            _signatoryAuthorityService = signatoryAuthorityService;
        }

        public ActionResult Index() => View();

        public async Task<ActionResult> Create(int id)
        {
            var project = await _projectWorkService.GetByIdAsync(id);
            var existing = await _handoverApprovedService.GetByProjectWorkIdAsync(project.ProjectWorkId);

            var model = new HandoverApprovedVm
            {
                ProjectWorkId = project.ProjectWorkId,
                ProjectWorkName = project.ProjectWorkTitleB
            };

            if (existing != null)
            {
                _mapper.Map(existing, model);
                await PopulateSignatoryDropdowns(model, existing.HeadAssistantId, existing.ConcernedEngineerId, existing.BranchClerkId, existing.SectionICId);
            }
            else
            {
                await PopulateSignatoryDropdowns(model);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HandoverApprovedVm model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    SetResponseMessage(DefaultMsg.InvalidInput, ResponseType.Error);
                    await PopulateSignatoryDropdowns(model, model.HeadAssistantId, model.ConcernedEngineerId, model.BranchClerkId, model.SectionICId);
                    return View(model);
                }

                var entity = _mapper.Map<HandoverApproved>(model);

                if (model.HandoverApprovedId > 0)
                {
                    await _handoverApprovedService.UpdateAsync(entity);
                    SetResponseMessage(string.Format(DefaultMsg.UpdateSuccess, "Handover Approval"), ResponseType.Success);
                }
                else
                {
                    await _handoverApprovedService.CreateAsync(entity);
                    SetResponseMessage(string.Format(DefaultMsg.SaveSuccess, "Handover Approval"), ResponseType.Success);
                }

                return RedirectToAction("Details", "ProjectWork", new { id = model.ProjectWorkId });
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.SaveFailed, "Handover Approval", ex.Message), ResponseType.Error);
                await PopulateSignatoryDropdowns(model, model.HeadAssistantId, model.ConcernedEngineerId, model.BranchClerkId, model.SectionICId);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _handoverApprovedService.DeleteAsync(id);
                SetResponseMessage(string.Format(DefaultMsg.DeleteSuccess, "Handover Approval"), ResponseType.Success);
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.DeleteFailed, "Handover Approval", ex.Message), ResponseType.Error);
            }

            return RedirectToAction("List");
        }

        private async Task PopulateSignatoryDropdowns(HandoverApprovedVm model, int? headId = null, int? engineerId = null, int? clerkId = null, int? ictId = null)
        {
            model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(headId);
            model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(engineerId);
            model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(clerkId);
            model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(ictId);
        }
    }
}
