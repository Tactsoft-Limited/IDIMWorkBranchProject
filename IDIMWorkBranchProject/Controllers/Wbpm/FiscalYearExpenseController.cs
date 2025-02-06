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

		public FiscalYearExpenseController(IActivityLogService activityLogService, IFiscalYearExpenseService fiscalYearExpenseService, IMapper mapper) : base(activityLogService)
		{
			_fiscalYearExpenseService = fiscalYearExpenseService;
			_mapper = mapper;
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
					return View(model);
				}

				var entity = _mapper.Map<ADPProject>(model);
				await _aDPProjectService.CreateAsync(entity);
				TempData["Message"] = Messages.Success(MessageType.Create.ToString());
				return RedirectToAction("Details/"+model.ADPProjectId, "ADPProject");
			}
			catch (Exception exception)
			{
				TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), exception.Message);
				return View(model);
			}
		}

	}
}