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
    public class FinalBillPaymentController : BaseController
    {
        private readonly IFinalBillPaymentService _finalBillPaymentService;
        private readonly IProjectWorkService _projectWorkService;
        private readonly IBGBFundService _bgbFundService;
        private readonly IContractorCompanyPaymentService _contractorCompanyPaymentService;
        private readonly IBGBMiscellaneousFundService _bgbMiscellaneousFundService;
        private readonly IFurnitureBillPaymentService _furnitureBillPaymentService;
        private readonly ISignatoryAuthorityService _signatoryAuthorityService;
        private readonly IMapper _mapper;
        public FinalBillPaymentController(IActivityLogService activityLogService, IFinalBillPaymentService finalBillPaymentService, IProjectWorkService projectWorkService, IBGBFundService bgbFundService, IContractorCompanyPaymentService contractorCompanyPaymentService, IBGBMiscellaneousFundService bgbMiscellaneousFundService, IMapper mapper, IFurnitureBillPaymentService furnitureBillPaymentService, ISignatoryAuthorityService signatoryAuthorityService) : base(activityLogService)
        {
            _finalBillPaymentService = finalBillPaymentService;
            _projectWorkService = projectWorkService;
            _bgbFundService = bgbFundService;
            _contractorCompanyPaymentService = contractorCompanyPaymentService;
            _bgbMiscellaneousFundService = bgbMiscellaneousFundService;
            _mapper = mapper;
            _furnitureBillPaymentService = furnitureBillPaymentService;
            _signatoryAuthorityService = signatoryAuthorityService;
        }

        // GET: FinalBillPayment
        public ActionResult Index()
        {
            return View();
        }       
        public async Task<ActionResult> Create(int id)
        {
             var projectWork=await _projectWorkService.GetByIdAsync(id);
            var contractionCompanyPayment=await _contractorCompanyPaymentService.GetByProjectWorkIdAsync(projectWork.ProjectWorkId);
            var bgbMiscellaneousFund = await _bgbMiscellaneousFundService.GetByProjectWorkIdAsync(projectWork.ProjectWorkId);
            var finalBillPayment= await _finalBillPaymentService.GetByProjectWorkIdAsync(projectWork.ProjectWorkId);
            var furnitureBillPayment = await _furnitureBillPaymentService.GetByProjectWorkIdAsync(projectWork.ProjectWorkId);            
            var model = new FinalBillPaymentVm
            {
                ProjectWorkId = projectWork.ProjectWorkId,
                ProjectWorkName = projectWork.ProjectWorkTitleB,                
                PreviouslyPaidBillNo = contractionCompanyPayment.Count(),
                PreviouslyPaidAmount = contractionCompanyPayment.Sum(e => e.FinalPaymentAmount),
                CollateralPaidAmound = projectWork.EstimatedCost * 10/100,
                FurnitureBillPaymentAmount = furnitureBillPayment.PaymentAmount,
                DepositBGBFund = (bgbMiscellaneousFund.Sum(a => a.Amount) - contractionCompanyPayment.Sum(e => e.FinalPaymentAmount)),                
                BGBFundDropdown = await _bgbFundService.GetDropdownAsync(),
                HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(),
                ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(),
                SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(),
                BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync()

            };
            if(finalBillPayment != null)
            {
                model.FinalBillPaymentId = finalBillPayment.FinalBillPaymentId;
                model.NetAmountAsPerFinalMeasurement = finalBillPayment.NetAmountAsPerFinalMeasurement;
                model.LetterNo = finalBillPayment.LetterNo;
                model.VatTaxPer = finalBillPayment.VatTaxPer;
                model.VatTaxAmount = finalBillPayment.VatTaxAmount;
                model.ContractorDueAfterVatTaxDeduction = finalBillPayment.ContractorDueAfterVatTaxDeduction;
                model.PayableFinalBill = finalBillPayment.PayableFinalBill;
                model.NetFinalBill = finalBillPayment.NetFinalBill;
                model.NetFinalBillWordB = finalBillPayment.NetFinalBillWordB;
                model.PaidFromBGBFundId = finalBillPayment.PaidFromBGBFundId;
                model.RemainingDepositsInBgbFund = finalBillPayment.RemainingDepositsInBgbFund;
                model.RemainingDepositsInBgbFundWordB = finalBillPayment.RemainingDepositsInBgbFundWordB;
                model.DuePaidAmount = finalBillPayment.DuePaidAmount;
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
                    TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
                    model.BGBFundDropdown = await _bgbFundService.GetDropdownAsync(model.ProjectWorkId);
                    model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.HeadAssistantId);
                    model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.BranchClerkId);
                    model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.ConcernedEngineerId);
                    model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.SectionICId);
                    return View(model);
                }
                if (model.FinalBillPaymentId > 0)
                {
                    var updatedFinalPayment= await _finalBillPaymentService.UpdateAsync(_mapper.Map<FinalBillPayment>(model));

                    TempData["Message"] = Messages.Success(MessageType.Update.ToString());

                    var bgbFund = await _bgbFundService.GetByFinalBillPaymentIdAsync(model.ProjectWorkId);

                    if (bgbFund != null)
                    {                        
                        bgbFund.ProjectWorkId = updatedFinalPayment.ProjectWorkId;
                        bgbFund.AmountDeposited = updatedFinalPayment.RemainingDepositsInBgbFund;
                        bgbFund.PaidFromBGBFundId = updatedFinalPayment.PaidFromBGBFundId;
                        bgbFund.PaidAmount = updatedFinalPayment.DuePaidAmount;

                        await _bgbFundService.UpdateAsync(bgbFund);
                    }
                }
                else 
                {
                    var entity = _mapper.Map<FinalBillPayment>(model);
                    var final= await _finalBillPaymentService.CreateAsync(entity);
                    projectWork.IsFinalBillSubmitted = true;
                    await _projectWorkService.UpdateAsync(projectWork);
                    TempData["Message"] = Messages.Success(MessageType.Create.ToString());

                    // Ensure that you don't set BGBFundId explicitly
                    var bGBFund = new BGBFund
                    {                    
                        ProjectWorkId = final.ProjectWorkId,
                        AmountDeposited = final.RemainingDepositsInBgbFund,
                        PaidFromBGBFundId = final.PaidFromBGBFundId,
                        PaidAmount = final.DuePaidAmount
                    };
                    await _bgbFundService.CreateAsync(bGBFund);
                    TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                }
                return RedirectToAction("details/" + model.ProjectWorkId, "ProjectWork");
            }


            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), exception.Message);
                model.BGBFundDropdown = await _bgbFundService.GetDropdownAsync(model.ProjectWorkId);
                model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.HeadAssistantId);
                model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.BranchClerkId);
                model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.ConcernedEngineerId);
                model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.SectionICId);
                return View(model);
            }
        }
    }
}