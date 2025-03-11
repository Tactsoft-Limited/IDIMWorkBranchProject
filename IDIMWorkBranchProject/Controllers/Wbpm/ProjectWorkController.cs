using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.Collections.Generic;
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
        private readonly IMapper _mapper;

        public ProjectWorkController(IActivityLogService activityLogService, IProjectWorkService projectWorkService,
            IMapper mapper, IADPProjectService aDPProjectService, IADPReceivePaymentService aDPReceivePaymentService,
            IContractorCompanyPaymentService contractorCompanyPaymentService, INohaService nohaService,
            IPerformanceSecurityService performanceSecurityService, IContractAgreementService contractAgreementService,
            IWorkOrderService workOrderService, IProjectWorkStatusService projectWorkStatusService) : base(activityLogService)
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

            // Building the model
            var model = new ProjectWorkDetailsVm
            {
                ProjectWorkId=projectWorks.ProjectWorkId,
                ProjectTitle = projectWorks.ADPProject.ProjectTitle,
                ProjectWorkTitle = projectWorks.ProjectWorkTitle,
                ProjectWorkTitleB = projectWorks.ProjectWorkTitleB,
                EstimatedCost = projectWorks.EstimatedCost,
                EstimatedCostInWord = projectWorks.EstimatedCostInWord,
                Remarks = projectWorks.Remarks,
                FirmNameB = contractAgreement?.ConstructionFirm, // Null-safe access
                IsNoahCompleted = projectWorks.IsNoahCompleted,
                NohaDate = noha?.NohaDate, // Null-safe access
                IsPerformanceSecuritySubmited = projectWorks.IsPerformanceSecuritySubmited,
                ExpiryDate = performanceSecurity?.ExpiryDate, // Null-safe access
                IsAgreementCompleted = projectWorks.IsAgreementCompleted,
                AgreementDate = contractAgreement?.AgreementDate, // Null-safe access
                IsWorkOrderCompleted = projectWorks.IsWorkOrderCompleted,
                WorkOrderDate = workOrder?.WorkOrderDate, // Null-safe access
                StartDate = workOrder?.StartDate, // Null-safe access
                EndDate = workOrder?.EndDate, // Null-safe access
                ProjectWorkStatus = projectWorkStatus?.ProjectWorkStatusId, // Null-safe access

                ADPReceivePayments = _mapper.Map<List<ADPReceivePaymentVm>>(await _ADPReceivePaymentService.GetByProjectWorkIdAsync(id)),
                ContractorCompanyPayments = _mapper.Map<List<ContractorCompanyPaymentVm>>(await _contractorCompanyPaymentService.GetByProjectWorkIdAsync(id))

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
            try
            {
                if (ModelState.IsValid)
                {
                    // Step 2: Map the model to the entity
                    var entity = _mapper.Map<ProjectWork>(model);

                    // Step 3: Attempt to save the entity to the database
                    await _projectWorkService.CreateAsync(entity);

                    // Show success message and reset the form
                    TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                    return RedirectToAction("details/" + model.ADPProjectId, "ADPProject");  // Reset model after success
                }

                // If the model state is not valid
                TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
            }
            catch (Exception exception)
            {

                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), exception.Message);
            }

            // Return the model to the view
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