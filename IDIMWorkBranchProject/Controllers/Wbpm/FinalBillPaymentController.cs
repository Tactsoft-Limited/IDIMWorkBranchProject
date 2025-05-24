using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class FinalBillPaymentController : BaseController
    {
        private readonly IFinalBillPaymentService _finalBillPaymentService;
        private readonly IProjectWorkService _projectWorkService;
        private readonly IBGBFundService _bgbFundService;
        private readonly IContractorCompanyPaymentService _contractorCompanyPaymentService;
        private readonly IBGBMiscellaneousFundService _bgbMiscellaneousFundService;
        private readonly IFurnitureBillPaymentService _furnitureBillPaymentService;
        private readonly ISignatoryAuthorityService _signatoryAuthorityService;
        private readonly IADPReceivePaymentService _aDPReceivePaymentService;
        private readonly IMapper _mapper;

        public FinalBillPaymentController(
            IActivityLogService activityLogService,
            IFinalBillPaymentService finalBillPaymentService,
            IProjectWorkService projectWorkService,
            IBGBFundService bgbFundService,
            IContractorCompanyPaymentService contractorCompanyPaymentService,
            IBGBMiscellaneousFundService bgbMiscellaneousFundService,
            IFurnitureBillPaymentService furnitureBillPaymentService,
            ISignatoryAuthorityService signatoryAuthorityService,
            IADPReceivePaymentService aDPReceivePaymentService,
            IMapper mapper) : base(activityLogService)
        {
            _finalBillPaymentService = finalBillPaymentService;
            _projectWorkService = projectWorkService;
            _bgbFundService = bgbFundService;
            _contractorCompanyPaymentService = contractorCompanyPaymentService;
            _bgbMiscellaneousFundService = bgbMiscellaneousFundService;
            _furnitureBillPaymentService = furnitureBillPaymentService;
            _signatoryAuthorityService = signatoryAuthorityService;
            _aDPReceivePaymentService = aDPReceivePaymentService;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            return View();
        }

        public async Task<ActionResult> Create(int id)
        {
            var projectWork = await _projectWorkService.GetByIdAsync(id);
            var finalBillPayment = await _finalBillPaymentService.GetByProjectWorkIdAsync(id);
            var companyPayments = await _contractorCompanyPaymentService.GetByAllProjectWorkAsync(id);
            var miscFunds = await _bgbMiscellaneousFundService.GetByProjectWorkIdAsync(id);
            var furnitureBill = await _furnitureBillPaymentService.GetByProjectWorkIdAsync(id);

            var model = new FinalBillPaymentVm
            {
                ProjectWorkId = id,
                ProjectWorkName = projectWork.ProjectWorkTitleB,
                EstimatedCost = projectWork.EstimatedCost,
                PreviouslyPaidBillNo = companyPayments.Count(),
                PreviouslyPaidAmount = companyPayments.Sum(x => x.FinalPaymentAmount),
                CollateralPaidAmound = projectWork.EstimatedCost * 10 / 100,
                FurnitureBillPaymentAmount = furnitureBill?.PaymentAmount ?? 0,
                DepositBGBFund = miscFunds.Sum(x => x.Amount) - companyPayments.Sum(x => x.FinalPaymentAmount),
                BGBFundDropdown = await _bgbFundService.GetDropdownAsync(),
                HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(),
                ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(),
                SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(),
                BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync()
            };

            if (finalBillPayment != null)
            {
                _mapper.Map(finalBillPayment, model);
                model.BGBFundDropdown = await _bgbFundService.GetDropdownAsync(finalBillPayment.PaidFromBGBFundId);
                model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(finalBillPayment.HeadAssistantId);
                model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(finalBillPayment.BranchClerkId);
                model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(finalBillPayment.ConcernedEngineerId);
                model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(finalBillPayment.SectionICId);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FinalBillPaymentVm model)
        {
            var projectWork = await _projectWorkService.GetByIdAsync(model.ProjectWorkId);
            try
            {
                if (!ModelState.IsValid)
                {
                    SetResponseMessage(DefaultMsg.InvalidInput, ResponseType.Error);
                    await PopulateDropdownsAsync(model);
                    return View(model);
                }

                if (model.FinalBillPaymentId > 0)
                {
                    var updated = await _finalBillPaymentService.UpdateAsync(_mapper.Map<FinalBillPayment>(model));
                    SetResponseMessage(DefaultMsg.UpdateSuccess, ResponseType.Success);

                    var bgbFund = await _bgbFundService.GetByFinalBillPaymentIdAsync(model.ProjectWorkId);
                    if (bgbFund != null)
                    {
                        bgbFund.ProjectWorkId = updated.ProjectWorkId;
                        bgbFund.AmountDeposited = updated.RemainingDepositsInBgbFund;
                        bgbFund.PaidFromBGBFundId = updated.PaidFromBGBFundId;
                        bgbFund.PaidAmount = updated.DuePaidAmount;

                        await _bgbFundService.UpdateAsync(bgbFund);
                    }
                }
                else
                {
                    var entity = _mapper.Map<FinalBillPayment>(model);
                    var created = await _finalBillPaymentService.CreateAsync(entity);

                    projectWork.IsFinalBillSubmitted = true;
                    await _projectWorkService.UpdateAsync(projectWork);

                    var bgbFund = new BGBFund
                    {
                        ProjectWorkId = created.ProjectWorkId,
                        AmountDeposited = created.RemainingDepositsInBgbFund,
                        PaidFromBGBFundId = created.PaidFromBGBFundId,
                        PaidAmount = created.DuePaidAmount
                    };
                    await _bgbFundService.CreateAsync(bgbFund);

                    SetResponseMessage(DefaultMsg.SaveSuccess, ResponseType.Success);
                }

                return RedirectToAction("Details", "ProjectWork", new { id = model.ProjectWorkId });
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.SaveFailed, "Final Bill Payment", ex.Message), ResponseType.Error);
                await PopulateDropdownsAsync(model);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _finalBillPaymentService.DeleteAsync(id);
                SetResponseMessage(string.Format(DefaultMsg.DeleteSuccess, "Final Bill Payment"), ResponseType.Success);
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.DeleteFailed, "Final Bill Payment", ex.Message), ResponseType.Error);
            }

            return RedirectToAction("List");
        }

        private async Task PopulateDropdownsAsync(FinalBillPaymentVm model)
        {
            model.BGBFundDropdown = await _bgbFundService.GetDropdownAsync(model.ProjectWorkId);
            model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.HeadAssistantId);
            model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.BranchClerkId);
            model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.ConcernedEngineerId);
            model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.SectionICId);
        }
    }
}
