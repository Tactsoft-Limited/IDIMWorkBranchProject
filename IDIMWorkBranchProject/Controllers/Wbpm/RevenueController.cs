using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    
    public class RevenueController : BaseController
    {
        private readonly IRevenueService _revenueService;
        private readonly IFiscalYearService _fiscalYearService;
        private readonly IRevenueNohaService _revenueNohaService;
        private readonly IRevenuePerformanceSecurityService _revenuePerformanceSecurityService;
        private readonly IRevenueWorkOrderService _revenueWorkOrderService;
        private readonly IRevenueContractAgreementService _revenueContractAgreementService;
        private readonly IConstructionCompanyService _constructionCompanyService;
        private readonly IMapper _mapper;
        public RevenueController(IActivityLogService activityLogService, IRevenueService revenueService, IMapper mapper, IFiscalYearService fiscalYearService, IRevenueNohaService revenueNohaService, IRevenuePerformanceSecurityService revenuePerformanceSecurityService, IRevenueWorkOrderService revenueWorkOrderService, IRevenueContractAgreementService revenueContractAgreementService) : base(activityLogService)
        {
            _revenueService = revenueService;
            _mapper = mapper;
            _fiscalYearService = fiscalYearService;
            _revenueNohaService = revenueNohaService;
            _revenuePerformanceSecurityService = revenuePerformanceSecurityService;
            _revenueWorkOrderService = revenueWorkOrderService;
            _revenueContractAgreementService = revenueContractAgreementService;
        }

        // GET: Revenue
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new RevenueSearchVm();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LoadData(RevenueSearchVm model)
        {
            try
            {
                var data = await _revenueService.GetPagedAsync(model);
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var revenue = await _revenueService.GetByIdAsync(id);
                if (revenue == null)
                    return HttpNotFound(); // or return a custom error view

                var noha = await _revenueNohaService.GetByRevenueIdAsync(id);
                var perfSec = await _revenuePerformanceSecurityService.GetByRevenueIdAsync(id);
                var workOrder = await _revenueWorkOrderService.GetByRevenueIdAsync(id);
                var workOrderList = await _revenueWorkOrderService.GetAllByRevenueId(id);
                var revenueContractAgreement = await _revenueContractAgreementService.GetByRevenueIdAsync(id);                

                var model = new RevenueDetailsVm
                {
                    RevenueDetail = _mapper.Map<RevenueVm>(revenue),
                    NohaDetail = noha != null ? _mapper.Map<RevenueNohaVm>(noha) : null,
                    PerformanceSecurityDetail = perfSec != null ? _mapper.Map<RevenuePerformanceSecurityVm>(perfSec) : null,
                    WorkOrderDetail = workOrder != null ? _mapper.Map<RevenueWorkOrderVm>(workOrder) : null,
                    RevenueContractAgreementDetail = revenueContractAgreement != null ? _mapper.Map<RevenueContractAgreementVm>(revenueContractAgreement) : null,                   
                    WorkOrderList = workOrderList != null ? _mapper.Map<List<RevenueWorkOrderVm>>(workOrderList) : new List<RevenueWorkOrderVm>()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> Create(int? id)
        {
            var revenue = await _revenueService?.GetByIdAsync(id);            

            var model = new RevenueVm
            {
                FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync()

            };
            

            if (revenue != null)
            {
                model.RevenueId=revenue.RevenueId;
                model.WorkTitle = revenue.WorkTitle;
                model.WorkTitleB=revenue.WorkTitleB;
                model.EstimateCost = revenue.EstimateCost;
                model.EstimateCostInWord = revenue.EstimateCostInWord;
                model.EstimateCostInWordB=revenue.EstimateCostInWordB;
                model.IsAgreementCompleted = revenue.IsAgreementCompleted;
                model.IsFurnitureIncluded = revenue.IsFurnitureIncluded;
                model.IsWorkOrderComplited = revenue.IsWorkOrderComplited;
                model.IsNoahCompleted = revenue.IsNoahCompleted;
                model.IsPerformanceSecuritySubmited=revenue.IsPerformanceSecuritySubmited;
                model.Remarks = revenue?.Remarks;
                model.FisCalYearId = revenue.FisCalYearId;
                model.FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync(revenue.FisCalYearId);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RevenueVm model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
                    model.FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync(model.FisCalYearId);
                    return View(model);
                }
                if (model.RevenueId > 0)
                {
                    await _revenueService.UpdateAsync(_mapper.Map<Revenue>(model));
                    TempData["Message"] = Messages.Success(MessageType.Update.ToString());
                    model.FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync(model.FisCalYearId);
                }
                else
                {
                    var entity = _mapper.Map<Revenue>(model);
                    await _revenueService.CreateAsync(_mapper.Map<Revenue>(entity));
                    TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                    model.FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync(model.FisCalYearId);
                }
                return RedirectToAction("Index");
            }


            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), exception.Message);
                model.FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync(model.FisCalYearId);
                return View(model);
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _revenueService.GetByIdAsync(id);

            if (entity == null)
            {
                TempData["Message"] = "The requested record was not found.";
                return RedirectToAction("List", "Revenue");
            }

            var model = _mapper.Map<RevenueVm>(entity);
            return View(model); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(RevenueVm model)
        {
            var entity = await _revenueService.GetByIdAsync(model.RevenueId);
            try
            {

                if (entity == null)
                {
                    TempData["Message"] = "Record Not Found";
                    return RedirectToAction("List", "Revenue");
                }

                await _revenueService.DeleteAsync(entity);

                TempData["Message"] = Messages.Success(MessageType.Delete.ToString());
                return RedirectToAction("List", "Revenue");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), exception.InnerException?.Message);
                return RedirectToAction("List", "Revenue"); // Avoids null reference
            }
        }
    }
}