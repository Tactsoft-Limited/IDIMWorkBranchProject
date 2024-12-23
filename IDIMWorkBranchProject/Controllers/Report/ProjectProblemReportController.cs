using BGB.Data.Database;
using IDIMWorkBranchProject.Extentions.Healper;
using IDIMWorkBranchProject.Models.WBP;
using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.WBP;
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

        public ActionResult ProjectProblemRpt(int id, string type = "PDF")
        {
            var reportPath = Path.Combine(Server.MapPath("~/Report/rdlc"), "RDLCProblem.rdlc");
            var data = _dbContext.ViewProjectSubProjectDetailsRpts.Where(i => i.SubProjectId == id).ToList();
            return ReportHelper.GenerateReport(reportPath, "DSProblem", data, false, type);
        }

        public ActionResult SubprojectBill(int id)
        {
            string type = "PDF";
            var reportPath = Path.Combine(Server.MapPath("~/Report/rdlc"), "RDLCBill.rdlc");
            var data = _dbContext.ViewProjectSubProjectDetailsRpts.Where(i => i.SubProjectId == id).ToList();
            return ReportHelper.GenerateReport(reportPath, "DSBill", data, false, type);
        }


        public ActionResult SubprojectExtend(int id)
        {
            string type = "PDF";
            var reportPath = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptProjectExtend.rdlc");
            var data = _dbContext.ViewProjectSubProjectDetailsRpts.Where(i => i.SubProjectId == id).ToList();
            return ReportHelper.GenerateReport(reportPath, "DsProjectExtend", data, false, type);
        }

        public ActionResult SubprojectProblem(int id)
        {
            string type = "PDF";
            var reportPath = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptProjectProblem.rdlc");
            var data = _dbContext.ViewProjectSubProjectDetailsRpts.Where(i => i.ProjectId == id).ToList();
            return ReportHelper.GenerateReport(reportPath, "DsProjectProblem", data, false, type);
        }

        public ActionResult SubProjectStatusRpt(int id)
        {
            string type = "PDF";
            var reportPath = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptProjectStatus.rdlc");
            var data = _dbContext.ViewProjectSubProjectDetailsRpts.Where(i => i.SubProjectId == id).ToList();
            return ReportHelper.GenerateReport(reportPath, "DsProjectStatus", data, false, type);
        }
    }
}