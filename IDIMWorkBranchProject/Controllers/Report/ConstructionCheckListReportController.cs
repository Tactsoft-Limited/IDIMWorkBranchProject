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
    public class ConstructionCheckListReportController : Controller
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

        public ConstructionCheckListReportController(
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

        // GET: ConstructionCheckListReport
        public ActionResult Index()
        {
            return RedirectToAction("ConstructionCheckList");
        }

        public async Task<ActionResult> ConstructionCheckList()
        {
            var model = new ReportFilterVm
            {
                ConstructionFirmDropdown = await ConstructionFirmService.GetDropdownAsync(),
                SubProjectDropdown = await SubProjectService.GetDropdownAsync(),
                FiscalYearDropdown = await FiscalYearService.GetDropdownAsync()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ConstructionCheckList(ReportFilterVm model)
        {
            string type = "PDF";
            int subProjectId = (model.SubProjectId != null) ? Convert.ToInt32(model.SubProjectId) : 0;
            int constructorFirmId = (model.ConstructionFirmId != null) ? Convert.ToInt32(model.ConstructionFirmId) : 0;
            int fiscalYearId = (model.FiscalYearId != null) ? Convert.ToInt32(model.FiscalYearId) : 0;
            var Param1 = new SqlParameter("@SubProjectId", subProjectId);
            var Param2 = new SqlParameter("@ConstructorFirmId", constructorFirmId);
            var Param3 = new SqlParameter("@FiscalYearId", fiscalYearId);
            string SP_SQL = "[pm].[GetConstructionChecklistRpt] @SubProjectId, @ConstructorFirmId, @FiscalYearId";
            var data = _dbContext.Database.SqlQuery<ReportDataVm>(SP_SQL, Param1, Param2, Param3).ToList();
            var reportPath = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptConstructionCheckList.rdlc");
            return ReportHelper.GenerateReport(reportPath, "ConstructionCheckListDataSet", data, false, type);

        }

    }
}