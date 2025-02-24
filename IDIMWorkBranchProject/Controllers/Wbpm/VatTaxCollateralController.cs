using AutoMapper;
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
    public class VatTaxCollateralController : BaseController
    {
        private readonly IVatTaxCollateralService _vatTaxCollateralService;
        private readonly IADPReceivePaymentService _aDPReceivePaymentService;
        private readonly IMapper _mapper;
        public VatTaxCollateralController(
            IActivityLogService activityLogService,
            IVatTaxCollateralService vatTaxCollateralService,
            IMapper mapper,
            IADPReceivePaymentService aDPReceivePaymentService) : base(activityLogService)
        {
            _vatTaxCollateralService = vatTaxCollateralService;
            _mapper = mapper;
            _aDPReceivePaymentService = aDPReceivePaymentService;
        }

        // GET: VatTaxCollateral
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Create(int id)
        {
            var data = await _vatTaxCollateralService.GetByIdAsync(id);
            var projectWork = await _aDPReceivePaymentService.GetByIdAsync(data.ADPReceivePaymentId);
            var model = new VatTaxCollateralVm();
            if (data != null)
            {
                model.VatTaxCollateralId = data.VatTaxCollateralId;
                model.ADPReceivePaymentId = projectWork.ADPReceivePaymentId;
                model.AllocatedAmountLetterNo = data.AllocatedAmountLetterNo;
                model.AllocatedAmountTillNow = data.AllocatedAmountTillNow;
                model.VoucherNo = data.VoucherNo;
                model.CodeNo = data.CodeNo;
                model.BillSubmissionNo = data.BillSubmissionNo;
                model.BillSubmissionDate = data.BillSubmissionDate;
                model.CurrentBillTotalBalance = data.CurrentBillTotalBalance;
                model.LastBillAmount = data.LastBillAmount;
                model.TotalAmount = data.TotalAmount;
                model.LastBillTotalBalance = data.LastBillTotalBalance;
                model.NetTotalAmount = data.NetTotalAmount;
                model.ReducedAllocatedAmountLetterNo= data.ReducedAllocatedAmountLetterNo;
                model.ReducedAllocatedAmountTillNow= data.ReducedAllocatedAmountTillNow;
                model.RelatedWorkBillAmount = data.RelatedWorkBillAmount;
                model.VoucherNo= data.VoucherNo;    
            }
            return View(model);
        }
    }
}