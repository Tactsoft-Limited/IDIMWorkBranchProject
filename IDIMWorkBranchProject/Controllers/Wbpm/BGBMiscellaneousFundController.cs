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
    public class BGBMiscellaneousFundController : BaseController
    {
        private readonly IBGBMiscellaneousFundService _bGBMiscellaneousFundService;
        private readonly IADPReceivePaymentService _aDPReceivePaymentService;
        private readonly IProjectWorkService _projectWorkService;
        private readonly IConstructionCompanyService _constructionCompanyService;
        private readonly IVatTaxCollateralService _vatTaxCollateralService;
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;

        public BGBMiscellaneousFundController(IActivityLogService activityLogService,
            IBGBMiscellaneousFundService bGBMiscellaneousFundService,
            IMapper mapper,
            IADPReceivePaymentService aDPReceivePaymentService,
            IProjectWorkService projectWorkService,
            IConstructionCompanyService constructionCompanyService,
            IReportService reportService,
            IVatTaxCollateralService vatTaxCollateralService) : base(activityLogService)
        {
            _bGBMiscellaneousFundService = bGBMiscellaneousFundService;
            _mapper = mapper;
            _aDPReceivePaymentService = aDPReceivePaymentService;
            _projectWorkService = projectWorkService;
            _constructionCompanyService = constructionCompanyService;
            _reportService = reportService;
            _vatTaxCollateralService = vatTaxCollateralService;
        }

        // GET: BGBFund
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new BGBMiscellaneousFundSearchVm();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LoadData(BGBMiscellaneousFundSearchVm model)
        {
            try
            {
                var data = await _bGBMiscellaneousFundService.GetPagedAsync(model);
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }


        public async Task<ActionResult> CreateOrEdit(int id)
        {
            var model = new BGBMiscellaneousFundVm();
            var adpPayment = await _aDPReceivePaymentService.GetByIdAsync(id);
            // Retrieve the required data in parallel for efficiency
            var bgbFund = await _bGBMiscellaneousFundService.GetByADPPaymentReceiveIdAsync(adpPayment.ADPReceivePaymentId);
            var vatTax = await _vatTaxCollateralService.GetByADPPaymentReceiveIdAsync(adpPayment.ADPReceivePaymentId);
            var projectWork = await _projectWorkService.GetByIdAsync(adpPayment.ProjectWorkId);
            var data = await _bGBMiscellaneousFundService.GetByADPPaymentReceiveIdAsync(adpPayment.ADPReceivePaymentId);

            // Throw exception if vatTax is null
            if (vatTax == null)
            {
                throw new InvalidOperationException("VAT tax information is required before proceeding. Please create it first.");
            }

            // If the bgbFund is found, map values to the model
            if (bgbFund != null)
            {
                model.FundId = bgbFund.FundId;
                model.ADPReceivePaymentId = bgbFund.ADPReceivePaymentId;
                model.ProjectWorkId = bgbFund.ProjectWorkId;
                model.ProjectWorkTitle = bgbFund.ProjectWork.ProjectWorkTitle;
            }

            // Map additional values from other services
            model.ADPReceivePaymentId = adpPayment.ADPReceivePaymentId;
            model.ProjectWorkId = projectWork.ProjectWorkId;
            model.ProjectWorkTitle = projectWork.ProjectWorkTitleB;
            model.Amount = vatTax.NeetAmount;

            // If additional data exists, update the model with these values
            if (data != null)
            {
                model.FundId = data.FundId;
                model.LetterNo = data.LetterNo;
                model.DepositeDate = data.DepositeDate;
                model.PayOrderNo = data.PayOrderNo;
                model.PayOrderDate = data.PayOrderDate;
                model.AccountName = data.AccountName;
                model.AccountNumber = data.AccountNumber;
                model.BankName = data.BankName;
                model.BrunchName = data.BrunchName;
                model.Amount = data.Amount;
                model.Remarks = data.Remarks;
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateOrEdit(BGBMiscellaneousFundVm model)
        {
            try
            {
                if (model.FundId > 0)
                {
                    await _bGBMiscellaneousFundService.UpdateAsync(_mapper.Map<BGBMiscellaneousFund>(model));
                    SetResponseMessage(string.Format(DefaultMsg.UpdateSuccess, "Fund"), ResponseType.Success);
                }
                else
                {
                    await _bGBMiscellaneousFundService.CreateAsync(_mapper.Map<BGBMiscellaneousFund>(model));
                    SetResponseMessage(string.Format(DefaultMsg.SaveSuccess, "Fund"), ResponseType.Success);
                }

                return RedirectToAction("Details", "ProjectWork", new { id = model.ProjectWorkId });
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.SaveFailed, "Receive Payment", ex.Message), ResponseType.Error);
                return View(model);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _bGBMiscellaneousFundService.DeleteAsync(id);
                SetResponseMessage(string.Format(DefaultMsg.DeleteSuccess, "Fund"), ResponseType.Success);
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.DeleteFailed, "Fund", ex.Message), ResponseType.Error);
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> PrintBGBMiscellaneousFundReport(int id, string type)
        {
            try
            {
                var data = await _reportService.GetBGBMiscellaneousFundAsync(id);

                if (data == null)
                    throw new Exception("Data Not Found! Please Create first");


                var reportDataSource = new List<ReportDataSource>
                {
                    new ReportDataSource("DsBGBMiscellaneousFundReport", data)
                };

                var config = new ReportConfig
                {
                    ReportFilePath = Path.Combine(Server.MapPath("~/Report/rdlc"), "BGBMiscellaneousFundReport.rdlc"),
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