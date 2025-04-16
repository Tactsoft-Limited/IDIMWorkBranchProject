using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Extentions.ReportHealper;
using IDIMWorkBranchProject.Extentions.ReportHelper;
using IDIMWorkBranchProject.Services.Report;
using IDIMWorkBranchProject.Services.Setup;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Controllers.Report
{
    public class ReportController : Controller
    {
        protected IFiscalYearService FiscalYearService { get; set; }
        protected IGeneralInformationService GeneralInformationService { get; set; }
        protected IReportService _reportService { get; set; }
        protected IUnitService UnitService { get; set; }
        private readonly IDIMDBEntities _dbContext;

        public ReportController(
            IFiscalYearService fiscalYearService,
            IGeneralInformationService generalInformationService,
            IUnitService unitService, IReportService reportService,
            IDIMDBEntities dbContext)
        {
            FiscalYearService = fiscalYearService;
            GeneralInformationService = generalInformationService;
            UnitService = unitService;
            _reportService = reportService;
            _dbContext = dbContext;
        }
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }




        //public ActionResult ProjectStatus()
        //{
        //    string conString = ConfigurationManager.ConnectionStrings["IDIMDBConnectionString"].ConnectionString;

        //    string connetionString = null;
        //    SqlConnection connection;
        //    SqlDataAdapter adapter;
        //    SqlCommand command = new SqlCommand();
        //    DataSet dataset = new DataSet();

        //    connetionString = conString;
        //    connection = new SqlConnection(connetionString);

        //    connection.Open();
        //    command.Connection = connection;
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.CommandText = "sp_ProjectStatus";
        //    //command.Parameters.AddWithValue("@CategoryId", model.ProductCategoryId);
        //    adapter = new SqlDataAdapter(command);
        //    adapter.Fill(dataset);
        //    connection.Close();

        //    DataTable table = new DataTable();

        //    table = dataset.Tables[0];


        //    ViewData["PivotDataTable"] = table;

        //    return View();

        //}

        public ActionResult ProjectStatus()
        {
            string conString = ConfigurationManager.ConnectionStrings["IDIMDBConnectionString"].ConnectionString;
            var dataset = new DataSet();
            using (var connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand("sp_ProjectStatus", connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                adapter.Fill(dataset);
            }
            DataTable table = dataset.Tables[0];
            ViewData["PivotDataTable"] = table;

            return View();
        }

        public async Task<ActionResult> PrintContactAgreement(int id)
        {

            try
            {
                var data = await _reportService.GetContractAgreementAsync(id);

                var reportDataSource = new List<ReportDataSource>
                {
                    new ReportDataSource("DsContractAgreement", data)
                };

                var config = new ReportConfig
                {
                    ReportFilePath = Path.Combine(Server.MapPath("~/Report/rdlc"), "ContractAgreementReport.rdlc")
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


        public async Task<ActionResult> PrintWorkOrder(int id)
        {

            try
            {
                var data = await _reportService.GetWorkOrderAsync(id);

                var reportDataSource = new List<ReportDataSource>
                {
                    new ReportDataSource("DsWorkOrder", data)
                };

                var config = new ReportConfig
                {
                    ReportFilePath = Path.Combine(Server.MapPath("~/Report/rdlc"), "ViewWorkOrderReport.rdlc"),
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

        public async Task<ActionResult> PrintFinalBillPayment(int id)
        {

            try
            {
                var data = await _reportService.GetFinalBillPaymentAsync(id);

                var reportDataSource = new List<ReportDataSource>
                {
                    new ReportDataSource("DsFinalBillPayment", data)
                };

                var config = new ReportConfig
                {
                    ReportFilePath = Path.Combine(Server.MapPath("~/Report/rdlc"), "FinalBillPaymentReport.rdlc"),
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

        public async Task<ActionResult> PrintFurnitureBillPayment(int id)
        {

            try
            {
                var data = await _reportService.GetFurnitureBillPaymentAsync(id);

                var reportDataSource = new List<ReportDataSource>
                {
                    new ReportDataSource("DsFurnitureBillPayment", data)
                };

                var config = new ReportConfig
                {
                    ReportFilePath = Path.Combine(Server.MapPath("~/Report/rdlc"), "FurnitureBillPaymentReport.rdlc"),
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

        public async Task<ActionResult> PrintCollateralReturn(int id)
        {

            try
            {
                var data = await _reportService.GetCollateralReturnAsync(id);

                var reportDataSource = new List<ReportDataSource>
                {
                    new ReportDataSource("DsCollateralReturn", data)
                };

                var config = new ReportConfig
                {
                    ReportFilePath = Path.Combine(Server.MapPath("~/Report/rdlc"), "CollateralReturnReport.rdlc"),
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