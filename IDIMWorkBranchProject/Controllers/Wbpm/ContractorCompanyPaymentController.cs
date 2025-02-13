using AutoMapper;

using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;

using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
	public class ContractorCompanyPaymentController : BaseController
    {
        private readonly IContractorCompanyPaymentService _contractorCompanyPaymentService;
        private readonly IMapper _mapper;

		public ContractorCompanyPaymentController(IActivityLogService activityLogService, IContractorCompanyPaymentService contractorCompanyPaymentService, IMapper mapper) : base(activityLogService)
		{
			_contractorCompanyPaymentService = contractorCompanyPaymentService;
			_mapper = mapper;
		}

		// GET: ContractorCompanyPayment
		public ActionResult Index()
        {
            return View();
        }
    }
}