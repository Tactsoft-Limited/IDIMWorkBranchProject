using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class HandoverApprovedController : BaseController
    {
        private readonly IHandoverApprovedService _handoverApprovedService;
        private readonly IProjectWorkService _projectWorkService;
        private readonly ISignatoryAuthorityService _signatoryAuthorityService;
        private readonly IMapper _mapper;
        public HandoverApprovedController(IActivityLogService activityLogService, IHandoverApprovedService handoverApprovedService, IMapper mapper, IProjectWorkService projectWorkService, ISignatoryAuthorityService signatoryAuthorityService) : base(activityLogService)
        {
            _handoverApprovedService = handoverApprovedService;
            _mapper = mapper;
            _projectWorkService = projectWorkService;
            _signatoryAuthorityService = signatoryAuthorityService;
        }

        // GET: HandoverApproved
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Create(int id)
        {
            var projectWork = await _projectWorkService.GetByIdAsync(id);
            var handoverApproveds = await _handoverApprovedService.GetByProjectWorkIdAsync(projectWork.ProjectWorkId);


            var model = new HandoverApprovedVm
            {
                ProjectWorkId = projectWork.ProjectWorkId,
                ProjectWorkName = projectWork.ProjectWorkTitleB,
                HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(),
                ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(),
                SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(),
                BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync()

            };
            if (handoverApproveds != null)
            {
                model.HandoverApprovedId = handoverApproveds.HandoverApprovedId;
                model.LetterNo = handoverApproveds.LetterNo;
                model.QuoateOne = handoverApproveds.QuoateOne;
                model.QuoateTwo = handoverApproveds.QuoateTwo;                
                model.QuoateOneDate = handoverApproveds.QuoateOneDate;
                model.QuoateTwoDate = handoverApproveds.QuoateTwoDate;
                model.Description = handoverApproveds.Description;               

                model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(handoverApproveds.HeadAssistantId);
                model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(handoverApproveds.ConcernedEngineerId);
                model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(handoverApproveds.BranchClerkId);
                model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(handoverApproveds.SectionICId);

            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HandoverApprovedVm model)
        {
            var projectWork = await _projectWorkService.GetByIdAsync(model.ProjectWorkId);
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
                    model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.HeadAssistantId);
                    model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.BranchClerkId);
                    model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.ConcernedEngineerId);
                    model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.SectionICId);
                    return View(model);
                }
                if (model.HandoverApprovedId > 0)
                {
                    await _handoverApprovedService.UpdateAsync(_mapper.Map<HandoverApproved>(model));
                    TempData["Message"] = Messages.Success(MessageType.Update.ToString());

                }
                else
                {
                    var entity = _mapper.Map<HandoverApproved>(model);
                    await _handoverApprovedService.CreateAsync(entity);
                    TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                                    }
                return RedirectToAction("details/" + model.ProjectWorkId, "ProjectWork");
            }

            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), exception.Message);
                model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.HeadAssistantId);
                model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.BranchClerkId);
                model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.ConcernedEngineerId);
                model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.SectionICId);
                return View(model);
            }
        }
    }
}