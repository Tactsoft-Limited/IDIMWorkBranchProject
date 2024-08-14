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
    public class PartialPaymentProjectReportController : Controller
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

        public PartialPaymentProjectReportController(
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
        // GET: PartialPaymentProjectReport
        public ActionResult Index()
        {
            return RedirectToAction("PartialPaymentProjectReport");
        }

        public async Task<ActionResult> PartialPaymentProjectReport()
        {
            var model = new ReportFilterVm
            {
                SubProjectDropdown = await SubProjectService.GetDropdownAsync()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult PartialPaymentProjectReport(ReportFilterVm model)
        {
            string type = "PDF";
            int subProjectId = (model.SubProjectId != null) ? Convert.ToInt32(model.SubProjectId) : 0;
            var Param1 = new SqlParameter("@SubProjectId", subProjectId);
            string SP_SQL = "[pm].[GetPartialPaymentByProjectRpt] @SubProjectId";
            var data = _dbContext.Database.SqlQuery<ReportDataVm>(SP_SQL, Param1).ToList();
            var reportPath = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptPartialPaymentByProjectDetails.rdlc");
            return ReportHelper.GenerateReport(reportPath, "PartialPaymentByProjectDataSet", data, false, type);
        }
    }
}