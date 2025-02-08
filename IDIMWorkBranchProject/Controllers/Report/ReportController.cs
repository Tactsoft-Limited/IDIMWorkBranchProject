using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.WBP;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

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
        private readonly IDIMDBEntities _dbContext;

        public ReportController(
            IFiscalYearService fiscalYearService,
            IGeneralInformationService generalInformationService,
            IProjectService projectService,
            IUnitService unitService,
            IProjectProblemService projectProblemService,
            ISubProjectService subProjectService,
            IConstructionFirmService constructionFirmService,
            IConsultantService consultantService,
            IDIMDBEntities dbContext)
        {
            FiscalYearService = fiscalYearService;
            GeneralInformationService = generalInformationService;
            ProjectService = projectService;
            UnitService = unitService;
            ProjectProblemService = projectProblemService;
            SubProjectService = subProjectService;
            ConstructionFirmService = constructionFirmService;
            ConsultantService = consultantService;
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
            DataSet dataset = new DataSet();
            using (SqlConnection connection = new SqlConnection(conString))
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

    }
}