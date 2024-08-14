using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.WBP;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.IO;
using BGB.Data.SqlViews.Pm;
using BGB.Data.Database;
using IDIMWorkBranchProject.Models.Report;
using IDIMWorkBranchProject.Extentions.Healper;
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
            string type = "PDF";
            var reportPath = Path.Combine(Server.MapPath("~/Report/rdlc"), "RDLCProblem.rdlc");
            var data = _dbContext.ViewProjectProblems.Where(i => i.SubProjectId == id).ToList();
            return ReportHelper.GenerateReport(reportPath, "DSProblem", data, false, type);
        }
    }
}