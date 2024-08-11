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
    public class ReportProblemController : Controller
    {
        protected IFiscalYearService FiscalYearService { get; set; }
        protected IGeneralInformationService GeneralInformationService { get; set; }

        protected ISubProjectService SubProjectService { get; set; }
        protected IConstructionFirmService ConstructionFirmService { get; set; }

        public ReportProblemController(IFiscalYearService fiscalYearService,

            IGeneralInformationService generalInformationService,

            ISubProjectService subProjectService
        )
        {

            GeneralInformationService = generalInformationService;
            FiscalYearService = fiscalYearService;

            SubProjectService = subProjectService;

        }
        // GET: ProjectReport
        //public async Task<ActionResult> Index()
        //{
        //    var model = new ReportFilterVm
        //    {

        //        SubProjectDropdown = await SubProjectService.GetDropdownAsync(),
        //        FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(),
        //    };

        //    return View(model);
        //}
        //[HttpPost]
        public async Task<ActionResult> Index(int id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report/rdlc"), "RDLCProblem.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("ProjectExtend");
            }
            List<ViewProjectProblem> lst = new List<ViewProjectProblem>();
            using (IDIMDBEntities db = new IDIMDBEntities())
            {

                lst = (from x in db.ViewProjectProblems.Where(f => f.SubProjectId == id)
                       select x).ToList();
            }
            ReportDataSource rd = new ReportDataSource("DSProblem", lst);
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