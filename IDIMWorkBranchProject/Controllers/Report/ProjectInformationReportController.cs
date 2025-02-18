using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Extentions.ReportHealper;
using IDIMWorkBranchProject.Extentions.ReportHelper;
using IDIMWorkBranchProject.Models.WBP;
using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.WBP;

using Microsoft.Reporting.WebForms;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Report
{
	public class ProjectInformationReportController : Controller
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

        public ProjectInformationReportController(IFiscalYearService fiscalYearService, IGeneralInformationService generalInformationService, IProjectService projectService, IUnitService unitService, IProjectProblemService projectProblemService, ISubProjectService subProjectService, IConstructionFirmService constructionFirmService, IConsultantService consultantService, IDIMDBEntities dbContext)
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

        // GET: ProjectInformationReport
        public ActionResult Index()
        {
            return RedirectToAction("ProjectInformation");
        }

        public async Task<ActionResult> ProjectInformation()
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
        public async Task<ActionResult> ProjectInformation(ProjectSearchVm model)
        {
            model.FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(model.FiscalYearId);
            model.Projects = await ProjectService.GetAllAsync();

            return View(model);
        }

        public ActionResult ProjectInformationRpt(int id, string type)
        {
            var data = _dbContext.ViewProjectInformationRpts.Where(i => i.ProjectId == id).ToList();
			var reportDataSource = new List<ReportDataSource>
            {
	            new ReportDataSource("DsWorkBranch", data)
            };

			var config = new ReportConfig
			{
				ReportFilePath = Path.Combine(Server.MapPath("~/Report/rdlc"), "rptProjectInformation.rdlc")
			};

			return new ReportResult(config, reportDataSource);
		}
    }
}