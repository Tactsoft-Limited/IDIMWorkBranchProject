using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.WBP;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Extentions;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System;
using IDIMWorkBranchProject.Extentions.ReportHelper;
using IDIMWorkBranchProject.Extentions.ReportHealper;
using System.Threading.Tasks;
using IDIMWorkBranchProject.Models.Report;
namespace IDIMWorkBranchProject.Controllers.Report
{
	public class ProjectExtendedController : Controller
    {
        // GET: ProjectExtended
        protected IFiscalYearService FiscalYearService { get; set; }
        protected IGeneralInformationService GeneralInformationService { get; set; }

        protected ISubProjectService SubProjectService { get; set; }
        protected IConstructionFirmService ConstructionFirmService { get; set; }
        private readonly IDIMDBEntities _dbContext;


        public ProjectExtendedController(IFiscalYearService fiscalYearService,

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

        //GET: ProjectReport
        public async Task<ActionResult> Index()
        {
            var model = new ReportFilterVm
            {
                SubProjectDropdown = await SubProjectService.GetDropdownAsync(),
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(int? id)
        {
			try
			{
				if (ModelState.IsValid)
				{
					var data = _dbContext.ViewExtendeds.Where(i => i.SubProjectId == id).ToList();

					var reportDataSource = new List<ReportDataSource>
				    {
					    new ReportDataSource("DsProjectExtend", data)
				    };

					var config = new ReportConfig
					{
						ReportFilePath = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptProjectExtend.rdlc")
					};

					return new ReportResult(config, reportDataSource);
				}
			}
			catch (Exception exception)
			{
				ViewBag.Message = Messages.Failed(MessageType.Create.ToString(), exception.Message);
			}
			ViewBag.Message = Messages.InvalidInput(MessageType.Create.ToString());
			return View();
		}
    }
}