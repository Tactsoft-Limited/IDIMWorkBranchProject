using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class ProjectWorkController : BaseController
    {
        private readonly IProjectWorkService _projectWorkService;
        protected readonly IADPProjectService _ADPProjectService;
        protected readonly IADPReceivePaymentService _ADPReceivePaymentService;
        protected readonly IContractorCompanyPaymentService _contractorCompanyPaymentService;
        protected readonly INohaService _nohaService;
        protected readonly IPerformanceSecurityService _performanceSecurityService;
        protected readonly IContractAgreementService _contractAgreementService;
        protected readonly IWorkOrderService _workOrderService;
        protected readonly IProjectWorkStatusService _projectWorkStatusService;
        protected readonly IFinalBillPaymentService _finalBillPaymentService;
        private readonly IFurnitureBillPaymentService _furnitureBillPaymentService;
        private readonly ICollateralReturnService _collateralReturnService;
        private readonly IMapper _mapper;

        public ProjectWorkController(IActivityLogService activityLogService, IProjectWorkService projectWorkService,
            IMapper mapper, IADPProjectService aDPProjectService, IADPReceivePaymentService aDPReceivePaymentService,
            IContractorCompanyPaymentService contractorCompanyPaymentService, INohaService nohaService,
            IPerformanceSecurityService performanceSecurityService, IContractAgreementService contractAgreementService,
            IWorkOrderService workOrderService, IProjectWorkStatusService projectWorkStatusService, IFinalBillPaymentService finalBillPaymentService, IFurnitureBillPaymentService furnitureBillPaymentService, ICollateralReturnService collateralReturnService) : base(activityLogService)
        {
            _projectWorkService = projectWorkService;
            _mapper = mapper;
            _ADPProjectService = aDPProjectService;
            _ADPReceivePaymentService = aDPReceivePaymentService;
            _contractorCompanyPaymentService = contractorCompanyPaymentService;
            _nohaService = nohaService;
            _performanceSecurityService = performanceSecurityService;
            _contractAgreementService = contractAgreementService;
            _workOrderService = workOrderService;
            _projectWorkStatusService = projectWorkStatusService;
            _finalBillPaymentService = finalBillPaymentService;
            _furnitureBillPaymentService = furnitureBillPaymentService;
            _collateralReturnService = collateralReturnService;
        }
        public ActionResult Index()
        {

            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            var model = new ProjectWorkSearchVm();
            return View(model);
        }
        public async Task<ActionResult> LoadData(ProjectWorkSearchVm model)
        {
            try
            {
                var data = await _projectWorkService.GetPagedAsync(model);
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }


        public async Task<ActionResult> Details(int id)
        {
            var projectWorks = await _projectWorkService.GetByIdAsync(id);

            // Mapping with null checks
            var noha = _mapper.Map<NohaVm>(await _nohaService.GetByProjectWorkIdAsync(id));
            var performanceSecurity = _mapper.Map<PerformanceSecurityVm>(await _performanceSecurityService.GetByProjectWorkIdAsync(id));
            var contractAgreement = _mapper.Map<ContractAgreementVm>(await _contractAgreementService.GetByProjectWorkIdAsync(id));
            var workOrder = _mapper.Map<WorkOrderVm>(await _workOrderService.GetByProjectWorkIdAsync(id));
            var projectWorkStatus = _mapper.Map<ProjectWorkStatusVm>(await _projectWorkStatusService.GetByProjectWorkIdAsync(id));
            var collateralReturn = _mapper.Map<CollateralReturnVm>(await _collateralReturnService.GetByProjectWorkIdAsync(id));

            // Building the model
            var model = new ProjectWorkDetailsVm
            {
                ProjectWorkId = projectWorks.ProjectWorkId,
                ProjectTitle = projectWorks.ADPProject.ProjectTitle,
                ProjectWorkTitle = projectWorks.ProjectWorkTitle,
                ProjectWorkTitleB = projectWorks.ProjectWorkTitleB,
                EstimatedCost = projectWorks.EstimatedCost,
                EstimatedCostInWord = projectWorks.EstimatedCostInWord,
                EstimatedCostInWordBangla = projectWorks.EstimatedCostInWordBangla,
                Remarks = projectWorks.Remarks,
                IsNoahCompleted = projectWorks.IsNoahCompleted,
                NohaLetterNo = noha?.LetterNo,
                NohaDate = noha?.NohaDate, // Null-safe access
                NohaDocument = noha?.ScanDocument,
                IsPerformanceSecuritySubmited = projectWorks.IsPerformanceSecuritySubmited,
                SubmissionDate = performanceSecurity?.SubmissionDate,
                ExpiryDate = performanceSecurity?.ExpiryDate, // Null-safe access
                PerformanceSecurityDocument = performanceSecurity?.ScanDocument,
                IsAgreementCompleted = projectWorks.IsAgreementCompleted,
                FirmNameB = contractAgreement?.ConstructionFirm, // Null-safe access
                AgreementDate = contractAgreement?.AgreementDate, // Null-safe access
                AgreementDocument = contractAgreement?.ScanDocument,
                IsWorkOrderCompleted = projectWorks.IsWorkOrderCompleted,
                ProjectWorkStatus = projectWorkStatus?.ProjectWorkStatusId, // Null-safe access
                IsFinalBillSubmitted = projectWorks.IsFinalBillSubmitted,
                IsFurnitureIncluded = projectWorks.IsFurnitureIncluded,
                CollateralLetterNo = collateralReturn?.LetterNo,


                WorkOrderList = _mapper.Map<List<WorkOrderVm>>(await _workOrderService.GetAllByProjectWorkIdAsync(id)),
                ADPReceivePayments = _mapper.Map<List<ADPReceivePaymentVm>>(await _ADPReceivePaymentService.GetByProjectWorkIdAsync(id)),
                ContractorCompanyPayments = _mapper.Map<List<ContractorCompanyPaymentVm>>(await _contractorCompanyPaymentService.GetByAllProjectWorkAsync(id)),
                FinalBillPayments = _mapper.Map<List<FinalBillPaymentVm>>(await _finalBillPaymentService.GetAllByProjectWorkIdAsync(id)),
                FurnnitureBillPayments = _mapper.Map<List<FurnitureBillPaymentVm>>(await _furnitureBillPaymentService.GetAllByProjectWorkIdAsync(id)),

            };

            return View(model);
        }



        public async Task<ActionResult> Create(int id)
        {
            var data = await _ADPProjectService.GetByIdAsync(id);
            var model = new ProjectWorkVm
            {
                ADPProjectId = data.ADPProjectId,
                ProjectTitle = data.ProjectTitle
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProjectWorkVm model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
                return View(model);
            }

            try
            {
                // Map and save ProjectWork entity
                var entity = _mapper.Map<ProjectWork>(model);
                var result = await _projectWorkService.CreateAsync(entity);

                // Create and save ProjectWorkStatus
                var workStatus = new ProjectWorkStatusVm
                {
                    ProjectWorkId = result.ProjectWorkId,
                    StatusTypeId = StatusType.InProcess
                };
                await _projectWorkStatusService.CreateAsync(_mapper.Map<ProjectWorkStatus>(workStatus));

                // Success message and redirect
                TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                return RedirectToAction(nameof(ADPProjectController.Details), nameof(ADPProject), new { id = model.ADPProjectId });
            }
            catch (DbUpdateException dbEx)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), "Database error: " + dbEx.Message);
            }
            catch (InvalidOperationException ioEx)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), "Invalid operation: " + ioEx.Message);
            }
            catch (Exception ex)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), ex.Message);
            }

            return View(model);
        }


        public async Task<ActionResult> Edit(int id)
        {

            var model = _mapper.Map<ProjectWorkVm>(await _projectWorkService.GetByIdAsync(id));
            model.ProjectTitle = await _ADPProjectService.GetAdpProjectTitle(model.ADPProjectId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProjectWorkVm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = _mapper.Map<ProjectWork>(model);
                    await _projectWorkService.UpdateAsync(entity);
                    TempData["Message"] = Messages.Success(MessageType.Update.ToString());
                    return RedirectToAction("details/" + model.ADPProjectId, "ADPProject");  // Reset model after success
                }

                TempData["Message"] = Messages.InvalidInput(MessageType.Update.ToString());
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Update.ToString(), exception.Message);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _projectWorkService.GetByIdAsync(id);

            if (entity == null)
            {
                TempData["Message"] = "The requested record was not found.";
                return RedirectToAction("details/" + entity.ADPProjectId, "ADPProject");
            }

            var model = _mapper.Map<ProjectWorkVm>(entity);
            model.ProjectTitle = await _ADPProjectService.GetAdpProjectTitle(entity.ADPProjectId);
            return View(model); // Load the delete confirmation view
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(ProjectWorkVm model)
        {
            var entity = await _projectWorkService.GetByIdAsync(model.ProjectWorkId);
            try
            {

                if (entity == null)
                {
                    TempData["Message"] = "Record Not Found";
                    return RedirectToAction("Details/" + entity.ADPProjectId, "ADPProject");
                }

                await _projectWorkService.DeleteAsync(entity);

                TempData["Message"] = Messages.Success(MessageType.Delete.ToString());
                return RedirectToAction("Details/" + entity.ADPProjectId, "ADPProject");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), exception.InnerException?.Message);
                return RedirectToAction("Details/" + entity.ADPProjectId, "ADPProject"); // Avoids null reference
            }
        }

    }
}