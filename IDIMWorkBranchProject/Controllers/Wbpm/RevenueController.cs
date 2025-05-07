using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    
    public class RevenueController : BaseController
    {
        private readonly IRevenueService _revenueService;
        private readonly IFiscalYearService _fiscalYearService;
        private readonly IMapper _mapper;
        public RevenueController(IActivityLogService activityLogService, IRevenueService revenueService, IMapper mapper, IFiscalYearService fiscalYearService) : base(activityLogService)
        {
            _revenueService = revenueService;
            _mapper = mapper;
            _fiscalYearService = fiscalYearService;
        }

        // GET: Revenue
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Create(int id)
        {
            var revenue = await _revenueService.GetByIdAsync(id);            

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
                return View(model);
            }


            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), exception.Message);
                model.FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync(model.FisCalYearId);
                return View(model);
            }
        }
    }
}