using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Extentions.Collections.Select2;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Setup;

namespace IDIMWorkBranchProject.Controllers.User
{
    public class GeneralInformationController : BaseController
    {
        protected IGeneralInformationService GeneralInformationService { get; set; }

        public GeneralInformationController(IActivityLogService activityLogService, IGeneralInformationService generalInformationService) : base(activityLogService)
        {
            GeneralInformationService = generalInformationService;
        }

        // GET: GeneralInformation
        public async Task<ActionResult> Get(Select2Param id)
        {
            var generalInformation = await GeneralInformationService.GetBySelect2Async(id);

            return Json(generalInformation, JsonRequestBehavior.AllowGet);
        }
    }
}