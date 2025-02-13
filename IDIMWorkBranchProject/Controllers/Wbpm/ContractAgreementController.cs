using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class ContractAgreementController : BaseController
    {
        private readonly IContractAgreementService _contractAgreementService;
        private readonly IProjectWorkService _projectWorkService;
        private readonly IConstructionCompanyService _constructionCompanyService;
        private readonly IMapper _mapper;

        public ContractAgreementController(IActivityLogService activityLogService, IContractAgreementService contractAgreementService, IProjectWorkService projectWorkService, IMapper mapper, IConstructionCompanyService constructionCompanyService) : base(activityLogService)
        {
            _contractAgreementService = contractAgreementService;
            _projectWorkService = projectWorkService;
            _mapper = mapper;
            _constructionCompanyService = constructionCompanyService;
        }

        // GET: ContractAgreement
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Create(int id)
        {
            var data = await _contractAgreementService.GetByIdAsync(id);
            var projectWork = await _projectWorkService.GetByIdAsync(data.ProjectWorkId);
            var model = new ContractAgreementVm();
            if (data != null)
            {
                model.ContractAgreementId = data.ContractAgreementId;
                model.ProjectWorkId = projectWork.ProjectWorkId;
                model.ProjectWorkTitle = projectWork.ProjectWorkTitle;
                model.ConstructionFirm = await _constructionCompanyService.GetConstructionFirm(projectWork.ConstructionCompanyId);
                model.ContractDay = data.ContractDay;
                model.ContractDate = data.ContractDate;
                model.AgreementCost = data.AgreementCost;
                model.AgreementCostInWord = data.AgreementCostInWord;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ContractAgreementVm model)
        {
            try
            {
                if (model.ContractAgreementId > 0)
                    await _contractAgreementService.UpdateAsync(_mapper.Map<ContractAgreement>(model));
                else
                    await _contractAgreementService.CreateAsync(_mapper.Map<ContractAgreement>(model));


                //TODO: Need to Implement Contract Agreement Report

                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}