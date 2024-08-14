using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Setup;

namespace IDIMWorkBranchProject.Controllers.Setup
{
    public class UnitController : BaseController
    {
        protected IUnitService UnitService { get; set; }
        public UnitController(IActivityLogService activityLogService, IUnitService unitService) : base(activityLogService)
        {
            UnitService = unitService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public async Task<ActionResult> List()
        {
            var list = await UnitService.GetAllAsync();

            return View(list);
        }

        public ActionResult Get()
        {
            return View();
        }
    }
}
