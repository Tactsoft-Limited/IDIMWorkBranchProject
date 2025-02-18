using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Extentions.ReportHealper;
using IDIMWorkBranchProject.Extentions.ReportHelper;
using IDIMWorkBranchProject.Models.WBP;
using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.WBP;

using Microsoft.Reporting.WebForms;

using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Report
{
	public class ProjectProblemReportController : Controller
	{
		protected IFiscalYearService FiscalYearService { get; set; }
		protected IGeneralInformationService GeneralInformationService { get; set; }
		protected IProjectService ProjectService { get; set; }
		protected IUnitService UnitService { get; set; }
		protected IProjectProblemService ProjectProblemService { get; set; }
		protected ISubProjectService SubProjectService { get; set; }
		protected IConstructionFirmService ConstructionFirmService { get; set; }
		protected IConsultantService ConsultantService { get; set; }
		private readonly IDIMDBEntities _dbContext;

		public ProjectProblemReportController(
			IFiscalYearService fiscalYearService,
			IGeneralInformationService generalInformationService,
			IProjectService projectService,
			IUnitService unitService,
			IProjectProblemService projectProblemService,
			ISubProjectService subProjectService,
			IConstructionFirmService constructionFirmService,
			IConsultantService consultantService,
			IDIMDBEntities dbContext)
		{
			FiscalYearService = fiscalYearService;
			GeneralInformationService = generalInformationService;
			ProjectService = projectService;
			UnitService = unitService;
			ProjectProblemService = projectProblemService;
			SubProjectService = subProjectService;
			ConstructionFirmService = constructionFirmService;
			ConsultantService = consultantService;
			_dbContext = dbContext;
		}
		// GET: ProjectProblemReport
		public ActionResult Index()
		{
			return RedirectToAction("ProjectProblem");
		}

		public async Task<ActionResult> ProjectProblem()
		{
			var model = new SubProjectSearchVm
			{
				UnitDropdown = await UnitService.GetDropDownAsync(),
				ConstructionFirmDropdown = await ConstructionFirmService.GetDropdownAsync(),
				SubProjects = await SubProjectService.GetByAsync()
			};

			return View(model);
		}

		[HttpPost]
		public async Task<ActionResult> ProjectProblem(SubProjectSearchVm model)
		{
			model.UnitDropdown = await UnitService.GetDropDownAsync(model.UnitId);
			model.ConstructionFirmDropdown = await ConstructionFirmService.GetDropdownAsync(model.ConstructionFirmId);
			model.SubProjects = await SubProjectService.GetByAsync(model);

			return View(model);
		}

		public ActionResult ProjectProblemRpt(int id)
		{
			var data = _dbContext.ViewProjectSubProjectDetailsRpts.Where(i => i.SubProjectId == id).ToList();
			var reportDataSource = new List<ReportDataSource>
			{
				new ReportDataSource("DSProblem", data)
			};
			var config = new ReportConfig
			{
				ReportFilePath = Path.Combine(Server.MapPath("~/Report/rdlc"), "RDLCProblem.rdlc")
			};

			return new ReportResult(config, reportDataSource);
		}

		public ActionResult SubprojectBill(int id)
		{
			var data = _dbContext.ViewProjectSubProjectDetailsRpts.Where(i => i.SubProjectId == id).ToList();

			var reportDataSource = new List<ReportDataSource>
			{
				new ReportDataSource("DSBill", data)
			};

			var config = new ReportConfig
			{
				ReportFilePath = Path.Combine(Server.MapPath("~/Report/rdlc"), "RDLCBill.rdlc")
			};

			return new ReportResult(config, reportDataSource);
		}

		public ActionResult SubprojectExtend(int id)
		{
			var data = _dbContext.ViewProjectSubProjectDetailsRpts.Where(i => i.SubProjectId == id).ToList();

			var reportDataSource = new List<ReportDataSource>
			{
				new ReportDataSource("DsProjectExtend", data)
			};

			var config = new ReportConfig
			{
				ReportFilePath = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptProjectExtend.rdlc")
			};

			return new ReportResult(config, reportDataSource);
		}

		public ActionResult SubprojectProblem(int id)
		{
			var data = _dbContext.ViewProjectSubProjectDetailsRpts.Where(i => i.ProjectId == id).ToList();

			var reportDataSource = new List<ReportDataSource>
			{
				new ReportDataSource("DsProjectProblem", data)
			};

			var config = new ReportConfig
			{
				ReportFilePath = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptProjectProblem.rdlc")
			};

			return new ReportResult(config, reportDataSource);
		}

		public ActionResult SubProjectStatusRpt(int id)
		{
			var data = _dbContext.ViewProjectSubProjectDetailsRpts.Where(i => i.SubProjectId == id).ToList();

			var reportDataSource = new List<ReportDataSource>
			{
				new ReportDataSource("DsProjectStatus", data)
			};

			var config = new ReportConfig
			{
				ReportFilePath = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptProjectStatus.rdlc")
			};

			return new ReportResult(config, reportDataSource);
		}

	}
}