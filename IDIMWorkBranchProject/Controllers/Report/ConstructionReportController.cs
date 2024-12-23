using BGB.Data.Database;
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
    public class ConstructionReportController : Controller
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

        public ConstructionReportController(
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
        // GET: ConstructionReport
        public ActionResult Index()
        {
            return RedirectToAction("ConstructionReport");
        }
        public async Task<ActionResult> ConstructionReport()
        {
            var model = new ReportFilterVm
            {
                ConsultantDropdown = await ConsultantService.GetDropdownAsync(),
                FiscalYearDropdown = await FiscalYearService.GetDropdownAsync()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ConstructionReport(ReportFilterVm model)
        {
            string type = "PDF";
            int consultantId = (model.ConsultantId != null) ? Convert.ToInt32(model.ConsultantId) : 0;
            int fiscalYearId = (model.FiscalYearId != null) ? Convert.ToInt32(model.FiscalYearId) : 0;
            var Param1 = new SqlParameter("@ConsultantId", consultantId);
            var Param2 = new SqlParameter("@FiscalYearId", fiscalYearId);
            string SP_SQL = "[pm].[GetDetailsOfConstructionRpt] @ConsultantId, @FiscalYearId";
            var data = _dbContext.Database.SqlQuery<ReportDataVm>(SP_SQL, Param1, Param2).ToList();
            var reportPath = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptConstructionDetails.rdlc");
            return ReportHelper.GenerateReport(reportPath, "ConstructionDetailsDataSet", data, false, type);
        }

    }
}