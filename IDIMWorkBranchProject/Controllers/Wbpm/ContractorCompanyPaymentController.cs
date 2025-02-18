using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
	public class ContractorCompanyPaymentController : BaseController
    {
        private readonly IContractorCompanyPaymentService _contractorCompanyPaymentService;
        private readonly IMapper _mapper;
        private readonly IProjectWorkService _projectWorkService;
        private readonly IADPReceivePaymentService _AdpReceivePaymentService;

        public ContractorCompanyPaymentController(IActivityLogService activityLogService, IContractorCompanyPaymentService contractorCompanyPaymentService, IMapper mapper, IProjectWorkService projectWorkService, IADPReceivePaymentService adpReceivePaymentService) : base(activityLogService)
        {
            _contractorCompanyPaymentService = contractorCompanyPaymentService;
            _mapper = mapper;
            _projectWorkService = projectWorkService;
            _AdpReceivePaymentService = adpReceivePaymentService;
        }

        // GET: ContractorCompanyPayment
        public ActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> Create(int id)
        {
            var projectWork = await _projectWorkService.GetByIdAsync(id);
            var adprecievePayment = await _AdpReceivePaymentService.GetByProjectWorkIdAsync(id);
           
            var model = new ContractorCompanyPaymentVm
            {
                ProjectWorkId = projectWork.ProjectWorkId,
                ProjectWorkTitle = projectWork.ProjectWorkTitle,
                
                EstimatedCost = projectWork.EstimatedCost,
                TotalWithdrawFromMinistry = adprecievePayment.Sum(x => x.BillPaidAmount),
                TotalWithdrawPercent = adprecievePayment.Sum(x => x.BillPaidPer),
                WorkStarted = projectWork.WorkStartDate,
                WorkEnded = projectWork.WorkEndDate,
                ConstructionCompany = projectWork.ConstructionCompany.FirmNameB,
                ProgressPer = adprecievePayment.Sum(x => x.BillPaidPer),
                ProgressAmount = adprecievePayment.Sum(x => x.BillPaidAmount),






            };
            return View(model);
        }


        [HttpPost]
        public async Task<ActionResult> Create(ContractorCompanyPaymentVm model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
                    return View(model);
                }

                var entity = _mapper.Map<ContractorCompanyPayment>(model);
                await _contractorCompanyPaymentService.CreateAsync(entity);
                TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                return RedirectToAction("details/" + model.ProjectWorkId, "ProjectWork");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), exception.Message);
                return View(model);
            }
        }

    }
}