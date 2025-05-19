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
	public class FinancialYearAllocationController : BaseController
    {
        protected readonly IFinancialYearAllocationService _financialYearAllocationService;
        protected readonly IADPProjectService _aDPProjectService;
        protected readonly IFiscalYearService _fiscalYearService;
        protected readonly IMapper _mapper;

        public FinancialYearAllocationController(IActivityLogService activityLogService, IFinancialYearAllocationService financialYearAllocationService, IMapper mapper, IADPProjectService aDPProjectService, IFiscalYearService fiscalYearService) : base(activityLogService)
        {
            _financialYearAllocationService = financialYearAllocationService;
            _mapper = mapper;
            _aDPProjectService = aDPProjectService;
            _fiscalYearService = fiscalYearService;
        }

        public ActionResult Index()
        {

            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            var model = new FinancialYearAllocationSearchVm();
            return View(model);
        }
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
            var data = await _aDPProjectService.GetByIdAsync(id);
            var model = new FinancialYearAllocationVm
            {
                ADPProjectId = data.ADPProjectId,
                ProjectTitle = data.ProjectTitle,
                FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync(),

            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(FinancialYearAllocationVm model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
                    model.FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync(model.FiscalYearId);
                    return View(model);
                }

                var entity = _mapper.Map<FinancialYearAllocation>(model);
                await _financialYearAllocationService.CreateAsync(entity);
                TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                return RedirectToAction("details/" + model.ADPProjectId, "ADPProject");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), exception.Message);
                model.FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync(model.FiscalYearId);
                return View(model);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {

            var model = _mapper.Map<FinancialYearAllocationVm>(await _financialYearAllocationService.GetByIdAsync(id));
            model.ProjectTitle = await _aDPProjectService.GetAdpProjectTitle(model.ADPProjectId);
            model.FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync(model.FiscalYearId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(FinancialYearAllocationVm model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = Messages.InvalidInput(MessageType.Update.ToString());
                    model.FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync(model.FiscalYearId);
                    return View(model);
                }

                var entity = _mapper.Map<FinancialYearAllocation>(model);
                await _financialYearAllocationService.UpdateAsync(entity);

                TempData["Message"] = Messages.Success(MessageType.Update.ToString());
                return RedirectToAction("details/" + model.ADPProjectId, "ADPProject");  // Redirect to list after success
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Update.ToString(), exception.InnerException.Message);
                model.FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync(model.FiscalYearId);
                return View(model);
            }
        }


        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _financialYearAllocationService.GetByIdAsync(id);

            if (entity == null)
            {
                TempData["Message"] = "The requested record was not found.";
                return RedirectToAction("details/" + entity.ADPProjectId, "ADPProject");
            }

            var model = _mapper.Map<FinancialYearAllocationVm>(entity);
            model.ProjectTitle = await _aDPProjectService.GetAdpProjectTitle(entity.ADPProjectId);
            return View(model); // Load the delete confirmation view
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(FinancialYearAllocationVm model)
        {
            var entity = await _financialYearAllocationService.GetByIdAsync(model.FinancialYearAllocationId);
            try
            {
               
                if (entity == null)
                {
                    TempData["Message"] = "Record Not Found";
                    return RedirectToAction("Details/" + entity.ADPProjectId, "ADPProject");
                }

                await _financialYearAllocationService.DeleteAsync(entity);

                TempData["Message"] = Messages.Success(MessageType.Delete.ToString());
                return RedirectToAction("Details/" + entity.ADPProjectId, "ADPProject" );
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), exception.InnerException?.Message);
                return RedirectToAction("Details/" + entity.ADPProjectId, "ADPProject" ); // Avoids null reference
            }
        }


    }
}