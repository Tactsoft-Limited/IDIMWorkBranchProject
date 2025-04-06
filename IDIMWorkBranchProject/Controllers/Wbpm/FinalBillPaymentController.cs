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
        private readonly IMapper _mapper;
        public FinalBillPaymentController(IActivityLogService activityLogService, IFinalBillPaymentService finalBillPaymentService, IProjectWorkService projectWorkService, IBGBFundService bgbFundService, IContractorCompanyPaymentService contractorCompanyPaymentService, IBGBMiscellaneousFundService bgbMiscellaneousFundService) : base(activityLogService)
        {
            _finalBillPaymentService = finalBillPaymentService;
            _projectWorkService = projectWorkService;
            _bgbFundService = bgbFundService;
            _contractorCompanyPaymentService = contractorCompanyPaymentService;
            _bgbMiscellaneousFundService = bgbMiscellaneousFundService;
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
            var model = new FinalBillPaymentVm
            {
                ProjectWorkId = projectWork.ProjectWorkId,
                ProjectWorkName = projectWork.ProjectWorkTitleB,
                PreviouslyPaidBillNo = contractionCompanyPayment.Count(),
                PreviouslyPaidAmount = contractionCompanyPayment.Sum(e => e.FinalPaymentAmount),
                DepositBGBFund = (bgbMiscellaneousFund.Sum(a => a.Amount) - contractionCompanyPayment.Sum(e => e.FinalPaymentAmount)),               
                BGBFundDropdown = await _bgbFundService.GetDropdownAsync(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FinalBillPaymentVm model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
                    model.BGBFundDropdown = await _bgbFundService.GetDropdownAsync(model.ProjectWorkId);
                    return View(model);
                }

                var entity = _mapper.Map<FinalBillPayment>(model);
                var final= await _finalBillPaymentService.CreateAsync(entity);
                TempData["Message"] = Messages.Success(MessageType.Create.ToString());

                var bGBFund = new BGBFund();
                bGBFund.ProjectWorkId = final.ProjectWorkId;
                bGBFund.AmountDeposited = final.RemainingDepositsInBgbFund;
                bGBFund.PaidFromProjectId = final.BGBFundId;
                bGBFund.PaidAmount = final.DuePaidAmount;

                await _bgbFundService.CreateAsync(bGBFund);
                TempData["Message"] = Messages.Success(MessageType.Create.ToString());

                return RedirectToAction("details/" + model.ProjectWorkId, "ProjectWork");
            }


            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), exception.Message);
                model.BGBFundDropdown = await _bgbFundService.GetDropdownAsync(model.ProjectWorkId);
                return View(model);
            }
        }
    }
}