using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Extentions.File;
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
    public class ProjectWorkController : BaseController
    {
        private readonly IProjectWorkService _projectWorkService;
        protected readonly IConstructionCompanyService _constructionCompanyService;
        protected readonly IADPProjectService _ADPProjectService;
        private readonly IMapper _mapper;
        private readonly string fileStorePath = "Documents/ProjectWorkFile";

        public ProjectWorkController(IActivityLogService activityLogService, IProjectWorkService projectWorkService, IMapper mapper, IConstructionCompanyService constructionCompanyService, IADPProjectService aDPProjectService) : base(activityLogService)
        {
            _projectWorkService = projectWorkService;
            _mapper = mapper;
            _constructionCompanyService = constructionCompanyService;
            _ADPProjectService = aDPProjectService;
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
    }
}