using IDIMWorkBranchProject.Extentions.Healper;
using IDIMWorkBranchProject.Models.Report;
using IDIMWorkBranchProject.Services.Setup;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Web.Mvc;
using BGB.Data.Database;
using IDIMWorkBranchProject.Services.WBP;
using System.Linq;

namespace IDIMWorkBranchProject.Controllers.Report
{
    public class ProjectPaymentReceiptReportController : Controller
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

        public ProjectPaymentReceiptReportController(
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
        // GET: ProjectPaymentReceiptReport
        public ActionResult Index()
        {
            return RedirectToAction("ProjectPaymentReceipt");
        }

        public async Task<ActionResult> ProjectPaymentReceipt()
        {
            var model = new ReportFilterVm
            {
                FiscalYearDropdown = await FiscalYearService.GetDropdownAsync()
            };
            return View(model);
        }

        public ActionResult ProjectPaymentReceiptRpt()
        {
            int fiscalYearId = Convert.ToInt32(Request.Form["FiscalYearId"]);
            string type = "PDF";
            var reportPath = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptProjectWiseReceiptPayment.rdlc");
            var data = _dbContext.ViewProjectPaymentReceiptRpts.Where(i => i.FiscalYearId == fiscalYearId).ToList();
            return ReportHelper.GenerateReport(reportPath, "DsProjectPaymentReceipt", data, false, type);
        }

    }
}