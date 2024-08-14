using BGB.Data.Database;
using IDIMWorkBranchProject.Extentions.Healper;
using IDIMWorkBranchProject.Models.Report;
using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.WBP;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Report
{
    public class ConsultancyFeesReportController : Controller
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

        public ConsultancyFeesReportController(
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
        // GET: ConsultancyFeesReport
        public ActionResult Index()
        {
            return RedirectToAction("ConsultancyFees");
        }

        public async Task<ActionResult> ConsultancyFees()
        {
            var model = new ReportFilterVm
            {
                ConstructionFirmDropdown = await ConstructionFirmService.GetDropdownAsync(),
                ConsultantDropdown = await ConsultantService.GetDropdownAsync(),
                FiscalYearDropdown = await FiscalYearService.GetDropdownAsync()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ConsultancyFees(ReportFilterVm model)
        {
            string type = "PDF";
            int constructorFirmId = (model.ConstructionFirmId != null) ? Convert.ToInt32(model.ConstructionFirmId) : 0;
            int consultantId = (model.ConsultantId != null) ? Convert.ToInt32(model.ConsultantId) : 0;
            int fiscalYearId = (model.FiscalYearId != null) ? Convert.ToInt32(model.FiscalYearId) : 0;
            var Param1 = new SqlParameter("@ConstructorFirmId", constructorFirmId);
            var Param2 = new SqlParameter("@ConsultantId", constructorFirmId);
            var Param3 = new SqlParameter("@FiscalYearId", fiscalYearId);
            string SP_SQL = "[pm].[GetConsultantFeeAmountRpt] @ConstructorFirmId, @ConsultantId, @FiscalYearId";
            var data = _dbContext.Database.SqlQuery<ReportDataVm>(SP_SQL, Param1, Param2, Param3).ToList();

            var reportPath = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptConsultantFeeAmount.rdlc");
            return ReportHelper.GenerateReport(reportPath, "ConsultancyFeesDataSet", data, false, type);

        }
    }
}