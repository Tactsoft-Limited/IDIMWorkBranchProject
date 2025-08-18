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
using System.Web;
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
        private readonly ISignatoryAuthorityService _signatoryAuthorityService;
        private readonly IReportService _reportService;
        private readonly IContractAgreementService _contractAgreementService;
        private readonly string fileStorePath = "Documents/ContractorCompanyPaymentDocuments";

        public ContractorCompanyPaymentController(IActivityLogService activityLogService, IContractorCompanyPaymentService contractorCompanyPaymentService, IMapper mapper, IProjectWorkService projectWorkService, IADPReceivePaymentService adpReceivePaymentService, IReportService reportService, IBGBMiscellaneousFundService bgbMiscellaneousFundService, ISignatoryAuthorityService signatoryAuthorityService, IContractAgreementService contractAgreementService) : base(activityLogService)
        {
            _contractorCompanyPaymentService = contractorCompanyPaymentService;
            _mapper = mapper;
            _projectWorkService = projectWorkService;
            _AdpReceivePaymentService = adpReceivePaymentService;
            _reportService = reportService;
            _bgbMiscellaneousFundService = bgbMiscellaneousFundService;
            _signatoryAuthorityService = signatoryAuthorityService;
            _contractAgreementService = contractAgreementService;
        }

        // GET: ContractorCompanyPayment
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new ContractorCompanyPaymentSearchVm();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LoadData(ContractorCompanyPaymentSearchVm model)
        {
            try
            {
                var data = await _contractorCompanyPaymentService.GetPagedAsync(model);
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
            var contractAgreement = await _contractAgreementService.GetByProjectWorkIdAsync(projectWork.ProjectWorkId);
            var companyPayment = await _contractorCompanyPaymentService.GetByAllProjectWorkAsync(projectWork.ProjectWorkId);
            var adprecievePayment = await _AdpReceivePaymentService.GetByProjectWorkIdAsync(projectWork.ProjectWorkId);
            var miscellaneousFund = await _bgbMiscellaneousFundService.GetByProjectWorkIdAsync(projectWork.ProjectWorkId);

            var model = new ContractorCompanyPaymentVm
            {

                ProjectWorkId = projectWork.ProjectWorkId,
                ProjectWorkTitle = projectWork.ProjectWorkTitleB,
                EstimatedCost = projectWork.EstimatedCost,
                ConstructionCompany = contractAgreement.ConstructionCompany.FirmNameB,
                TotalWithdrawFromMinistry = adprecievePayment.Sum(x => x.BillPaidAmount),
                TotalWithdrawPercent = adprecievePayment.Sum(x => x.BillPaidPer),
                PreviouslyTotalPaidNo = companyPayment.Count(),
                PreviouslyPaidAmount = companyPayment.Sum(x => x.FinalPaymentAmount),
                TotalDepositsInFund = miscellaneousFund.Sum(x => x.Amount) - companyPayment.Sum(x => x.FinalPaymentAmount),
                ProgressPer = adprecievePayment.Sum(x => x.BillPaidPer),
                ProgressAmount = adprecievePayment.Sum(x => x.BillPaidAmount),
                BillPaymentNumber = companyPayment.Count() + 1,
                HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(),
                ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(),
                SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(),
                BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(),
                OfficerDropdown = await _signatoryAuthorityService.GetDropdownAsync(),
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
                    SetResponseMessage(DefaultMsg.InvalidInput, ResponseType.Error);
                    model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.HeadAssistantId);
                    model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.BranchClerkId);
                    model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.ConcernedEngineerId);
                    model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.SectionICId);
                    model.OfficerDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.OfficerId);
                    return View(model);
                }

                var entity = _mapper.Map<ContractorCompanyPayment>(model);
                await _contractorCompanyPaymentService.CreateAsync(entity);
                SetResponseMessage(string.Format(DefaultMsg.SaveSuccess, "Contractor Company Payment"), ResponseType.Success);
                return RedirectToAction("Details", "ProjectWork", new { id = model.ProjectWorkId });
            }
            catch (Exception exception)
            {
                SetResponseMessage(string.Format(DefaultMsg.SaveFailed, "Contractor Company Payment", exception.Message), ResponseType.Error);
                model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.HeadAssistantId);
                model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.BranchClerkId);
                model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.ConcernedEngineerId);
                model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.SectionICId);
                model.OfficerDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.OfficerId);
                return View(model);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = _mapper.Map<ContractorCompanyPaymentVm>(await _contractorCompanyPaymentService.GetByIdAsync(id));
            var companyPayment = await _contractorCompanyPaymentService.GetByAllProjectWorkAsync(model.ProjectWorkId);
            var adprecievePayment = await _AdpReceivePaymentService.GetByProjectWorkIdAsync(model.ProjectWorkId);
            var projectWork = await _projectWorkService.GetByIdAsync(model.ProjectWorkId);
            model.ProjectWorkTitle = projectWork.ProjectWorkTitleB;
            model.EstimatedCost = projectWork.EstimatedCost;
            model.TotalWithdrawFromMinistry = adprecievePayment.Sum(x => x.BillPaidAmount);
            model.TotalWithdrawPercent = adprecievePayment.Sum(x => x.BillPaidPer);
            model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.HeadAssistantId);
            model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.BranchClerkId);
            model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.ConcernedEngineerId);
            model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.SectionICId);
            model.OfficerDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.OfficerId);            
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
                    SetResponseMessage(DefaultMsg.InvalidInput, ResponseType.Error);
                    model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.HeadAssistantId);
                    model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.BranchClerkId);
                    model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.ConcernedEngineerId);
                    model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.SectionICId);
                    model.OfficerDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.OfficerId);
                    return View(model);
                }
                string fileName = null;

                // Upload new file if provided
                if (model.DocumentFile != null && model.DocumentFile.ContentLength > 0)
                {
                    fileName = HandleFileUpload(model.DocumentFile, model.ContractorCompanyPaymentDocument);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        SetResponseMessage("File upload failed", ResponseType.Error);
                        return View(model);
                    }
                    model.ContractorCompanyPaymentDocument = fileName;
                }

                var entity = _mapper.Map<ContractorCompanyPayment>(model);
                await _contractorCompanyPaymentService.UpdateAsync(entity);
                SetResponseMessage(string.Format(DefaultMsg.UpdateSuccess, "Contractor Company Payment"), ResponseType.Success);
                return RedirectToAction("Details", "ProjectWork", new { id = model.ProjectWorkId });
            }
            catch (Exception exception)
            {
                SetResponseMessage(string.Format(DefaultMsg.UpdateFailed, "Contractor Company Payment", exception.Message), ResponseType.Error);
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
                await _contractorCompanyPaymentService.DeleteAsync(id);
                SetResponseMessage(string.Format(DefaultMsg.DeleteSuccess, "Contractor Company Payment"), ResponseType.Success);
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.DeleteFailed, "Contractor Company Payment", ex.Message), ResponseType.Error);
            }

            return RedirectToAction("List");
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


        public ActionResult PreviewDocument(string fileName)
        {
            var filePath = Path.Combine(Server.MapPath($"~/{fileStorePath}"), fileName);

            if (System.IO.File.Exists(filePath))
            {
                return File(filePath, "application/pdf");
            }

            return HttpNotFound();
        }

        private string HandleFileUpload(HttpPostedFileBase file, string existingFileName)
        {
            if (file == null || file.ContentLength <= 0)
                return null;

            if (!string.IsNullOrWhiteSpace(existingFileName))
                FileExtention.DeleteFile(existingFileName, fileStorePath);

            return FileExtention.UploadFile(file, fileStorePath);
        }

    }

}