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
        private readonly IReportService _reportService;

        public ContractorCompanyPaymentController(IActivityLogService activityLogService, IContractorCompanyPaymentService contractorCompanyPaymentService, IMapper mapper, IProjectWorkService projectWorkService, IADPReceivePaymentService adpReceivePaymentService, IReportService reportService) : base(activityLogService)
        {
            _contractorCompanyPaymentService = contractorCompanyPaymentService;
            _mapper = mapper;
            _projectWorkService = projectWorkService;
            _AdpReceivePaymentService = adpReceivePaymentService;
            _reportService = reportService;
        }

        // GET: ContractorCompanyPayment
        public ActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> Create(int id)
        {
            var projectWork = await _projectWorkService.GetByIdAsync(id);
            var adprecievePayment = await _AdpReceivePaymentService.GetByProjectWorkIdAsync(id);
           
            var model = new ContractorCompanyPaymentVm
            {
                ProjectWorkId = projectWork.ProjectWorkId,
                ProjectWorkTitle = projectWork.ProjectWorkTitle,
                
                EstimatedCost = projectWork.EstimatedCost,
                TotalWithdrawFromMinistry = adprecievePayment.Sum(x => x.BillPaidAmount),
                TotalWithdrawPercent = adprecievePayment.Sum(x => x.BillPaidPer),
                WorkStarted = projectWork.WorkStartDate,
                WorkEnded = projectWork.WorkEndDate,
                ConstructionCompany = projectWork.ConstructionCompany.FirmNameB,
                ProgressPer = adprecievePayment.Sum(x => x.BillPaidPer),
                ProgressAmount = adprecievePayment.Sum(x => x.BillPaidAmount),






            };
            return View(model);
        }


        [HttpPost]
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