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
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class ADPReceivePaymentController : BaseController
    {
        private readonly IADPReceivePaymentService _aDPReceivePaymentService;
        private readonly IProjectWorkService _projectWorkService;
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;
        public ADPReceivePaymentController(IActivityLogService activityLogService, IADPReceivePaymentService aDPReceivePaymentService, IMapper mapper, IProjectWorkService projectWorkService, IReportService reportService) : base(activityLogService)
        {
            _aDPReceivePaymentService = aDPReceivePaymentService;
            _mapper = mapper;
            _projectWorkService = projectWorkService;
            _reportService = reportService;
        }

        // GET: ADPReceivePayment
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Create(int id)
        {
            var projectWork = await _projectWorkService.GetByIdAsync(id);
            var receivePayments = await _aDPReceivePaymentService.GetByProjectWorkIdAsync(id);

            if (receivePayments.Sum(x => x.BillPaidPer) == 100 || receivePayments.Sum(x => x.BillPaidAmount) == projectWork.EstimatedCost)
                throw new Exception("Full Payment already received");

            var model = new ADPReceivePaymentVm
            {
                ProjectWorkId = projectWork.ProjectWorkId,
                EstimatedCost = projectWork.EstimatedCost,
                ProjectWorkTitle = projectWork.ProjectWorkTitle,
                ConstructionFirm = projectWork.ConstructionCompany.FirmNameB,
                BillNumber = receivePayments.Count() + 1,
                FinancialProgressPer = receivePayments.Sum(x => x.BillPaidPer),
                BillPaidPerTillDate = receivePayments.Sum(x => x.BillPaidPer),
                BillPaidAmountTillDate = receivePayments.Sum(x => x.BillPaidAmount),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ADPReceivePaymentVm model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
                    return View(model);
                }

                var entity = _mapper.Map<ADPReceivePayment>(model);
                await _aDPReceivePaymentService.CreateAsync(entity);
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
            var model = _mapper.Map<ADPReceivePaymentVm>(await _aDPReceivePaymentService.GetByIdAsync(id));
            var projectWork = await _projectWorkService.GetByIdAsync(model.ProjectWorkId);
            model.ProjectWorkTitle = projectWork.ProjectWorkTitle;
            model.ConstructionFirm = projectWork.ConstructionCompany.FirmName;
            model.EstimatedCost = projectWork.EstimatedCost;

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ADPReceivePaymentVm model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
                    return View(model);
                }

                var entity = _mapper.Map<ADPReceivePayment>(model);
                await _aDPReceivePaymentService.UpdateAsync(entity);
                TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                return RedirectToAction("details/" + model.ProjectWorkId, "ProjectWork");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), exception.Message);
                return View(model);
            }
        }


        public async Task<ActionResult> PrintADPReceivePayment(int id, string type)
        {
            try
            {
                var data = await _reportService.GetADPReceivePaymenAsync(id);

                var reportDataSource = new List<ReportDataSource>
                {
                    new ReportDataSource("DsADPReceivePayment", data)
                };

                var config = new ReportConfig
                {
                    ReportFilePath = Path.Combine(Server.MapPath("~/Report/rdlc"), "ADPReceivePaymentReport.rdlc"),
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