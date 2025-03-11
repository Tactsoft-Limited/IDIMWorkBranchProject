using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
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
        private readonly IRecruitmentCommitteeService _recruitmentCommitteeService;
        private readonly IMapper _mapper;
        private readonly string fileStorePath = "Documents/ContractAggrementFiles";

        public ContractAgreementController(IActivityLogService activityLogService, IContractAgreementService contractAgreementService, IProjectWorkService projectWorkService, IMapper mapper, IConstructionCompanyService constructionCompanyService, IRecruitmentCommitteeService recruitmentCommitteeService) : base(activityLogService)
        {
            _contractAgreementService = contractAgreementService;
            _projectWorkService = projectWorkService;
            _mapper = mapper;
            _constructionCompanyService = constructionCompanyService;
            _recruitmentCommitteeService = recruitmentCommitteeService;
        }

        // GET: ContractAgreement
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Create(int id)
        {
            var projectWork = await _projectWorkService.GetByIdAsync(id);
            var contractAgreemen = await _contractAgreementService.GetByProjectWorkIdAsync(id);


            var model = new ContractAgreementVm
            {
                ProjectWorkId = projectWork.ProjectWorkId,
                ProjectWorkTitle = projectWork.ProjectWorkTitle,
                ConstructionCompanyId = contractAgreemen?.ConstructionCompanyId ?? 0,
            };
            if (contractAgreemen != null) 
            {
                model.ContractAgreementId = contractAgreemen.ContractAgreementId;
                model.AgreementDate = contractAgreemen.AgreementDate;
                model.ScanDocument = contractAgreemen.ScanDocument;
            }

            await PopulateDropdownsAsync(model, contractAgreemen);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ContractAgreementVm model)
        {
            string fileName = null;
            try
            {
                if (model.ContractAgreementId > 0)
                {
                    if (model.DocumentFile != null && model.DocumentFile.ContentLength > 0)
                    {
                        //Delete Old File
                        FileExtention.DeleteFile(model.ScanDocument, fileStorePath);

                        fileName = FileExtention.UploadFile(model.DocumentFile, fileStorePath);

                        // If file is successfully uploaded, save the file name to the model
                        if (fileName != null)
                        {
                            model.ScanDocument = fileName;
                        }
                        else
                        {
                            TempData["Message"] = Messages.FileUploadFailed(MessageType.Create.ToString());
                            return View(model);
                        }
                    }
                    await _contractAgreementService.UpdateAsync(_mapper.Map<ContractAgreement>(model));
                    TempData["Message"] = Messages.Success(MessageType.Update.ToString());

                }

                else
                {
                    // Step 1: Check if file is uploaded
                    if (model.DocumentFile != null && model.DocumentFile.ContentLength > 0)
                    {
                        fileName = FileExtention.UploadFile(model.DocumentFile, fileStorePath);

                        // If file is successfully uploaded, save the file name to the model
                        if (fileName != null)
                        {
                            model.ScanDocument = fileName;
                        }
                        else
                        {
                            TempData["Message"] = Messages.FileUploadFailed(MessageType.Create.ToString());
                            return View(model);
                        }
                    }
                    await _contractAgreementService.CreateAsync(_mapper.Map<ContractAgreement>(model));
                    TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                }             
                
                return RedirectToAction("details/" + model.ProjectWorkId, "ProjectWork");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), $"An error occurred while processing your request.{exception.InnerException.Message}");
                // Populate dropdowns even if an error occurs
                await PopulateDropdownsAsync(model, null);
                return View(model);
            }

        }

        private async Task PopulateDropdownsAsync(ContractAgreementVm model, ContractAgreement contractAgreement)
        {
            if (contractAgreement != null)
            {
                model.AddDGDropdown = await _recruitmentCommitteeService.GetDropdownAsync(contractAgreement.AddDGId);
                model.DDGDropdown = await _recruitmentCommitteeService.GetDropdownAsync(contractAgreement.DDGId);
                model.ProjectdirectorDropdown = await _recruitmentCommitteeService.GetDropdownAsync(contractAgreement.ProjectDirectorId);
                model.DirectorDropdown = await _recruitmentCommitteeService.GetDropdownAsync(contractAgreement.DirectorId);
                model.ConstructionFirmDropdown=await _constructionCompanyService.GetDropdownAsync(contractAgreement.ConstructionCompanyId);
            }
            else
            {
                model.AddDGDropdown = await _recruitmentCommitteeService.GetDropdownAsync(model.AddDGId);
                model.DDGDropdown = await _recruitmentCommitteeService.GetDropdownAsync(model.DDGId);
                model.ProjectdirectorDropdown = await _recruitmentCommitteeService.GetDropdownAsync(model.ProjectDirectorId);
                model.DirectorDropdown = await _recruitmentCommitteeService.GetDropdownAsync(model.DirectorId);
                model.ConstructionFirmDropdown = await _constructionCompanyService.GetDropdownAsync(model.ConstructionCompanyId);
            }
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _contractAgreementService.GetByIdAsync(id);

            if (entity == null)
            {
                TempData["Message"] = "The requested record was not found.";
                return RedirectToAction("details/" + entity.ProjectWorkId, "ProjectWork");
            }

            var model = _mapper.Map<ContractAgreementVm>(entity);
            model.ProjectWorkTitle = await _projectWorkService.GetProjectWorkTitle(entity.ProjectWorkId);
            return View(model); // Load the delete confirmation view
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(ContractAgreementVm model)
        {
            var entity = await _contractAgreementService.GetByIdAsync(model.ContractAgreementId);
            try
            {

                if (entity == null)
                {
                    TempData["Message"] = "Record Not Found";
                    return RedirectToAction("Details/" + entity.ProjectWorkId, "ProjetWork");
                }

                await _contractAgreementService.DeleteAsync(entity);

                TempData["Message"] = Messages.Success(MessageType.Delete.ToString());
                return RedirectToAction("Details/" + entity.ProjectWorkId, "ProjectWork");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), exception.InnerException?.Message);
                return RedirectToAction("Details/" + entity.ProjectWorkId, "ProjectWork"); // Avoids null reference
            }
        }
    }
}