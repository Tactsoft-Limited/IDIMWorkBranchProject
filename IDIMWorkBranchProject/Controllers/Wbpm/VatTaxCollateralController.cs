using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Extentions.ReportHealper;
using IDIMWorkBranchProject.Extentions.ReportHelper;
using IDIMWorkBranchProject.Models;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Report;
using IDIMWorkBranchProject.Services.Wbpm;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class VatTaxCollateralController : BaseController
    {
        private readonly IVatTaxCollateralService _vatTaxCollateralService;
        private readonly IADPReceivePaymentService _aDPReceivePaymentService;
        private readonly IProjectWorkService _projectWorkService;
        private readonly IReportService _reportService;
        private readonly ISignatoryAuthorityService _signatoryAuthorityService; 
        private readonly IMapper _mapper;
        public VatTaxCollateralController(
            IActivityLogService activityLogService,
            IVatTaxCollateralService vatTaxCollateralService,
            IMapper mapper,
            IADPReceivePaymentService aDPReceivePaymentService,
            IProjectWorkService projectWorkService,
            IReportService reportService,
            ISignatoryAuthorityService signatoryAuthorityService) : base(activityLogService)
        {
            _vatTaxCollateralService = vatTaxCollateralService;
            _mapper = mapper;
            _aDPReceivePaymentService = aDPReceivePaymentService;
            _projectWorkService = projectWorkService;
            _reportService = reportService;
            _signatoryAuthorityService = signatoryAuthorityService;
        }

        // GET: VatTaxCollateral
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Create(int id)
        {
            var adpPayment = await _aDPReceivePaymentService.GetByIdAsync(id);
            var projectWork = await _projectWorkService.GetByIdAsync(adpPayment.ProjectWorkId);
            var data = await _vatTaxCollateralService.GetByADPPaymentReceiveIdAsync(adpPayment.ADPReceivePaymentId);

            var model = new VatTaxCollateralVm();
            model.ADPReceivePaymentId = adpPayment.ADPReceivePaymentId;
            model.ProjectWorkId = projectWork.ProjectWorkId;
            model.ProjectWorkTitle = projectWork.ProjectWorkTitleB;
            model.BillPaidAmount = adpPayment.BillPaidAmount;
            model.CurrentBillTotalBalance = adpPayment.BillPaidAmount;

            if (data != null)
            {
                model.VatTaxCollateralId = data.VatTaxCollateralId;
                model.TaxPer = data.TaxPer;
                model.TaxAmount = data.TaxAmount;
                model.VatPer = data.VatPer;
                model.VatAmount = data.VatAmount;
                model.CollateralPer = data.CollateralPer;
                model.CollateralAmount = data.CollateralAmount;
                model.NeetAmount = data.NeetAmount;
                model.NeetAmountInWord = data.NeetAmountInWord;
                model.TotalDeductionAmount = data.TotalDeductionAmount;
                model.AllocatedAmountLetterNo = data.AllocatedAmountLetterNo;
                model.AllocatedAmountTillNow = data.AllocatedAmountTillNow;
                model.VoucherNo = data.VoucherNo;
                model.CodeNo = data.CodeNo;
                model.BillSubmissionNo = data.BillSubmissionNo;
                model.BillSubmissionDate = data.BillSubmissionDate;
                model.LastBillAmount = data.LastBillAmount;
                model.TotalAmount = data.TotalAmount;
                model.LastBillTotalBalance = data.LastBillTotalBalance;
                model.NetTotalAmount = data.NetTotalAmount;
                model.ReducedAllocatedAmountLetterNo = data.ReducedAllocatedAmountLetterNo;
                model.ReducedAllocatedAmountTillNow = data.ReducedAllocatedAmountTillNow;
                model.RelatedWorkBillAmount = data.RelatedWorkBillAmount;
                model.VoucherNo = data.VoucherNo;
                model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.HeadAssistantId);
                model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.ConcernedEngineerId);
                model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.SectionICId);
                model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.BranchClerkId);
                model.OfficersDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.OfficerId);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VatTaxCollateralVm model)
        {
            if (!ModelState.IsValid)
            {
                SetResponseMessage(DefaultMsg.InvalidInput, ResponseType.Error);
                model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.HeadAssistantId);
                model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.ConcernedEngineerId);
                model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.SectionICId);
                model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.BranchClerkId);
                model.OfficersDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.OfficerId);

                return View(model);
            }
            try
            {
                if (model.VatTaxCollateralId > 0)
                {
                    await _vatTaxCollateralService.UpdateAsync(_mapper.Map<VatTaxCollateral>(model));
                    SetResponseMessage(string.Format(DefaultMsg.UpdateSuccess, "Vat/Tax"), ResponseType.Success);
                }
                else
                {
                    await _vatTaxCollateralService.CreateAsync(_mapper.Map<VatTaxCollateral>(model));
                    SetResponseMessage(string.Format(DefaultMsg.SaveSuccess, "Vat/Tax"), ResponseType.Success);
                }

                return RedirectToAction("Details", "ProjectWork", new { id = model.ProjectWorkId });
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.SaveFailed, "", ex.Message), ResponseType.Error);
                model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.HeadAssistantId);
                model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.ConcernedEngineerId);
                model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.SectionICId);
                model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.BranchClerkId);
                model.OfficersDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.OfficerId);
                return View(model);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _vatTaxCollateralService.DeleteAsync(id);
                SetResponseMessage(string.Format(DefaultMsg.DeleteSuccess, "Vat/Tax"), ResponseType.Success);
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.DeleteFailed, "Vat/Tax", ex.Message), ResponseType.Error);
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<ActionResult> PrintVatTaxReport(int id, string type)
        {
            try
            {
                var data = await _reportService.GetVatTaxAsync(id);

                if (data == null)
                    throw new Exception("Data Not Found! Please Create first");


                var reportDataSource = new List<ReportDataSource>
                {
                    new ReportDataSource("DsVatTaxReport", data)
                };

                var config = new ReportConfig
                {
                    ReportFilePath = Path.Combine(Server.MapPath("~/Report/rdlc"), "VatTaxReport.rdlc"),
                    ReportType = type,
                    DeviceInfo = new Extentions.ReportHelper.DeviceInfo(type).Portrait(),
                };

                return new ReportResult(config, reportDataSource);
            }
            catch (Exception exception)
            {
                // Log the exception if necessary
                // You can also throw a custom exception if you want
                throw new InvalidOperationException("An error occurred while generating the report.", exception);
            }
        }
    }
}