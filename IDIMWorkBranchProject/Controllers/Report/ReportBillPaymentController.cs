using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.WBP;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.IO;
using IDIMWorkBranchProject.Models.Report;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Extentions.ReportHealper;
using IDIMWorkBranchProject.Extentions.ReportHelper;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
namespace IDIMWorkBranchProject.Controllers.Report
{
	public class ReportBillPaymentController : Controller
    {
        protected IFiscalYearService FiscalYearService { get; set; }
        protected IGeneralInformationService GeneralInformationService { get; set; }

        protected ISubProjectService SubProjectService { get; set; }
        protected IConstructionFirmService ConstructionFirmService { get; set; }
        private readonly IDIMDBEntities _dbContext;

        public ReportBillPaymentController(IFiscalYearService fiscalYearService,

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
        public async Task<ActionResult> Index()
        {
            var model = new ReportFilterVm
            {
                SubProjectDropdown = await SubProjectService.GetDropdownAsync(),
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ReportFilterVm model)
        {
            int subProjectid = (model.SubProjectId != null) ? Convert.ToInt32(model.SubProjectId) : 0;
            var data = _dbContext.ViewExtendeds.Where(i => i.SubProjectId == subProjectid).ToList();
			var reportDataSource = new List<ReportDataSource>
            {
	            new ReportDataSource("DsBillPayment", data)
            };

			var config = new ReportConfig
			{
				ReportFilePath = Path.Combine(Server.MapPath("~/Report/rdlc"), "rptProjectBillPayment.rdlc")
			};

			return new ReportResult(config, reportDataSource);
		}
    }
}