using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Extentions.ReportHealper;
using IDIMWorkBranchProject.Extentions.ReportHelper;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Report;
using IDIMWorkBranchProject.Services.Wbpm;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class ContractorCompanyPaymentController : BaseController
    {
        private readonly IContractorCompanyPaymentService _contractorCompanyPaymentService;
        private readonly IMapper _mapper;
        private readonly IProjectWorkService _projectWorkService;
        private readonly IADPReceivePaymentService _AdpReceivePaymentService;
        private readonly IBGBMiscellaneousFundService _bgbMiscellaneousFundService;
        private readonly IReportService _reportService;

        public ContractorCompanyPaymentController(IActivityLogService activityLogService, IContractorCompanyPaymentService contractorCompanyPaymentService, IMapper mapper, IProjectWorkService projectWorkService, IADPReceivePaymentService adpReceivePaymentService, IReportService reportService, IBGBMiscellaneousFundService bgbMiscellaneousFundService) : base(activityLogService)
        {
            _contractorCompanyPaymentService = contractorCompanyPaymentService;
            _mapper = mapper;
            _projectWorkService = projectWorkService;
            _AdpReceivePaymentService = adpReceivePaymentService;
            _reportService = reportService;
            _bgbMiscellaneousFundService = bgbMiscellaneousFundService;
        }

        // GET: ContractorCompanyPayment
        public ActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> Create(int id)
        {
            var projectWork = await _projectWorkService.GetByIdAsync(id);
            var companyPayment = await _contractorCompanyPaymentService.GetByProjectWorkIdAsync(projectWork.ProjectWorkId);
            var adprecievePayment = await _AdpReceivePaymentService.GetByProjectWorkIdAsync(projectWork.ProjectWorkId);
            var miscellaneousFund = await _bgbMiscellaneousFundService.GetByProjectWorkIdAsync(projectWork.ProjectWorkId);

            var model = new ContractorCompanyPaymentVm
            {
                ProjectWorkId = projectWork.ProjectWorkId,
                ProjectWorkTitle = projectWork.ProjectWorkTitleB,
                TotalWithdrawFromMinistry = adprecievePayment.Sum(x => x.BillPaidAmount),
                TotalWithdrawPercent = adprecievePayment.Sum(x => x.BillPaidPer),
                PreviouslyTotalPaidNo = companyPayment.Count(),
                PreviouslyPaidAmount = companyPayment.Sum(x => x.FinalPaymentAmount),
                TotalDepositsInFund = miscellaneousFund.Sum(x => x.Amount) - companyPayment.Sum(x => x.FinalPaymentAmount),
                WorkStarted = projectWork.WorkStartDate,
                WorkEnded = projectWork.WorkEndDate,
                EstimatedCost = projectWork.EstimatedCost,
                ConstructionCompany = projectWork.ConstructionCompany.FirmNameB,
                ProgressPer = adprecievePayment.Sum(x => x.BillPaidPer),
                ProgressAmount = adprecievePayment.Sum(x => x.BillPaidAmount),
                BillPaymentNumber = companyPayment.Count() + 1,

            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ContractorCompanyPaymentVm model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
                    return View(model);
                }

                var entity = _mapper.Map<ContractorCompanyPayment>(model);
                await _contractorCompanyPaymentService.CreateAsync(entity);
                TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                return RedirectToAction("details/" + model.ProjectWorkId, "ProjectWork");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), exception.Message);
                return View(model);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = _mapper.Map<ContractorCompanyPaymentVm>(await _contractorCompanyPaymentService.GetByIdAsync(id));
            var companyPayment = await _contractorCompanyPaymentService.GetByProjectWorkIdAsync(model.ProjectWorkId);
            var adprecievePayment = await _AdpReceivePaymentService.GetByProjectWorkIdAsync(model.ProjectWorkId);
            var projectWork = await _projectWorkService.GetByIdAsync(model.ProjectWorkId);
            model.ProjectWorkTitle = projectWork.ProjectWorkTitleB;
            model.EstimatedCost = projectWork.EstimatedCost;
            model.TotalWithdrawFromMinistry = adprecievePayment.Sum(x => x.BillPaidAmount);
            model.TotalWithdrawPercent = adprecievePayment.Sum(x => x.BillPaidPer);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ContractorCompanyPaymentVm model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
                    return View(model);
                }

                var entity = _mapper.Map<ContractorCompanyPayment>(model);
                await _contractorCompanyPaymentService.UpdateAsync(entity);
                TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                return RedirectToAction("details/" + model.ProjectWorkId, "ProjectWork");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), exception.Message);
                return View(model);
            }
        }


        public async Task<ActionResult> PrintContractorCompanyPaymentReport(int id, string type)
        {
            try
            {
                var data = await _reportService.GetContractorCompanyPaymentAsync(id);

                if (data == null)
                    throw new Exception("Data Not Found! Please Create first");


                var reportDataSource = new List<ReportDataSource>
                {
                    new ReportDataSource("DsContractorCompanyPaymentReport", data)
                };

                var config = new ReportConfig
                {
                    ReportFilePath = Path.Combine(Server.MapPath("~/Report/rdlc"), "ContractorCompanyPaymentReport.rdlc"),
                    ReportType = type,
                    DeviceInfo = new Extentions.ReportHelper.DeviceInfo(type).LegalPortrait(),
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