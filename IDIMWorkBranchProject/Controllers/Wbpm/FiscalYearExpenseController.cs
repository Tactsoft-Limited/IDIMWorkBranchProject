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
	public class FiscalYearExpenseController : BaseController
	{
		protected readonly IFiscalYearExpenseService _fiscalYearExpenseService;
		protected readonly IMapper _mapper;
		protected readonly IADPProjectService _aDPProjectService;
		protected readonly IFiscalYearService _fiscalYearService;

		public FiscalYearExpenseController(IActivityLogService activityLogService, IFiscalYearExpenseService fiscalYearExpenseService, IMapper mapper, IADPProjectService aDPProjectService, IFiscalYearService fiscalYearService) : base(activityLogService)
		{
			_fiscalYearExpenseService = fiscalYearExpenseService;
			_mapper = mapper;
			_aDPProjectService = aDPProjectService;
			_fiscalYearService = fiscalYearService;
		}


		// GET: FiscalYearExpense
		public ActionResult Index()
		{
			return RedirectToAction("List");
		}

		public ActionResult List()
		{
			var model = new FiscalYearExpenseSearchVm();
			return View(model);
		}
        public async Task<ActionResult> LoadData(FiscalYearExpenseSearchVm model)
        {
            try
            {
                var data = await _fiscalYearExpenseService.GetPagedAsync(model);
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
			var model = new FiscalYearExpenseVm
			{
				ADPProjectId = data.ADPProjectId,
				ProjectTitle = data.ProjectTitle,
				FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync()
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(FiscalYearExpenseVm model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
					model.FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync(model.FiscalYearId);
					return View(model);
				}

				var entity = _mapper.Map<FiscalYearExpense>(model);
				await _fiscalYearExpenseService.CreateAsync(entity);
				TempData["Message"] = Messages.Success(MessageType.Create.ToString());
				return RedirectToAction("Details/" + model.ADPProjectId, "ADPProject");
			}
			catch (Exception exception)
			{
				TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), exception.Message);
				model.FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync();
				return View(model);
			}
		}

		public async Task<ActionResult> Edit(int id)
		{
			var model = _mapper.Map<FiscalYearExpenseVm>(await _fiscalYearExpenseService.GetByIdAsync(id));
			model.ProjectTitle = await _aDPProjectService.GetAdpProjectTitle(model.ADPProjectId);
			model.FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync();
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(FiscalYearExpenseVm model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
					model.FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync(model.FiscalYearId);
					return View(model);
				}

				var entity = _mapper.Map<FiscalYearExpense>(model);
				await _fiscalYearExpenseService.UpdateAsync(entity);
				TempData["Message"] = Messages.Success(MessageType.Create.ToString());
				return RedirectToAction("Details/" + model.ADPProjectId, "ADPProject");
			}
			catch (Exception exception)
			{
				TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), exception.Message);
				model.FiscalYearDropdown = await _fiscalYearService.GetDropdownAsync(model.FiscalYearId);
				return View(model);
			}
		}

	}
}