using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class FurnitureBillPaymentController : BaseController
    {
        private readonly IFurnitureBillPaymentService _furnitureBillPaymentService;
        private readonly IProjectWorkService _projectWorkService;
        private readonly IContractAgreementService _contractAgreementService;
        private readonly IConstructionCompanyService _constructionCompanyService;
        private readonly IBGBMiscellaneousFundService _bGBMiscellaneousFundService;
        private readonly IContractorCompanyPaymentService _contractorCompanyPaymentService;
        private readonly IMapper _mapper;
        public FurnitureBillPaymentController(IActivityLogService activityLogService, IFurnitureBillPaymentService furnitureBillPaymentService, IMapper mapper, IProjectWorkService projectWorkService, IContractAgreementService contractAgreementService, IConstructionCompanyService constructionCompanyService, IBGBMiscellaneousFundService bGBMiscellaneousFundService, IContractorCompanyPaymentService contractorCompanyPaymentService) : base(activityLogService)
        {
            _furnitureBillPaymentService = furnitureBillPaymentService;
            _mapper = mapper;
            _projectWorkService = projectWorkService;
            _contractAgreementService = contractAgreementService;
            _constructionCompanyService = constructionCompanyService;
            _bGBMiscellaneousFundService = bGBMiscellaneousFundService;
            _contractorCompanyPaymentService = contractorCompanyPaymentService;
        }

        // GET: FurnitureBillPayment
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Create(int id)
        {
            var projectWorks=await _projectWorkService.GetByIdAsync(id);
            var contactAgreements = await _contractAgreementService.GetByProjectWorkIdAsync(projectWorks.ProjectWorkId);
            var bgbMiscellaneousFund = await _bGBMiscellaneousFundService.GetByProjectWorkIdAsync(projectWorks.ProjectWorkId);
            var contractionCompanyPayment = await _contractorCompanyPaymentService.GetByProjectWorkIdAsync(projectWorks.ProjectWorkId);
            var furnitureBillPayment = await _furnitureBillPaymentService.GetByProjectWorkIdAsync(projectWorks.ProjectWorkId);


            var model = new FurnitureBillPaymentVm
            {
                ProjectWorkId = projectWorks.ProjectWorkId,
                ProjectWorkTitleB=projectWorks.ProjectWorkTitleB,
                ConstractorCompanyId =contactAgreements.ConstructionCompanyId,
                ConstractorCompanyName = contactAgreements.ConstructionCompany.FirmNameB,
                DepositsInFund= (bgbMiscellaneousFund.Sum(a => a.Amount) - contractionCompanyPayment.Sum(e => e.FinalPaymentAmount)),
                ConstructionCompanyDropdown = await _constructionCompanyService.GetDropdownAsync(),               
            };
            if (furnitureBillPayment != null)
            {
                model.FurnitureBillPaymentId = furnitureBillPayment.FurnitureBillPaymentId;
                model.AllocationToFurniture = furnitureBillPayment.AllocationToFurniture;
                model.AllocationToFurnitureInWordB = furnitureBillPayment.AllocationToFurnitureInWordB;
                model.DepositedInFund = furnitureBillPayment.DepositedInFund;
                model.DepositedInFundInWordB = furnitureBillPayment.DepositedInFundInWordB;
                model.PaymentAmount = furnitureBillPayment.PaymentAmount;
                model.PaymentAmountInWordB = furnitureBillPayment.PaymentAmountInWordB;
                model.LetterNo = furnitureBillPayment.LetterNo;
                model.QuoteOne = furnitureBillPayment.QuoteOne;
                model.QuoteTwo = furnitureBillPayment.QuoteTwo;
                model.QuoteThree = furnitureBillPayment.QuoteThree;
                model.QuoteFour = furnitureBillPayment.QuoteFour;
                model.ChangedConstractorCompanyId = furnitureBillPayment.ChangedConstractorCompanyId;
                model.ConstructionCompanyDropdown = await _constructionCompanyService.GetDropdownAsync(furnitureBillPayment.ChangedConstractorCompanyId);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FurnitureBillPaymentVm model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
                    model.ConstructionCompanyDropdown = await _constructionCompanyService.GetDropdownAsync(model.ConstractorCompanyId);
                    return View(model);
                }
                if (model.FurnitureBillPaymentId > 0)
                {
                    await _furnitureBillPaymentService.UpdateAsync(_mapper.Map<FurnitureBillPayment>(model));
                    TempData["Message"] = Messages.Success(MessageType.Update.ToString());                   
                }
                else
                {
                    var entity = _mapper.Map<FurnitureBillPayment>(model);
                    await _furnitureBillPaymentService.CreateAsync(entity);
                    TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                }
                return RedirectToAction("details/" + model.ProjectWorkId, "ProjectWork");
            }


            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), exception.Message);
                model.ConstructionCompanyDropdown = await _constructionCompanyService.GetDropdownAsync(model.ConstractorCompanyId);
                return View(model);
            }
        }
    }
}