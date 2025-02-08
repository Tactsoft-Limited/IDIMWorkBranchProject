using IDIMWorkBranchProject.Services;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.WBP
{
    public class ContractorBillPaymentController : BaseController
    {
        public ContractorBillPaymentController(IActivityLogService activityLogService) : base(activityLogService)
        {
        }

        // GET: ContractorBillPayment
        public ActionResult Index()
        {
            return View();
        }
    }
}