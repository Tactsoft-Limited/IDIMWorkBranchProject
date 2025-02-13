using AutoMapper;
using BGB.Data.Entities.Pm;
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
    public class ContractAgreementController : BaseController
    {
        private readonly IContractAgreementService _contractAgreementService;
        private readonly IConstructionCompanyService _constructionCompanyService;
        private readonly IProjectWorkService _projectWorkService;
        private readonly IMapper _mapper;

        public ContractAgreementController(IActivityLogService activityLogService, IContractAgreementService contractAgreementService, IMapper mapper, IConstructionCompanyService constructionCompanyService, IProjectWorkService projectWorkService) : base(activityLogService)
        {
            _contractAgreementService = contractAgreementService;
            _mapper = mapper;
            _constructionCompanyService = constructionCompanyService;
            _projectWorkService = projectWorkService;
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Create(int id)
        {

            var data = await _projectWorkService.GetByIdAsync(id);
            var model = new ContractAgreementVm
            {
                ProjectWorkId = data.ProjectWorkId,
                ProjectWorkTitle = data.ProjectWorkTitle,
                ConstructionFirm = data.ConstructionCompany.FirmNameB,
                ConstructionCompanyId = data.ConstructionCompanyId,
                AgreementCost = data.EstimatedCost,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ContractAgreementVm model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
                    return View(model);
                }

                var entity = _mapper.Map<ContractAgreement>(model);
                await _contractAgreementService.CreateAsync(entity);
                TempData["Message"] = Messages.Success(MessageType.Create.ToString());

                return RedirectToAction("details/" + model.ProjectWorkId, "ProjectWork");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), exception.Message);

                return View(model);
            }
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var model = _mapper.Map<ContractAgreementVm>(await _contractAgreementService.GetByIdAsync(id));
            model.ProjectWorkTitle = await _projectWorkService.GetProjectWorkTitle(model.ProjectWorkId);
            model.ConstructionFirm = await _constructionCompanyService.GetConstructionFirm(model.ConstructionCompanyId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ContractAgreementVm model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = Messages.InvalidInput(MessageType.Update.ToString());
                    return View(model);
                }

                var entity = _mapper.Map<ContractAgreement>(model);
                await _contractAgreementService.UpdateAsync(entity);

                TempData["Message"] = Messages.Success(MessageType.Update.ToString());
                return RedirectToAction("details/" + model.ProjectWorkId, "ProjectWork");  // Redirect to list after success
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Update.ToString(), exception.InnerException.Message);
                return View(model);
            }
        }

    }
}
