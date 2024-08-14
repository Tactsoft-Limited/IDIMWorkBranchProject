using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.WBP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.IO;
using IDIMWorkBranchProject.Models.Report;
using BGB.Data.SqlViews.Pm;
using BGB.Data.Database;
using IDIMWorkBranchProject.Extentions.Healper;
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
            string type = "PDF";
            int subProjectid = (model.SubProjectId != null) ? Convert.ToInt32(model.SubProjectId) : 0;
            var reportPath = Path.Combine(Server.MapPath("~/Report/rdlc"), "rptProjectBillPayment.rdlc");
            var data = _dbContext.ViewExtendeds.Where(i => i.SubProjectId == subProjectid).ToList();
            return ReportHelper.GenerateReport(reportPath, "DsBillPayment", data, false, type);
        }
    }
}