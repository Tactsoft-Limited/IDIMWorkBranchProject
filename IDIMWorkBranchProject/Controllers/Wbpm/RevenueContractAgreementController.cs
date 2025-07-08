using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class RevenueContractAgreementController : BaseController
    {
        private readonly IRevenueContractAgreementService _revenueContractAgreementService;
        private readonly IProjectDirectorService _projectDirectorService;
        private readonly IRevenueService _revenueService;
        private readonly IConstructionCompanyService _constructionCompanyService;
        private readonly IRecruitmentCommitteeService _recruitmentCommitteeService;
        private readonly IMapper _mapper;
        private readonly string fileStorePath = "Documents/RevenueContractAggrementFiles";
        public RevenueContractAgreementController(IActivityLogService activityLogService, IRevenueContractAgreementService revenueContractAgreementService, IProjectDirectorService projectDirectorService, IRevenueService revenueService, IConstructionCompanyService constructionCompanyService, IRecruitmentCommitteeService recruitmentCommitteeService, IMapper mapper) : base(activityLogService)
        {
            _revenueContractAgreementService = revenueContractAgreementService;
            _projectDirectorService = projectDirectorService;
            _revenueService = revenueService;
            _constructionCompanyService = constructionCompanyService;
            _recruitmentCommitteeService = recruitmentCommitteeService;
            _mapper = mapper;
        }

        // GET: RevenueContractAgreement
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Create(int id)
        {
            var revenue = await _revenueService.GetByIdAsync(id);
            var revenueContractAgreement = await _revenueContractAgreementService.GetByRevenueIdAsync(id );


            var model = new RevenueContractAgreementVm
            {
                RevenueId = revenue.RevenueId,
                RevenueWorkTitleB = revenue.WorkTitleB,
                ConstructionCompanyId = revenueContractAgreement?.ConstructionCompanyId ?? 0,
            };            
            if (revenueContractAgreement != null)
            {
                model.ContractAgreementId = revenueContractAgreement.ContractAgreementId;
                model.AgreementDate = revenueContractAgreement.AgreementDate;
                model.ScanDocument = revenueContractAgreement.ScanDocument;
            }

            await PopulateDropdownsAsync(model, revenueContractAgreement);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RevenueContractAgreementVm model)
        {
            var revenue = await _revenueService.GetByIdAsync(model.RevenueId);
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
                    await _revenueContractAgreementService.UpdateAsync(_mapper.Map<RevenueContractAgreement>(model));
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
                    await _revenueContractAgreementService.CreateAsync(_mapper.Map<RevenueContractAgreement>(model));
                    revenue.IsAgreementCompleted = true;
                    await _revenueService.UpdateAsync(revenue);
                    TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                }

                return RedirectToAction("details/" + model.RevenueId, "Revenue");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), $"An error occurred while processing your request.{exception.InnerException.Message}");
                // Populate dropdowns even if an error occurs
                await PopulateDropdownsAsync(model, null);
                return View(model);
            }

        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _revenueContractAgreementService.GetByIdAsync(id);

            if (entity == null)
            {
                TempData["Message"] = "The requested record was not found.";
                return RedirectToAction("details/" + entity.RevenueId, "Revenue");
            }

            var model = _mapper.Map<RevenueContractAgreementVm>(entity);
            model.RevenueWorkTitleB = await _revenueService.GetWorkTitle(entity.RevenueId);
            model.ConstructionFirm = await _constructionCompanyService.GetConstructionFirm(entity.RevenueId);
            return View(model); // Load the delete confirmation view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(RevenueContractAgreementVm model)
        {
            var entity = await _revenueContractAgreementService.GetByIdAsync(model.ContractAgreementId);
            try
            {

                if (entity == null)
                {
                    TempData["Message"] = "Record Not Found";
                    return RedirectToAction("Details/" + entity.RevenueId, "Revenue");
                }

                await _revenueContractAgreementService.DeleteAsync(entity);

                TempData["Message"] = Messages.Success(MessageType.Delete.ToString());
                return RedirectToAction("Details/" + entity.RevenueId, "Revenue");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), exception.InnerException?.Message);
                return RedirectToAction("Details/" + entity.RevenueId, "Revenue"); // Avoids null reference
            }
        }


        private async Task PopulateDropdownsAsync(RevenueContractAgreementVm model, RevenueContractAgreement revenueContractAgreements)
        {
            if (revenueContractAgreements != null)
            {
                model.AddDGDropdown = await _recruitmentCommitteeService.GetDropdownAsync(revenueContractAgreements.AddDGId);
                model.DDGDropdown = await _recruitmentCommitteeService.GetDropdownAsync(revenueContractAgreements.DDGId);
                model.ProjectdirectorDropdown = await _recruitmentCommitteeService.GetDropdownAsync(revenueContractAgreements.ProjectDirectorId);
                model.DirectorDropdown = await _recruitmentCommitteeService.GetDropdownAsync(revenueContractAgreements.DirectorId);
                model.ConstructionFirmDropdown = await _constructionCompanyService.GetDropdownAsync(revenueContractAgreements.ConstructionCompanyId);
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