using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Extentions.Healper;
using IDIMWorkBranchProject.Models.Report;
using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.WBP;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Report
{
    public class YearlyConstructionReportController : Controller
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

        public YearlyConstructionReportController(
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
        // GET: YearlyConstructionReport
        public ActionResult Index()
        {
            return RedirectToAction("YearlyConstructionReport");
        }

        public async Task<ActionResult> YearlyConstructionReport()
        {
            var model = new ReportFilterVm
            {
                FiscalYearDropdown = await FiscalYearService.GetDropdownAsync()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult YearlyConstructionReport(ReportFilterVm model)
        {
            string type = "PDF";
            int fiscalYearId = (model.FiscalYearId != null) ? Convert.ToInt32(model.FiscalYearId) : 0;
            var Param1 = new SqlParameter("@FiscalYearId", fiscalYearId);
            string SP_SQL = "[pm].[GetYearlyConstructionDetailsRpt] @FiscalYearId";
            var data = _dbContext.Database.SqlQuery<ReportDataVm>(SP_SQL, Param1).ToList();
            var reportPath = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptYearlyConstructionDetails.rdlc");
            return ReportHelper.GenerateReport(reportPath, "YearlyConstructionDetailsDataSet", data, true, type);

        }
    }
}