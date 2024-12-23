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
    public class SubProjectReportController : Controller
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

        public SubProjectReportController(
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
        // GET: SubProjectReport
        public ActionResult Index()
        {
            return RedirectToAction("SubProject");
        }

        public async Task<ActionResult> SubProject()
        {

            var model = new ProjectSearchVm
            {
                AuthorizeUnitDropdown = await UnitService.GetDropDownAsync(),
                FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(),
                Projects = await ProjectService.GetAllAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> SubProject(ProjectSearchVm model)
        {
            model.FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(model.FiscalYearId);
            model.Projects = await ProjectService.GetAllAsync();

            return View(model);
        }

        public ActionResult SubProjectRpt(int id, string type)
        {
            var reportPath = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptProjectWiseSubProject.rdlc");
            var data = _dbContext.ViewProjectWiseSubProjectRpts.Where(i => i.ProjectId == id).ToList();
            return ReportHelper.GenerateReport(reportPath, "DsSubProject", data, true, type);
        }
    }
}