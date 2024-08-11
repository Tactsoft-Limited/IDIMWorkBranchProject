using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Extentions.Collections.Select2;
using IDIMWorkBranchProject.Services.Setup;

namespace IDIMWorkBranchProject.Controllers.User
{
    public class GeneralInformationController : Controller
    {
        protected IGeneralInformationService GeneralInformationService { get; set; }

        public GeneralInformationController(IGeneralInformationService generalInformationService)
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