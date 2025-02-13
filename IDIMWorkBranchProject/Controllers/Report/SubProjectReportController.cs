using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Extentions.ReportHealper;
using IDIMWorkBranchProject.Extentions.ReportHelper;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.WBP;
using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.WBP;

using Microsoft.Reporting.WebForms;

using System.Collections.Generic;
using System;
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
			try
			{
				var data = _dbContext.ViewProjectWiseSubProjectRpts.Where(i => i.ProjectId == id).ToList();
				var reportDataSource = new List<ReportDataSource>
		        {
			        new ReportDataSource("DsArmyDisease", data)
		        };

				var config = new ReportConfig
				{
					ReportFilePath = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptProjectWiseSubProject.rdlc")
				};

				return new ReportResult(config, reportDataSource);
			}
			catch (Exception exception)
			{
				// Log the exception if necessary
				// You can also throw a custom exception if you want
				throw new InvalidOperationException("An error occurred while generating the report.", exception);
			}
		}
    }
}