using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class FinancialYearAllocationController : BaseController
    {
        private readonly IFinancialYearAllocationService _financialYearAllocationService;
        private readonly IADPProjectService _aDPProjectService;
        private readonly IFiscalYearService _fiscalYearService;
        private readonly IMapper _mapper;

        public FinancialYearAllocationController(
            IActivityLogService activityLogService,
            IFinancialYearAllocationService financialYearAllocationService,
            IMapper mapper,
            IADPProjectService aDPProjectService,
            IFiscalYearService fiscalYearService) : base(activityLogService)
        {
            _financialYearAllocationService = financialYearAllocationService;
            _mapper = mapper;
            _aDPProjectService = aDPProjectService;
            _fiscalYearService = fiscalYearService;
        }

        public ActionResult Index() => RedirectToAction("List");

        public ActionResult List()
        {
            var model = new FinancialYearAllocationSearchVm();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LoadData(FinancialYearAllocationSearchVm model)
        {
            try
            {
                var data = await _financialYearAllocationService.GetPagedAsync(model);
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        public async Task<ActionResult> Create(int id)
        {
            var project = await _aDPProjectService.GetByIdAsync(id);
            var model = new FinancialYearAllocationVm
            {
                ADPProjectId = project.ADPProjectId,
                ProjectTitle = project.ProjectTitle
            };
            await PopulateDropdownsAsync(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FinancialYearAllocationVm model)
        {
            if (!ModelState.IsValid)
            {
                SetResponseMessage(DefaultMsg.InvalidInput, ResponseType.Error);
                await PopulateDropdownsAsync(model);
                return View(model);
            }

            try
            {
                var entity = _mapper.Map<FinancialYearAllocation>(model);
                await _financialYearAllocationService.CreateAsync(entity);
                SetResponseMessage(DefaultMsg.SaveSuccess, ResponseType.Success);
                return RedirectToAction("Details", "ADPProject", new { id = model.ADPProjectId });
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.SaveFailed, "Financial Year Allocation", ex.Message), ResponseType.Error);
                await PopulateDropdownsAsync(model);
                return View(model);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var entity = await _financialYearAllocationService.GetByIdAsync(id);
            var model = _mapper.Map<FinancialYearAllocationVm>(entity);
            model.ProjectTitle = await _aDPProjectService.GetAdpProjectTitle(model.ADPProjectId);
            await PopulateDropdownsAsync(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(FinancialYearAllocationVm model)
        {
            if (!ModelState.IsValid)
            {
                SetResponseMessage(DefaultMsg.InvalidInput, ResponseType.Error);
                await PopulateDropdownsAsync(model);
                return View(model);
            }

            try
            {
                var entity = _mapper.Map<FinancialYearAllocation>(model);
                await _financialYearAllocationService.UpdateAsync(entity);
                SetResponseMessage(DefaultMsg.UpdateSuccess, ResponseType.Success);
                return RedirectToAction("Details", "ADPProject", new { id = model.ADPProjectId });
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.UpdateFailed, "Financial Year Allocation", ex.Message), ResponseType.Error);
                await PopulateDropdownsAsync(model);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _financialYearAllocationService.DeleteAsync(id);
                SetResponseMessage(string.Format(DefaultMsg.DeleteSuccess, "Financial Year Allocation"), ResponseType.Success);
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.DeleteFailed, "Financial Year Allocation", ex.Message), ResponseType.Error);
            }

            return RedirectToAction("List");
        }

        private async Task PopulateDropdownsAsync(FinancialYearAllocationVm model)
        {
            model.FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync(model.FiscalYearId);
        }
    }
}
