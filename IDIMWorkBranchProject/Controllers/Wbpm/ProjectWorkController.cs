using AutoMapper;

using BGB.Data.Entities.Wbpm;

using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
	public class ProjectWorkController : BaseController
    {
        private readonly IProjectWorkService _projectWorkService;
        protected readonly IConstructionCompanyService _constructionCompanyService;
        protected readonly IADPProjectService _ADPProjectService;
        protected readonly IADPReceivePaymentService _ADPReceivePaymentService;
        protected readonly IContractorCompanyPaymentService _contractorCompanyPaymentService;
        private readonly IMapper _mapper;
        private readonly string fileStorePath = "Documents/ProjectWorkFile";

        public ProjectWorkController(IActivityLogService activityLogService, IProjectWorkService projectWorkService, IMapper mapper, IConstructionCompanyService constructionCompanyService, IADPProjectService aDPProjectService, IADPReceivePaymentService aDPReceivePaymentService, IContractorCompanyPaymentService contractorCompanyPaymentService) : base(activityLogService)
        {
            _projectWorkService = projectWorkService;
            _mapper = mapper;
            _constructionCompanyService = constructionCompanyService;
            _ADPProjectService = aDPProjectService;
            _ADPReceivePaymentService = aDPReceivePaymentService;
            _contractorCompanyPaymentService = contractorCompanyPaymentService;
        }
        public ActionResult Index()
        {

            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            var model = new ProjectWorkSearchVm();
            return View(model);
        }
        //public async Task<ActionResult> LoadData(FinancialYearAllocationSearchVm model)
        //{
        //    try
        //    {
        //        var data = await _projectWorkService.GetPagedAsync(model);
        //        return Json(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { error = ex.Message });
        //    }
        //}


        public async Task<ActionResult> Details(int id)
        {
            var model = new ProjectWorkDetailsVm
            {
                ProjectWorks = _mapper.Map<ProjectWorkVm>(await _projectWorkService.GetByIdAsync(id)),
                ADPReceivePayments = _mapper.Map<List<ADPReceivePaymentVm>>(await _ADPReceivePaymentService.GetByProjectWorkIdAsync(id)),
                ContractorCompanyPayments = _mapper.Map<List<ContractorCompanyPaymentVm>>(await _contractorCompanyPaymentService.GetByProjectWorkIdAsync(id))
            };
            return View(model);
        }


        public async Task<ActionResult> Create(int id)
        {
            var data = await _ADPProjectService.GetByIdAsync(id);
            var model = new ProjectWorkVm
            {
                ADPProjectId = data.ADPProjectId,
                ProjectTitle = data.ProjectTitle,
                ConstructionFirmDropdown = await _constructionCompanyService.GetDropdownAsync()
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProjectWorkVm model)
        {
            string fileName = null;

            try
            {
                if (ModelState.IsValid)
                {
                    // Step 1: Check if file is uploaded
                    if (model.BankGuaranteeDocumentFile != null && model.BankGuaranteeDocumentFile.ContentLength > 0)
                    {
                        fileName = FileExtention.UploadFile(model.BankGuaranteeDocumentFile, fileStorePath);

                        // If file is successfully uploaded, save the file name to the model
                        if (fileName != null)
                        {
                            model.BankGuaranteeDocument = fileName;
                        }
                        else
                        {
                            TempData["Message"] = Messages.FileUploadFailed(MessageType.Create.ToString());
                            return View(model);
                        }
                    }
                    if (model.NOADocumentFile != null && model.NOADocumentFile.ContentLength > 0)
                    {
                        fileName = FileExtention.UploadFile(model.NOADocumentFile, fileStorePath);

                        // If file is successfully uploaded, save the file name to the model
                        if (fileName != null)
                        {
                            model.NOADocument = fileName;
                        }
                        else
                        {
                            TempData["Message"] = Messages.FileUploadFailed(MessageType.Create.ToString());
                            return View(model);
                        }
                    }
                    if (model.AgreementDocumentFile != null && model.AgreementDocumentFile.ContentLength > 0)
                    {
                        fileName = FileExtention.UploadFile(model.AgreementDocumentFile, fileStorePath);

                        // If file is successfully uploaded, save the file name to the model
                        if (fileName != null)
                        {
                            model.AgreementDocument = fileName;
                        }
                        else
                        {
                            TempData["Message"] = Messages.FileUploadFailed(MessageType.Create.ToString());
                            return View(model);
                        }
                    }
                    if (model.WorkOrderDocumentFile != null && model.WorkOrderDocumentFile.ContentLength > 0)
                    {
                        fileName = FileExtention.UploadFile(model.WorkOrderDocumentFile, fileStorePath);

                        // If file is successfully uploaded, save the file name to the model
                        if (fileName != null)
                        {
                            model.WorkOrderDocument = fileName;
                        }
                        else
                        {
                            TempData["Message"] = Messages.FileUploadFailed(MessageType.Create.ToString());
                            return View(model);
                        }
                    }

                    // Step 2: Map the model to the entity
                    var entity = _mapper.Map<ProjectWork>(model);

                    // Step 3: Attempt to save the entity to the database
                    await _projectWorkService.CreateAsync(entity);

                    // Show success message and reset the form
                    TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                    model.ConstructionFirmDropdown = await _constructionCompanyService.GetDropdownAsync(model.ConstructionCompanyId);
                    return View(new ProjectWorkVm());  // Reset model after success
                }

                // If the model state is not valid
                TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
                model.ConstructionFirmDropdown = await _constructionCompanyService.GetDropdownAsync(model.ConstructionCompanyId);
            }
            catch (Exception exception)
            {
                if (fileName != null)
                {
                    FileExtention.DeleteFile(fileStorePath, fileName);
                }

                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), exception.Message);
                model.ConstructionFirmDropdown = await _constructionCompanyService.GetDropdownAsync(model.ConstructionCompanyId);
            }

            // Return the model to the view
            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {

            var model = _mapper.Map<ProjectWorkVm>(await _projectWorkService.GetByIdAsync(id));
            model.ProjectTitle = await _ADPProjectService.GetAdpProjectTitle(model.ADPProjectId);
            model.ConstructionFirmDropdown = await _constructionCompanyService.GetDropdownAsync(model.ConstructionCompanyId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProjectWorkVm model)
        {
            string fileName = null;
            try
            {
                if (ModelState.IsValid)
                {
                    // Step 1: Check if file is uploaded
                    if (model.BankGuaranteeDocumentFile != null && model.BankGuaranteeDocumentFile.ContentLength > 0)
                    {
                        //Delete Old File
                        if (model.BankGuaranteeDocumentFile != null)
                            FileExtention.DeleteFile(fileStorePath, model.BankGuaranteeDocument);

                        fileName = FileExtention.UploadFile(model.BankGuaranteeDocumentFile, fileStorePath);

                        // If file is successfully uploaded, save the file name to the model
                        if (fileName != null)
                        {
                            model.BankGuaranteeDocument = fileName;
                        }
                        else
                        {
                            TempData["Message"] = Messages.FileUploadFailed(MessageType.Create.ToString());
                            return View(model);
                        }
                    }
                    if (model.AgreementDocumentFile != null && model.AgreementDocumentFile.ContentLength > 0)
                    {
                        //Delete Old File
                        if (model.AgreementDocumentFile != null)
                            FileExtention.DeleteFile(fileStorePath, model.AgreementDocument);

                        fileName = FileExtention.UploadFile(model.AgreementDocumentFile, fileStorePath);

                        // If file is successfully uploaded, save the file name to the model
                        if (fileName != null)
                        {
                            model.AgreementDocument = fileName;
                        }
                        else
                        {
                            TempData["Message"] = Messages.FileUploadFailed(MessageType.Create.ToString());
                            return View(model);
                        }
                    }
                    if (model.NOADocumentFile != null && model.NOADocumentFile.ContentLength > 0)
                    {
                        //Delete Old File
                        if (model.NOADocumentFile != null)
                            FileExtention.DeleteFile(fileStorePath, model.NOADocument);

                        fileName = FileExtention.UploadFile(model.NOADocumentFile, fileStorePath);

                        // If file is successfully uploaded, save the file name to the model
                        if (fileName != null)
                        {
                            model.NOADocument = fileName;
                        }
                        else
                        {
                            TempData["Message"] = Messages.FileUploadFailed(MessageType.Create.ToString());
                            return View(model);
                        }
                    }
                    if (model.WorkOrderDocumentFile != null && model.WorkOrderDocumentFile.ContentLength > 0)
                    {
                        //Delete Old File
                        if (model.WorkOrderDocumentFile != null)
                            FileExtention.DeleteFile(fileStorePath, model.WorkOrderDocument);

                        fileName = FileExtention.UploadFile(model.WorkOrderDocumentFile, fileStorePath);

                        // If file is successfully uploaded, save the file name to the model
                        if (fileName != null)
                        {
                            model.WorkOrderDocument = fileName;
                        }
                        else
                        {
                            TempData["Message"] = Messages.FileUploadFailed(MessageType.Create.ToString());
                            return View(model);
                        }
                    }
                    var entity = _mapper.Map<ProjectWork>(model);
                    await _projectWorkService.UpdateAsync(entity);
                    TempData["Message"] = Messages.Success(MessageType.Update.ToString());
                    return View(new ProjectWorkVm());  // Reset model after success
                }

                TempData["Message"] = Messages.InvalidInput(MessageType.Update.ToString());
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Update.ToString(), exception.Message);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _projectWorkService.GetByIdAsync(id);

            if (entity == null)
            {
                TempData["Message"] = "The requested record was not found.";
                return RedirectToAction("details/" + entity.ADPProjectId, "ADPProject");
            }

            var model = _mapper.Map<ProjectWorkVm>(entity);
            model.ProjectTitle = await _ADPProjectService.GetAdpProjectTitle(entity.ADPProjectId);
            model.ConstructionFirmDropdown = await _constructionCompanyService.GetDropdownAsync(model.ConstructionCompanyId);
            return View(model); // Load the delete confirmation view
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(ProjectWorkVm model)
        {
            var entity = await _projectWorkService.GetByIdAsync(model.ProjectWorkId);
            try
            {

                if (entity == null)
                {
                    TempData["Message"] = "Record Not Found";
                    return RedirectToAction("Details/" + entity.ADPProjectId, "ADPProject");
                }

                await _projectWorkService.DeleteAsync(entity);

                TempData["Message"] = Messages.Success(MessageType.Delete.ToString());
                return RedirectToAction("Details/" + entity.ADPProjectId, "ADPProject");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), exception.InnerException?.Message);
                return RedirectToAction("Details/" + entity.ADPProjectId, "ADPProject"); // Avoids null reference
            }
        }
    }
}