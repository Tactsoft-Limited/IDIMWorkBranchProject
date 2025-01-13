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
    public class ProjectPaymentDepositReportController : Controller
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

        public ProjectPaymentDepositReportController(
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
        // GET: ProjectPaymentDepositReport
        public ActionResult Index()
        {
            return RedirectToAction("ProjectPaymentDeposit");
        }


        public async Task<ActionResult> ProjectPaymentDeposit()
        {
            var model = new ReportFilterVm
            {
                ConstructionFirmDropdown = await ConstructionFirmService.GetDropdownAsync(),
                SubProjectDropdown = await SubProjectService.GetDropdownAsync()
            };
            return View(model);
        }

        public ActionResult ProjectPaymentDepositRpt(ReportFilterVm model)
        {
            string type = "PDF";

            int subProjectId = (model.SubProjectId != null) ? Convert.ToInt32(model.SubProjectId) : 0;
            int constructorFirmId = (model.ConstructionFirmId != null) ? Convert.ToInt32(model.ConstructionFirmId) : 0;
            string letterno = (model.LetterNumber != null) ? model.LetterNumber : "";
            var Param1 = new SqlParameter("@SubProjectId", subProjectId);
            var Param2 = new SqlParameter("@ConstructorFirmId", constructorFirmId);
            var Param3 = new SqlParameter("@Letterno", letterno);
            string SP_SQL = "[pm].[GetProjectPaymentDepositRpt] @SubProjectId, @ConstructorFirmId, @Letterno";
            var data = _dbContext.Database.SqlQuery<ReportDataVm>(SP_SQL, Param1, Param2, Param3).ToList();

            var reportPath = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptProjectPaymentDeposit.rdlc");
            return ReportHelper.GenerateReport(reportPath, "ProjectPaymentDepositDataSet", data, false, type);
        }

    }
}