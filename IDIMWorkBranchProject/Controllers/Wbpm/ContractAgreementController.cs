using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
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
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new ContractAgreementSearchVm();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LoadData(ContractAgreementSearchVm model)
        {
            try
            {
                var data = await _contractAgreementService.GetPagedAsync(model);
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
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

            if (!ModelState.IsValid)
            {
                SetResponseMessage(DefaultMsg.InvalidInput, ResponseType.Error);
                return View(model);
            }

            try
            {
                var projectWork = await _projectWorkService.GetByIdAsync(model.ProjectWorkId);
                string fileName = null;
                if (model.ContractAgreementId > 0)
                {
                    if (model.DocumentFile != null && model.DocumentFile.ContentLength > 0)
                    {
                        fileName = HandleFileUpload(model.DocumentFile, model.ScanDocument);
                        if (string.IsNullOrEmpty(fileName))
                        {
                            SetResponseMessage("File upload failed", ResponseType.Error);
                            return View(model);
                        }
                        model.ScanDocument = fileName;
                    }
                    await _contractAgreementService.UpdateAsync(_mapper.Map<ContractAgreement>(model));
                    SetResponseMessage(string.Format(DefaultMsg.SaveSuccess, "Contract Agreement"), ResponseType.Success);
                }
                else
                {
                    if (model.DocumentFile != null && model.DocumentFile.ContentLength > 0)
                    {
                        fileName = HandleFileUpload(model.DocumentFile, model.ScanDocument);
                        if (string.IsNullOrEmpty(fileName))
                        {
                            SetResponseMessage("File upload failed", ResponseType.Error);
                            return View(model);
                        }
                        model.ScanDocument = fileName;
                    }

                    await _contractAgreementService.CreateAsync(_mapper.Map<ContractAgreement>(model));
                    projectWork.IsAgreementCompleted = true;
                    await _projectWorkService.UpdateAsync(projectWork);
                    SetResponseMessage(string.Format(DefaultMsg.UpdateSuccess, "Contract Agreement"), ResponseType.Success);
                }

                return RedirectToAction("Details", "ProjectWork", new { id = model.ProjectWorkId });
            }
            catch (Exception exception)
            {
                SetResponseMessage(string.Format(DefaultMsg.SaveFailed, "Contract Agreement", exception.Message), ResponseType.Error);
                // Populate dropdowns even if an error occurs
                await PopulateDropdownsAsync(model, null);
                return View(model);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _contractAgreementService.DeleteAsync(id);
                SetResponseMessage(string.Format(DefaultMsg.DeleteSuccess, "Contract Agreement"), ResponseType.Success);
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.DeleteFailed, "Contract Agreement", ex.Message), ResponseType.Error);
            }

            return RedirectToAction("List");
        }

        private async Task PopulateDropdownsAsync(ContractAgreementVm model, ContractAgreement contractAgreement)
        {
            if (contractAgreement != null)
            {
                model.AddDGDropdown = await _recruitmentCommitteeService.GetDropdownAsync(contractAgreement.AddDGId);
                model.DDGDropdown = await _recruitmentCommitteeService.GetDropdownAsync(contractAgreement.DDGId);
                model.ProjectdirectorDropdown = await _recruitmentCommitteeService.GetDropdownAsync(contractAgreement.ProjectDirectorId);
                model.DirectorDropdown = await _recruitmentCommitteeService.GetDropdownAsync(contractAgreement.DirectorId);
                model.ConstructionFirmDropdown = await _constructionCompanyService.GetDropdownAsync(contractAgreement.ConstructionCompanyId);
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

        private string HandleFileUpload(HttpPostedFileBase file, string existingFileName)
        {
            if (file == null || file.ContentLength <= 0)
                return null;

            // Delete the existing file if it exists
            if (!string.IsNullOrWhiteSpace(existingFileName))
                FileExtention.DeleteFile(existingFileName, fileStorePath);

            // Upload and return the new file name
            return FileExtention.UploadFile(file, fileStorePath);
        }

        public ActionResult PreviewDocument(string fileName)
        {
            // Build the full path to the file
            var filePath = Path.Combine(Server.MapPath($"~/{fileStorePath}"), fileName);

            // Check if the file exists
            if (System.IO.File.Exists(filePath))
            {
                // Return the file as a FileResult with the correct content type
                return File(filePath, "application/pdf");
            }
            else
            {
                return HttpNotFound(); // Return 404 if the file doesn't exist
            }
        }
    }
}