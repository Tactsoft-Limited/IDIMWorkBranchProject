using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly ISignatoryAuthorityService _signatoryAuthorityService;
        private readonly IMapper _mapper;

        public FurnitureBillPaymentController(
            IActivityLogService activityLogService,
            IFurnitureBillPaymentService furnitureBillPaymentService,
            IMapper mapper,
            IProjectWorkService projectWorkService,
            IContractAgreementService contractAgreementService,
            IConstructionCompanyService constructionCompanyService,
            IBGBMiscellaneousFundService bGBMiscellaneousFundService,
            IContractorCompanyPaymentService contractorCompanyPaymentService,
            ISignatoryAuthorityService signatoryAuthorityService
        ) : base(activityLogService)
        {
            _furnitureBillPaymentService = furnitureBillPaymentService;
            _mapper = mapper;
            _projectWorkService = projectWorkService;
            _contractAgreementService = contractAgreementService;
            _constructionCompanyService = constructionCompanyService;
            _bGBMiscellaneousFundService = bGBMiscellaneousFundService;
            _contractorCompanyPaymentService = contractorCompanyPaymentService;
            _signatoryAuthorityService = signatoryAuthorityService;
        }

        public ActionResult Index() => View();

        public async Task<ActionResult> Create(int id)
        {
            var project = await _projectWorkService.GetByIdAsync(id);
            var agreement = await _contractAgreementService.GetByProjectWorkIdAsync(project.ProjectWorkId);
            var funds = await _bGBMiscellaneousFundService.GetByProjectWorkIdAsync(project.ProjectWorkId);
            var contractorPayments = await _contractorCompanyPaymentService.GetByAllProjectWorkAsync(project.ProjectWorkId);
            var existingPayment = await _furnitureBillPaymentService.GetByProjectWorkIdAsync(project.ProjectWorkId);

            var model = new FurnitureBillPaymentVm
            {
                ProjectWorkId = project.ProjectWorkId,
                ProjectWorkTitleB = project.ProjectWorkTitleB,
                ConstractorCompanyId = agreement.ConstructionCompanyId,
                ConstractorCompanyName = agreement.ConstructionCompany.FirmNameB,
                DepositsInFund = funds.Sum(f => f.Amount) - contractorPayments.Sum(p => p.FinalPaymentAmount)
            };

            if (existingPayment != null)
            {
                _mapper.Map(existingPayment, model);
                model.ConstructionCompanyDropdown = await _constructionCompanyService.GetDropdownAsync(existingPayment.ChangedConstractorCompanyId);
                await PopulateSignatoryDropdowns(model, existingPayment.ChangedConstractorCompanyId);
            }
            else
            {
                model.ConstructionCompanyDropdown = await _constructionCompanyService.GetDropdownAsync();
                await PopulateSignatoryDropdowns(model);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FurnitureBillPaymentVm model)
        {
            if (!ModelState.IsValid)
            {
                SetResponseMessage(DefaultMsg.InvalidInput, ResponseType.Error);
                model.ConstructionCompanyDropdown = await _constructionCompanyService.GetDropdownAsync(model.ConstractorCompanyId);
                await PopulateSignatoryDropdowns(model, model.ChangedConstractorCompanyId);
                return View(model);
            }

            try
            {
                var entity = _mapper.Map<FurnitureBillPayment>(model);

                if (model.FurnitureBillPaymentId > 0)
                {
                    await _furnitureBillPaymentService.UpdateAsync(entity);
                    SetResponseMessage(string.Format(DefaultMsg.UpdateSuccess, "Furniture Bill Payment"), ResponseType.Success);
                }
                else
                {
                    await _furnitureBillPaymentService.CreateAsync(entity);
                    SetResponseMessage(string.Format(DefaultMsg.SaveSuccess, "Furniture Bill Payment"), ResponseType.Success);
                }

                return RedirectToAction("Details", "ProjectWork", new { id = model.ProjectWorkId });
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.SaveFailed, "Furniture Bill Payment", ex.Message), ResponseType.Error);
                model.ConstructionCompanyDropdown = await _constructionCompanyService.GetDropdownAsync(model.ConstractorCompanyId);
                await PopulateSignatoryDropdowns(model, model.ChangedConstractorCompanyId);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _furnitureBillPaymentService.DeleteAsync(id);
                SetResponseMessage(string.Format(DefaultMsg.DeleteSuccess, "Furniture Bill Payment"), ResponseType.Success);
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.DeleteFailed, "Furniture Bill Payment", ex.Message), ResponseType.Error);
            }

            return RedirectToAction("List");
        }


        private async Task PopulateSignatoryDropdowns(FurnitureBillPaymentVm model, int? selectedId = null)
        {
            model.HeadAssistantDropdown = await _signatoryAuthorityService.GetDropdownAsync(selectedId);
            model.ConcernedEngineerDropdown = await _signatoryAuthorityService.GetDropdownAsync(selectedId);
            model.SectionICTDropdown = await _signatoryAuthorityService.GetDropdownAsync(selectedId);
            model.BranchClerkDropdown = await _signatoryAuthorityService.GetDropdownAsync(selectedId);
        }
    }
}
