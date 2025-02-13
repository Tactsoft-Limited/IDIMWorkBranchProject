using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.WBP;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Extentions.ReportHealper;
using IDIMWorkBranchProject.Extentions.ReportHelper;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
namespace IDIMWorkBranchProject.Controllers.Report
{
	public class ReportProblemController : Controller
    {
        protected IFiscalYearService FiscalYearService { get; set; }
        protected IGeneralInformationService GeneralInformationService { get; set; }

        protected ISubProjectService SubProjectService { get; set; }
        protected IConstructionFirmService ConstructionFirmService { get; set; }
        private readonly IDIMDBEntities _dbContext;

        public ReportProblemController(IFiscalYearService fiscalYearService,

            IGeneralInformationService generalInformationService,

            ISubProjectService subProjectService
,
            IDIMDBEntities dbContext)
        {

            GeneralInformationService = generalInformationService;
            FiscalYearService = fiscalYearService;

            SubProjectService = subProjectService;
            _dbContext = dbContext;
        }
        // GET: ProjectReport
        //public async Task<ActionResult> Index()
        //{
        //    var model = new ReportFilterVm
        //    {

        //        SubProjectDropdown = await SubProjectService.GetDropdownAsync(),
        //    };

        //    return View(model);
        //}

        [HttpPost]
        public ActionResult Index(int id)
        {
            var data = _dbContext.ViewProjectProblems.Where(i => i.SubProjectId == id).ToList();
            var reportDataSource = new List<ReportDataSource>
            {
	            new ReportDataSource("DSProblem", data)
            };

			var config = new ReportConfig
			{
				ReportFilePath = Path.Combine(Server.MapPath("~/Report/rdlc"), "RDLCProblem.rdlc")
			};

			return new ReportResult(config, reportDataSource);
		}
    }
}