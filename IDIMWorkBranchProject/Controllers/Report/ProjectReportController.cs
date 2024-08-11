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
    public class ProjectReportController : Controller
    {
        protected IFiscalYearService FiscalYearService { get; set; }
        protected IGeneralInformationService GeneralInformationService { get; set; }
        protected IProjectService ProjectService { get; set; }
        protected IUnitService UnitService { get; set; }
        protected IProjectProblemService ProjectProblemService { get; set; }
        protected ISubProjectService SubProjectService { get; set; }
        protected IConstructionFirmService ConstructionFirmService { get; set; }

        public ProjectReportController(
            IFiscalYearService fiscalYearService,
            IGeneralInformationService generalInformationService,
            IProjectService projectService,
            IUnitService unitService,
            IProjectProblemService projectProblemService,
            ISubProjectService subProjectService,
            IConstructionFirmService constructionFirmService
        )
        {
            FiscalYearService = fiscalYearService;
            GeneralInformationService = generalInformationService;
            ProjectService = projectService;
            UnitService = unitService;
            ProjectProblemService = projectProblemService;
            SubProjectService = subProjectService;
            ConstructionFirmService = constructionFirmService;
        }
        // GET: ProjectReport
        public async Task<ActionResult> Index()
        {
            var model = new ReportFilterVm
            {
     
                FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(),
            
            };

            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Index(ReportFilterVm model)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report/rdlc"), "rptProjectList.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View();
            }
            List<ViewProjectList> lst = new List<ViewProjectList>();
            using (IDIMDBEntities db = new IDIMDBEntities())
            {
                int fiscalYearId =Convert.ToInt32( model.FiscalYearId);

                lst = (from x in db.ViewProjectLists.Where(f => f.FiscalYearId == fiscalYearId)
                       select x).ToList();
                //lst = db.ViewProjectPaymentReceiptRpts.ToList();

            }
            ReportDataSource rd = new ReportDataSource("DsProjectList", lst);
            lr.DataSources.Add(rd);
            string reportType = "Pdf";
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>11in</PageWidth>" +
                "  <PageHeight>8.5in</PageHeight>" +
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