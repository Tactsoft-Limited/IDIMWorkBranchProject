using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Extentions.ReportHealper;
using IDIMWorkBranchProject.Extentions.ReportHelper;
using IDIMWorkBranchProject.Models.Report;
using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.WBP;

using Microsoft.Reporting.WebForms;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Report
{
	public class FullPaymentProjectReportController : Controller
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

		public FullPaymentProjectReportController(
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
		// GET: FullPaymentProjectReport
		public ActionResult Index()
		{
			return RedirectToAction("FullPaymentProjectReport");
		}

		public async Task<ActionResult> FullPaymentProjectReport()
		{
			var model = new ReportFilterVm
			{
				SubProjectDropdown = await SubProjectService.GetDropdownAsync()
			};
			return View(model);
		}

		[HttpPost]
		public ActionResult FullPaymentProjectReport(ReportFilterVm model)
		{
			int subProjectId = (model.SubProjectId != null) ? Convert.ToInt32(model.SubProjectId) : 0;
			var Param1 = new SqlParameter("@SubProjectId", subProjectId);
			string SP_SQL = "[pm].[GetFullPaymentByProjectRpt] @SubProjectId";
			var data = _dbContext.Database.SqlQuery<ReportDataVm>(SP_SQL, Param1).ToList();

			var reportDataSource = new List<ReportDataSource>
			{
				new ReportDataSource("FullPaymentByProjectDataSet", data)
			};

			var config = new ReportConfig
			{
				ReportFilePath = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptFullPaymentByProjectDetails.rdlc")
			};

			return new ReportResult(config, reportDataSource);

		}
	}
}