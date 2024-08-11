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
    public class ReportController : Controller
    {
        protected IFiscalYearService FiscalYearService { get; set; }
        protected IGeneralInformationService GeneralInformationService { get; set; }
        protected IProjectService ProjectService { get; set; }
        protected IUnitService UnitService { get; set; }
        protected IProjectProblemService ProjectProblemService { get; set; }
        protected ISubProjectService SubProjectService { get; set; }
        protected IConstructionFirmService ConstructionFirmService { get; set; }
        protected IConsultantService ConsultantService { get; set; }

        public ReportController(
            IFiscalYearService fiscalYearService,
            IGeneralInformationService generalInformationService,
            IProjectService projectService,
            IUnitService unitService,
            IProjectProblemService projectProblemService,
            ISubProjectService subProjectService,
            IConstructionFirmService constructionFirmService,
            IConsultantService consultantService
        )
        {
            FiscalYearService = fiscalYearService;
            GeneralInformationService = generalInformationService;
            ProjectService = projectService;
            UnitService = unitService;
            ProjectProblemService = projectProblemService;
            SubProjectService = subProjectService;
            ConstructionFirmService = constructionFirmService;
            ConsultantService = consultantService;
        }
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> ProjectInformation()
        {

            var model = new ProjectSearchVm
            {
                AuthorizeUnitDropdown = await UnitService.GetDropDownAsync(),
                FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(),
                Projects = await ProjectService.GetByAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ProjectInformation(ProjectSearchVm model)
        {
            model.AuthorizeUnitDropdown = await UnitService.GetDropDownAsync(model.AuthorizeUnitId);
            model.FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(model.FiscalYearId);
            model.Projects = await ProjectService.GetByAsync(model);

            return View(model);
        }


        public ActionResult ProjectInformationRpt(int id, string type)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report/rdlc"), "rptProjectInformation.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("ProjectInformation");
            }
            List<ViewProjectInformationRpt> lst = new List<ViewProjectInformationRpt>();
            using (IDIMDBEntities db = new IDIMDBEntities())
            {

                lst = (from x in db.ViewProjectInformationRpts.Where(f => f.ProjectId == id)
                    select x).ToList();
            }
            ReportDataSource rd = new ReportDataSource("DsWorkBranch", lst);
            lr.DataSources.Add(rd);
            string reportType = type;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>" + type + "</OutputFormat>" +
                "  <PageWidth>8.27in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>.75in</MarginLeft>" +
                "  <MarginRight>.30in</MarginRight>" +
                "  <MarginBottom>0.40in</MarginBottom>" +
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

        public async Task<ActionResult> SubProject()
        {

            var model = new ProjectSearchVm
            {
                AuthorizeUnitDropdown = await UnitService.GetDropDownAsync(),
                FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(),
                Projects = await ProjectService.GetByAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> SubProject(ProjectSearchVm model)
        {
            model.AuthorizeUnitDropdown = await UnitService.GetDropDownAsync(model.AuthorizeUnitId);
            model.FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(model.FiscalYearId);
            model.Projects = await ProjectService.GetByAsync(model);

            return View(model);
        }


        public ActionResult SubProjectRpt(int id, string type)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptProjectWiseSubProject.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("ProjectInformation");
            }
            List<ViewProjectWiseSubProjectRpt> lst = new List<ViewProjectWiseSubProjectRpt>();
            using (IDIMDBEntities db = new IDIMDBEntities())
            {

                lst = (from x in db.ViewProjectWiseSubProjectRpts.Where(f => f.ProjectId == id)
                    select x).ToList();
            }
            ReportDataSource rd = new ReportDataSource("DsSubProject", lst);
            lr.DataSources.Add(rd);
            string reportType = type;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>" + type + "</OutputFormat>" +
                "  <PageWidth>11.69in</PageWidth>" +
                "  <PageHeight>8.27in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>.75in</MarginLeft>" +
                "  <MarginRight>.30in</MarginRight>" +
                "  <MarginBottom>0.40in</MarginBottom>" +
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

        public async Task<ActionResult> ProjectStatus()
        {
            string conString = ConfigurationManager.ConnectionStrings["IDIMDBConnectionString"].ConnectionString;

            string connetionString = null;
            SqlConnection connection;
            SqlDataAdapter adapter;
            SqlCommand command = new SqlCommand();
            DataSet dataset = new DataSet();

            connetionString = conString;
            connection = new SqlConnection(connetionString);

            connection.Open();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_ProjectStatus";
            //command.Parameters.AddWithValue("@CategoryId", model.ProductCategoryId);
            adapter = new SqlDataAdapter(command);
            adapter.Fill(dataset);
            connection.Close();

            DataTable table = new DataTable();

            table = dataset.Tables[0];


            ViewData["PivotDataTable"] = table;

            return View();



        }
        public async Task<ActionResult> ProjectProblem()
        {
            var model = new SubProjectSearchVm
            {
                UnitDropdown = await UnitService.GetDropDownAsync(),
                ConstructionFirmDropdown = await ConstructionFirmService.GetDropdownAsync(),
                SubProjects = await SubProjectService.GetByAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ProjectProblem(SubProjectSearchVm model)
        {
            model.UnitDropdown = await UnitService.GetDropDownAsync(model.UnitId);
            model.ConstructionFirmDropdown = await ConstructionFirmService.GetDropdownAsync(model.ConstructionFirmId);
            model.SubProjects = await SubProjectService.GetByAsync(model);

            return View(model);
        }




        public ActionResult ProjectProblemRpt(int id, string type = "PDF")
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report/rdlc"), "RDLCProblem.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("ProjectProblem");
            }
            List<ViewProjectSubProjectDetailsRpt> lst = new List<ViewProjectSubProjectDetailsRpt>();
            using (IDIMDBEntities db = new IDIMDBEntities())
            {

                lst = (from x in db.ViewProjectSubProjectDetailsRpts.Where(f => f.SubProjectId == id)
                    select x).ToList();
            }
            ReportDataSource rd = new ReportDataSource("DSProblem", lst);
            lr.DataSources.Add(rd);

            string reportType = type;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
                "  <DeviceInfo>" +
                "  <OutputFormat>" + type + "</OutputFormat>" +
                "  <PageWidth>8.27in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>.75in</MarginLeft>" +
                "  <MarginRight>.30in</MarginRight>" +
                "  <MarginBottom>0.40in</MarginBottom>" +
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


        public ActionResult SubprojectBill(int id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report/rdlc"), "RDLCBill.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("SubprojectBill");
            }
            List<ViewProjectSubProjectDetailsRpt> lst = new List<ViewProjectSubProjectDetailsRpt>();
            using (IDIMDBEntities db = new IDIMDBEntities())
            {

                lst = (from x in db.ViewProjectSubProjectDetailsRpts.Where(f => f.SubProjectId == id)
                    select x).ToList();
            }
            ReportDataSource rd = new ReportDataSource("DSBill", lst);
            lr.DataSources.Add(rd);
            string type = "PDF";
            string reportType = type;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>" + type + "</OutputFormat>" +
                "  <PageWidth>8.27in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>.75in</MarginLeft>" +
                "  <MarginRight>.30in</MarginRight>" +
                "  <MarginBottom>0.40in</MarginBottom>" +
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


        public ActionResult SubprojectExtend(int id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptProjectExtend.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("ProjectExtend");
            }
            List<ViewProjectSubProjectDetailsRpt> lst = new List<ViewProjectSubProjectDetailsRpt>();
            using (IDIMDBEntities db = new IDIMDBEntities())
            {

                lst = (from x in db.ViewProjectSubProjectDetailsRpts.Where(f => f.SubProjectId == id)
                    select x).ToList();
            }
            ReportDataSource rd = new ReportDataSource("DsProjectExtend", lst);
            lr.DataSources.Add(rd);
            string type = "PDF";
            string reportType = type;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>" + type + "</OutputFormat>" +
                "  <PageWidth>8.27in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>.75in</MarginLeft>" +
                "  <MarginRight>.30in</MarginRight>" +
                "  <MarginBottom>0.40in</MarginBottom>" +
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




        public ActionResult SubprojectProblem(int id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptProjectProblem.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("ProjectProblem");
            }
            List<ViewProjectSubProjectDetailsRpt> lst = new List<ViewProjectSubProjectDetailsRpt>();
            using (IDIMDBEntities db = new IDIMDBEntities())
            {

                lst = (from x in db.ViewProjectSubProjectDetailsRpts.Where(f => f.ProjectId == id)
                    select x).ToList();
            }
            ReportDataSource rd = new ReportDataSource("DsProjectProblem", lst);
            lr.DataSources.Add(rd);
            string type = "PDF";
            string reportType = type;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>" + type + "</OutputFormat>" +
                "  <PageWidth>8.27in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>.75in</MarginLeft>" +
                "  <MarginRight>.30in</MarginRight>" +
                "  <MarginBottom>0.40in</MarginBottom>" +
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

        public ActionResult SubProjectStatusRpt(int id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptProjectStatus.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("SubProjectStatus");
            }
            List<ViewProjectSubProjectDetailsRpt> lst = new List<ViewProjectSubProjectDetailsRpt>();
            using (IDIMDBEntities db = new IDIMDBEntities())
            {

                lst = (from x in db.ViewProjectSubProjectDetailsRpts.Where(f => f.SubProjectId == id)
                    select x).ToList();
            }
            ReportDataSource rd = new ReportDataSource("DsProjectStatus", lst);
            lr.DataSources.Add(rd);
            string type = "PDF";
            string reportType = type;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>" + type + "</OutputFormat>" +
                "  <PageWidth>8.27in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>.75in</MarginLeft>" +
                "  <MarginRight>.30in</MarginRight>" +
                "  <MarginBottom>0.40in</MarginBottom>" +
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






        public async Task<ActionResult> ProjectPaymentReceipt()
        {
            var model = new ReportFilterVm
            {
                FiscalYearDropdown = await FiscalYearService.GetDropdownAsync()
            };
            return View(model);
        }


        public ActionResult ProjectPaymentReceiptRpt()
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptProjectWiseReceiptPayment.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("ProjectPaymentReceipt");
            }
            List<ViewProjectPaymentReceiptRpt> lst = new List<ViewProjectPaymentReceiptRpt>();
            using (IDIMDBEntities db = new IDIMDBEntities())
            {
                int fiscalYearId = Convert.ToInt32(Request.Form["FiscalYearId"]);

                lst = (from x in db.ViewProjectPaymentReceiptRpts.Where(f => f.FiscalYearId == fiscalYearId)
                    select x).ToList();
                //lst = db.ViewProjectPaymentReceiptRpts.ToList();

            }
            ReportDataSource rd = new ReportDataSource("DsProjectPaymentReceipt", lst);
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


        #region NasirWorkForReport

        public async Task<ActionResult> ProjectPaymentDeposit()
        {
            var model = new ReportFilterVm
            {
                ConstructionFirmDropdown = await ConstructionFirmService.GetDropdownAsync(),
                SubProjectDropdown = await SubProjectService.GetDropdownAsync()
            };
            return View(model);
        }

        public ActionResult ProjectPaymentDepositRpt(ReportFilterVm model)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptProjectPaymentDeposit.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("ProjectPaymentDeposit");
            }
            List<ReportDataVm> lst = new List<ReportDataVm>();
            using (IDIMDBEntities db = new IDIMDBEntities())
            {
                int subProjectId = (model.SubProjectId != null) ? Convert.ToInt32(model.SubProjectId) : 0;
                int constructorFirmId = (model.ConstructionFirmId != null) ? Convert.ToInt32(model.ConstructionFirmId) : 0;
                string letterno = (model.Letterno != null) ? model.Letterno : "";


                var Param1 = new SqlParameter("@SubProjectId", subProjectId);
                var Param2 = new SqlParameter("@ConstructorFirmId", constructorFirmId);
                var Param3 = new SqlParameter("@Letterno", letterno);

                string SP_SQL = "[pm].[GetProjectPaymentDepositRpt] @SubProjectId, @ConstructorFirmId, @Letterno";

                lst = db.Database.SqlQuery<ReportDataVm>(SP_SQL, Param1, Param2, Param3).ToList();
            }
            ReportDataSource rd = new ReportDataSource("ProjectPaymentDepositDataSet", lst);
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

        public async Task<ActionResult> ConstructionCheckList()
        {
            var model = new ReportFilterVm
            {
                ConstructionFirmDropdown = await ConstructionFirmService.GetDropdownAsync(),
                SubProjectDropdown = await SubProjectService.GetDropdownAsync(),
                FiscalYearDropdown = await FiscalYearService.GetDropdownAsync()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ConstructionCheckList(ReportFilterVm model)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptConstructionCheckList.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("ConstructionCheckList");
            }
            List<ReportDataVm> lst = new List<ReportDataVm>();
            using (IDIMDBEntities db = new IDIMDBEntities())
            {
                int subProjectId = (model.SubProjectId != null) ? Convert.ToInt32(model.SubProjectId) : 0;
                int constructorFirmId = (model.ConstructionFirmId != null) ? Convert.ToInt32(model.ConstructionFirmId) : 0;
                int fiscalYearId = (model.FiscalYearId != null) ? Convert.ToInt32(model.FiscalYearId) : 0;

                var Param1 = new SqlParameter("@SubProjectId", subProjectId);
                var Param2 = new SqlParameter("@ConstructorFirmId", constructorFirmId);
                var Param3 = new SqlParameter("@FiscalYearId", fiscalYearId);

                string SP_SQL = "[pm].[GetConstructionChecklistRpt] @SubProjectId, @ConstructorFirmId, @FiscalYearId";

                lst = db.Database.SqlQuery<ReportDataVm>(SP_SQL, Param1, Param2, Param3).ToList();
            }
            ReportDataSource rd = new ReportDataSource("ConstructionCheckListDataSet", lst);
            lr.DataSources.Add(rd);
            string reportType = "Pdf";
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.27in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>0.10in</MarginLeft>" +
                "  <MarginRight>0.10in</MarginRight>" +
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

        public async Task<ActionResult> ConsultancyFees()
        {
            var model = new ReportFilterVm
            {
                ConstructionFirmDropdown = await ConstructionFirmService.GetDropdownAsync(),
                ConsultantDropdown = await ConsultantService.GetDropdownAsync(),
                FiscalYearDropdown = await FiscalYearService.GetDropdownAsync()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ConsultancyFees(ReportFilterVm model)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptConsultantFeeAmount.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("ConsultancyFees");
            }
            List<ReportDataVm> lst = new List<ReportDataVm>();
            using (IDIMDBEntities db = new IDIMDBEntities())
            {
                int constructorFirmId = (model.ConstructionFirmId != null) ? Convert.ToInt32(model.ConstructionFirmId) : 0;
                int consultantId = (model.ConsultantId != null) ? Convert.ToInt32(model.ConsultantId) : 0;
                int fiscalYearId = (model.FiscalYearId != null) ? Convert.ToInt32(model.FiscalYearId) : 0;

                var Param1 = new SqlParameter("@ConstructorFirmId", constructorFirmId);
                var Param2 = new SqlParameter("@ConsultantId", constructorFirmId);
                var Param3 = new SqlParameter("@FiscalYearId", fiscalYearId);

                string SP_SQL = "[pm].[GetConsultantFeeAmountRpt] @ConstructorFirmId, @ConsultantId, @FiscalYearId";

                lst = db.Database.SqlQuery<ReportDataVm>(SP_SQL, Param1, Param2, Param3).ToList();
            }
            ReportDataSource rd = new ReportDataSource("ConsultancyFeesDataSet", lst);
            lr.DataSources.Add(rd);
            string reportType = "Pdf";
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.27in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>0.10in</MarginLeft>" +
                "  <MarginRight>0.10in</MarginRight>" +
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

        public async Task<ActionResult> ConstructionReport()
        {
            var model = new ReportFilterVm
            {
                ConsultantDropdown = await ConsultantService.GetDropdownAsync(),
                FiscalYearDropdown = await FiscalYearService.GetDropdownAsync()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ConstructionReport(ReportFilterVm model)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptConstructionDetails.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("ConsultancyFees");
            }
            List<ReportDataVm> lst = new List<ReportDataVm>();
            using (IDIMDBEntities db = new IDIMDBEntities())
            {
                int consultantId = (model.ConsultantId != null) ? Convert.ToInt32(model.ConsultantId) : 0;
                int fiscalYearId = (model.FiscalYearId != null) ? Convert.ToInt32(model.FiscalYearId) : 0;

                var Param1 = new SqlParameter("@ConsultantId", consultantId);
                var Param2 = new SqlParameter("@FiscalYearId", fiscalYearId);

                string SP_SQL = "[pm].[GetDetailsOfConstructionRpt] @ConsultantId, @FiscalYearId";

                lst = db.Database.SqlQuery<ReportDataVm>(SP_SQL, Param1, Param2).ToList();
            }
            ReportDataSource rd = new ReportDataSource("ConstructionDetailsDataSet", lst);
            lr.DataSources.Add(rd);
            string reportType = "Pdf";
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.27in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>0.10in</MarginLeft>" +
                "  <MarginRight>0.10in</MarginRight>" +
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

        public async Task<ActionResult> YearlyConstructionReport()
        {
            var model = new ReportFilterVm
            {
                FiscalYearDropdown = await FiscalYearService.GetDropdownAsync()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult YearlyConstructionReport(ReportFilterVm model)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptYearlyConstructionDetails.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("ConsultancyFees");
            }
            List<ReportDataVm> lst = new List<ReportDataVm>();
            using (IDIMDBEntities db = new IDIMDBEntities())
            {
                int fiscalYearId = (model.FiscalYearId != null) ? Convert.ToInt32(model.FiscalYearId) : 0;

                var Param1 = new SqlParameter("@FiscalYearId", fiscalYearId);

                string SP_SQL = "[pm].[GetYearlyConstructionDetailsRpt] @FiscalYearId";

                lst = db.Database.SqlQuery<ReportDataVm>(SP_SQL, Param1).ToList();
            }
            ReportDataSource rd = new ReportDataSource("YearlyConstructionDetailsDataSet", lst);
            lr.DataSources.Add(rd);
            string reportType = "Pdf";
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>11.69in</PageWidth>" +
                "  <PageHeight>8.27in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>0.10in</MarginLeft>" +
                "  <MarginRight>0.10in</MarginRight>" +
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

        public async Task<ActionResult> FullPaymentByProjectReport()
        {
            var model = new ReportFilterVm
            {
                SubProjectDropdown = await SubProjectService.GetDropdownAsync()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult FullPaymentByProjectReport(ReportFilterVm model)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptFullPaymentByProjectDetails.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("FullPaymentByProjectReport");
            }
            List<ReportDataVm> lst = new List<ReportDataVm>();
            using (IDIMDBEntities db = new IDIMDBEntities())
            {
                int subProjectId = (model.SubProjectId != null) ? Convert.ToInt32(model.SubProjectId) : 0;

                var Param1 = new SqlParameter("@SubProjectId", subProjectId);

                string SP_SQL = "[pm].[GetFullPaymentByProjectRpt] @SubProjectId";

                lst = db.Database.SqlQuery<ReportDataVm>(SP_SQL, Param1).ToList();
            }
            ReportDataSource rd = new ReportDataSource("FullPaymentByProjectDataSet", lst);
            lr.DataSources.Add(rd);
            string reportType = "Excel";
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>Excel</OutputFormat>" +
                "  <PageWidth>8.27in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>0.10in</MarginLeft>" +
                "  <MarginRight>0.10in</MarginRight>" +
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

        public async Task<ActionResult> PartialPaymentByProjectReport()
        {
            var model = new ReportFilterVm
            {
                SubProjectDropdown = await SubProjectService.GetDropdownAsync()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult PartialPaymentByProjectReport(ReportFilterVm model)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report/rdlc"), "RptPartialPaymentByProjectDetails.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("PartialPaymentByProjectReport");
            }
            List<ReportDataVm> lst = new List<ReportDataVm>();
            using (IDIMDBEntities db = new IDIMDBEntities())
            {
                int subProjectId = (model.SubProjectId != null) ? Convert.ToInt32(model.SubProjectId) : 0;

                var Param1 = new SqlParameter("@SubProjectId", subProjectId);

                string SP_SQL = "[pm].[GetPartialPaymentByProjectRpt] @SubProjectId";

                lst = db.Database.SqlQuery<ReportDataVm>(SP_SQL, Param1).ToList();
            }
            ReportDataSource rd = new ReportDataSource("PartialPaymentByProjectDataSet", lst);
            lr.DataSources.Add(rd);
            string reportType = "Excel";
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>Excel</OutputFormat>" +
                "  <PageWidth>8.27in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>0.10in</MarginLeft>" +
                "  <MarginRight>0.10in</MarginRight>" +
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

        #endregion
    }
}