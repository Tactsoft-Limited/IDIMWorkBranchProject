using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Dashboard;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers
{
    public class DashboardController : BaseController
    {
        protected IDashboardService DashboardService { get; set; }
        public DashboardController(IActivityLogService activityLogService, IDashboardService dashboardService) : base(activityLogService)
        {
            DashboardService = dashboardService;
        }

        public async Task<ActionResult> Index()
        {
            var model = await DashboardService.GetAll();
            // ViewBag.ProjectList = await DashboardService.ProjectList();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}