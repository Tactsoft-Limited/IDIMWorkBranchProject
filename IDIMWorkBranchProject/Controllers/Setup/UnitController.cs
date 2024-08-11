using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Services.Setup;

namespace IDIMWorkBranchProject.Controllers.Setup
{
    public class UnitController : Controller
    {
        protected IUnitService UnitService { get; set; }

        public UnitController(
            IUnitService unitService)
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
