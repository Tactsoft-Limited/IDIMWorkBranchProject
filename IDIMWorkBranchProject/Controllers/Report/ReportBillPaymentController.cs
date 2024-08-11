using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.WBP;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using IDIMWorkBranchProject.Entity;
using Microsoft.Reporting.WebForms;
using System.IO;
using IDIMWorkBranchProject.Models.WBP;
using IDIMWorkBranchProject.Models.Report;
namespace IDIMWorkBranchProject.Controllers.Report
{
    public class ReportBillPaymentController : Controller
    {
        protected IFiscalYearService FiscalYearService { get; set; }
        protected IGeneralInformationService GeneralInformationService { get; set; }
      
        protected ISubProjectService SubProjectService { get; set; }
        protected IConstructionFirmService ConstructionFirmService { get; set; }

        public ReportBillPaymentController(IFiscalYearService fiscalYearService,

            IGeneralInformationService generalInformationService,
       
            ISubProjectService subProjectService
        )
        {
         
            GeneralInformationService = generalInformationService;
            FiscalYearService = fiscalYearService;

            SubProjectService = subProjectService;
         
        }
        public async Task<ActionResult> Index()
        {
            var model = new ReportFilterVm
            {
                SubProjectDropdown = await SubProjectService.GetDropdownAsync(),
                FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(),
            };

            return View(model);
        }
        
        [HttpPost]
        public ActionResult Index(ReportFilterVm model)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report/rdlc"), "rptProjectBillPayment.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View();
            }
            List<ViewProjectBillPayment> lst = new List<ViewProjectBillPayment>();
            using (IDIMDBEntities db = new IDIMDBEntities())
            {
                int subProjectid = (model.SubProjectId != null) ? Convert.ToInt32(model.SubProjectId) : 0;

                lst = (from x in db.ViewProjectBillPayments.Where(f => f.SubProjectId == subProjectid)
                       select x).ToList();
                //lst = db.ViewProjectPaymentReceiptRpts.ToList();

            }
            ReportDataSource rd = new ReportDataSource("DsBillPayment", lst);
            lr.DataSources.Add(rd);
            string reportType = "Pdf";
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>.50in</MarginLeft>" +
                "  <MarginRight>.30in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            //Render the report
            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension);
            return File(renderedBytes, mimeType);
        }
    }
}