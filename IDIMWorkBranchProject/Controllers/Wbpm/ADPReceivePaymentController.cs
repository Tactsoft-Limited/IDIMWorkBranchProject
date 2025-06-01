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
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class ADPReceivePaymentController : BaseController
    {
        private readonly IADPReceivePaymentService _aDPReceivePaymentService;
        private readonly IProjectWorkService _projectWorkService;
        private readonly IReportService _reportService;
        private readonly ISignatoryAuthorityService _signatoryAuthorityService;
        private readonly IContractAgreementService _contractAgreementService;
        private readonly IMapper _mapper;

        public ADPReceivePaymentController(IActivityLogService activityLogService, IADPReceivePaymentService aDPReceivePaymentService, IProjectWorkService projectWorkService, IReportService reportService, ISignatoryAuthorityService signatoryAuthorityService, IContractAgreementService contractAgreementService, IMapper mapper) : base(activityLogService)
        {
            _aDPReceivePaymentService = aDPReceivePaymentService;
            _projectWorkService = projectWorkService;
            _reportService = reportService;
            _signatoryAuthorityService = signatoryAuthorityService;
            _contractAgreementService = contractAgreementService;
            _mapper = mapper;
        }


        // GET: ADPReceivePayment
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new ADPReceivePaymentSearchVm();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LoadData(ADPReceivePaymentSearchVm model)
        {
            try
            {
                var data = await _aDPReceivePaymentService.GetPagedAsync(model);
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        public async Task<ActionResult> Create(int id)
        {
            var projectWork = await _projectWorkService.GetByIdAsync(id);
            var receivePayments = await _aDPReceivePaymentService.GetByProjectWorkIdAsync(id);
            var contractAgreement = await _contractAgreementService.GetByProjectWorkIdAsync(projectWork.ProjectWorkId);

            if (receivePayments.Sum(x => x.BillPaidPer) == 100 || receivePayments.Sum(x => x.BillPaidAmount) == projectWork.EstimatedCost)
                throw new Exception("Full Payment already received");

            var model = new ADPReceivePaymentVm
            {
                ProjectWorkId = projectWork.ProjectWorkId,
                EstimatedCost = projectWork.EstimatedCost,
                ProjectWorkTitle = projectWork.ProjectWorkTitle,
                ConstructionFirm = contractAgreement.ConstructionCompany.FirmNameB,
                BillNumber = receivePayments.Count() + 1,
                FinancialProgressPer = receivePayments.Sum(x => x.BillPaidPer),
                BillPaidPerTillDate = receivePayments.Sum(x => x.BillPaidPer),
                BillPaidAmountTillDate = receivePayments.Sum(x => x.BillPaidAmount),
                HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(),
                ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(),
                SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(),
                BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ADPReceivePaymentVm model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
                model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.HeadAssistantId);
                model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.BranchClerkId);
                model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.ConcernedEngineerId);
                model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.SectionICId);
                return View(model);
            }

            try
            {

                var entity = _mapper.Map<ADPReceivePayment>(model);
                var result = await _aDPReceivePaymentService.CreateAsync(entity);
                SetResponseMessage(string.Format(DefaultMsg.SaveSuccess, "Receive Payment"), ResponseType.Success);
                return RedirectToAction("Details", "ProjectWork", new { id = model.ProjectWorkId });
            }
            catch (Exception exception)
            {
                SetResponseMessage(string.Format(DefaultMsg.SaveFailed, "Receive Payment", exception.Message), ResponseType.Error);
                model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.HeadAssistantId);
                model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.BranchClerkId);
                model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.ConcernedEngineerId);
                model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.SectionICId);
                return View(model);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = _mapper.Map<ADPReceivePaymentVm>(await _aDPReceivePaymentService.GetByIdAsync(id));
            var projectWork = await _projectWorkService.GetByIdAsync(model.ProjectWorkId);
            model.ProjectWorkTitle = projectWork.ProjectWorkTitle;
            model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.HeadAssistantId);
            model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.BranchClerkId);
            model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.ConcernedEngineerId);
            model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.SectionICId);
            model.ConstructionFirm = projectWork.ContractAgreements.Where(x => x.ProjectWorkId == projectWork.ProjectWorkId).Select(x => x.ConstructionCompany.FirmName).FirstOrDefault();
            model.EstimatedCost = projectWork.EstimatedCost;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ADPReceivePaymentVm model)
        {
            if (!ModelState.IsValid)
            {
                SetResponseMessage(DefaultMsg.InvalidValue, ResponseType.Error);
                model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.HeadAssistantId);
                model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.BranchClerkId);
                model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.ConcernedEngineerId);
                model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.SectionICId);
                return View(model);
            }

            try
            {
                var entity = _mapper.Map<ADPReceivePayment>(model);
                var result = await _aDPReceivePaymentService.UpdateAsync(entity);
                SetResponseMessage(string.Format(DefaultMsg.UpdateSuccess, "Receive Payment"), ResponseType.Success);
                return RedirectToAction("Details", "ProjectWork", new { id = model.ProjectWorkId });
            }
            catch (Exception exception)
            {
                SetResponseMessage(string.Format(DefaultMsg.UpdateFailed, "Receive Payment", exception.Message), ResponseType.Error);
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
                await _aDPReceivePaymentService.DeleteAsync(id);
                SetResponseMessage(string.Format(DefaultMsg.DeleteSuccess, "Receive Payment"), ResponseType.Success);
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.DeleteFailed, "Receive Payment", ex.Message), ResponseType.Error);
            }
            return RedirectToAction("Index");
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