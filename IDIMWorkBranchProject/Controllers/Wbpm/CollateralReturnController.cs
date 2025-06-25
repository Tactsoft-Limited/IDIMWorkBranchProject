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
    public class CollateralReturnController : BaseController
    {
        private readonly ICollateralReturnService _collateralReturnService;
        private readonly IProjectWorkService _projectWorkService;
        private readonly ISignatoryAuthorityService _signatoryAuthorityService;
        private readonly IMapper _mapper;
        public CollateralReturnController(IActivityLogService activityLogService, ICollateralReturnService collateralReturnService, IProjectWorkService projectWorkService, ISignatoryAuthorityService signatoryAuthorityService, IMapper mapper) : base(activityLogService)
        {
            _collateralReturnService = collateralReturnService;
            _projectWorkService = projectWorkService;
            _signatoryAuthorityService = signatoryAuthorityService;
            _mapper = mapper;
        }

        // GET: CollateralReturn
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Create(int id)
        {
            var projectWork = await _projectWorkService.GetByIdAsync(id);
            var collateralReturns = await _collateralReturnService.GetByProjectWorkIdAsync(projectWork.ProjectWorkId);


            var model = new CollateralReturnVm
            {
                ProjectWorkId = projectWork.ProjectWorkId,
                ProjectWorkName = projectWork.ProjectWorkTitleB,
                HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(),
                ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(),
                SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(),
                BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync()

            };
            if (collateralReturns != null)
            {
                model.CollateralReturnId = collateralReturns.CollateralReturnId;
                model.LetterNo = collateralReturns.LetterNo;
                model.QuoteOne = collateralReturns.QuoteOne;
                model.QuoteTwo = collateralReturns.QuoteTwo;
                model.QuoteThree = collateralReturns.QuoteThree;
                model.QuoteOneDate = collateralReturns.QuoteOneDate;
                model.QuoteTwoDate = collateralReturns.QuoteTwoDate;
                model.QuoteThreeDate = collateralReturns.QuoteThreeDate;

                model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(collateralReturns.HeadAssistantId);
                model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(collateralReturns.ConcernedEngineerId);
                model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(collateralReturns.BranchClerkId);
                model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(collateralReturns.SectionICId);

            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CollateralReturnVm model)
        {
            var projectWork = await _projectWorkService.GetByIdAsync(model.ProjectWorkId);
            try
            {
                if (!ModelState.IsValid)
                {
                    SetResponseMessage(DefaultMsg.InvalidInput, ResponseType.Error);
                    model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.HeadAssistantId);
                    model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.BranchClerkId);
                    model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.ConcernedEngineerId);
                    model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.SectionICId);
                    return View(model);
                }
                if (model.CollateralReturnId > 0)
                {
                    await _collateralReturnService.UpdateAsync(_mapper.Map<CollateralReturn>(model));
                    SetResponseMessage(string.Format(DefaultMsg.UpdateSuccess, "Collateral Return"), ResponseType.Success);
                }
                else
                {
                    var entity = _mapper.Map<CollateralReturn>(model);
                    await _collateralReturnService.CreateAsync(entity);
                    SetResponseMessage(string.Format(DefaultMsg.SaveSuccess, "Collateral Return"), ResponseType.Success);
                }
                return RedirectToAction("Details", "ProjectWork", new { id = model.ProjectWorkId });
            }

            catch (Exception exception)
            {
                SetResponseMessage(exception.Message, ResponseType.Error);
                model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.HeadAssistantId);
                model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.BranchClerkId);
                model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.ConcernedEngineerId);
                model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.SectionICId);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _collateralReturnService.DeleteAsync(id);
                SetResponseMessage(string.Format(DefaultMsg.DeleteSuccess, "Collateral Return"), ResponseType.Success);
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.DeleteFailed, "Collateral Return", ex.Message), ResponseType.Error);
            }
            return RedirectToAction("Index");
        }
    }
}