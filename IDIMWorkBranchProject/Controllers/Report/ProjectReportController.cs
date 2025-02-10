using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.WBP;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.IO;
using IDIMWorkBranchProject.Models.Report;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Extentions.ReportHealper;
using IDIMWorkBranchProject.Extentions.ReportHelper;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;


namespace IDIMWorkBranchProject.Controllers.Report
{
	public class ProjectReportController : Controller
    {
        protected IFiscalYearService FiscalYearService { get; set; }
        protected IGeneralInformationService GeneralInformationService { get; set; }
        protected IProjectService ProjectService { get; set; }
        protected IUnitService UnitService { get; set; }
        protected IProjectProblemService ProjectProblemService { get; set; }
        protected ISubProjectService SubProjectService { get; set; }
        protected IConstructionFirmService ConstructionFirmService { get; set; }
        private readonly IDIMDBEntities _dbContext;

        public ProjectReportController(
            IFiscalYearService fiscalYearService,
            IGeneralInformationService generalInformationService,
            IProjectService projectService,
            IUnitService unitService,
            IProjectProblemService projectProblemService,
            ISubProjectService subProjectService,
            IConstructionFirmService constructionFirmService
,
            IDIMDBEntities dbContext)
        {
            FiscalYearService = fiscalYearService;
            GeneralInformationService = generalInformationService;
            ProjectService = projectService;
            UnitService = unitService;
            ProjectProblemService = projectProblemService;
            SubProjectService = subProjectService;
            ConstructionFirmService = constructionFirmService;
            _dbContext = dbContext;
        }
        // GET: ProjectReport
        public async Task<ActionResult> Index()
        {
            var model = new ReportFilterVm
            {

                FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(),

            };

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(ReportFilterVm model)
        {
            int fiscalYearId = Convert.ToInt32(model.FiscalYearId);
            var data = _dbContext.ViewProjectProblems.Where(i => i.FiscalYearId == fiscalYearId).ToList();

			var reportDataSource = new List<ReportDataSource>
            {
	            new ReportDataSource("DsProjectList", data)
            };

			var config = new ReportConfig
			{
				ReportFilePath = Path.Combine(Server.MapPath("~/Report/rdlc"), "rptProjectList.rdlc")
			};

			return new ReportResult(config, reportDataSource);
		}
    }
}