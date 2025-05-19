using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class PerformanceSecurityController : BaseController
    {
        private readonly IPerformanceSecurityService _performanceSecurityService;
        private readonly IProjectWorkService _projectWorkService;
        private readonly IMapper _mapper;
        private readonly string fileStorePath = "Documents/PerformanceSecurityFiles";

        public PerformanceSecurityController(IActivityLogService activityLogService, IPerformanceSecurityService performanceSecurityService, IProjectWorkService projectWorkService, IMapper mapper) : base(activityLogService)
        {
            _performanceSecurityService = performanceSecurityService;
            _projectWorkService = projectWorkService;
            _mapper = mapper;
        }

        // GET: PerformanceSecurity
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            var model = new PerformanceSecuritySearchVm();
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> LoadData(PerformanceSecuritySearchVm model)
        {
            try
            {
                var data = await _performanceSecurityService.GetPagedAsync(model);
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
            var performanceSecurity = await _performanceSecurityService.GetByProjectWorkIdAsync(id);

            var model = new PerformanceSecurityVm
            {
                ProjectWorkId = projectWork.ProjectWorkId,
                ProjectWorkTitleB = projectWork.ProjectWorkTitleB,
            };
            if (performanceSecurity != null)
            {
                model.PerformanceSecurityId = performanceSecurity.PerformanceSecurityId;
                model.SubmissionDate = performanceSecurity.SubmissionDate;
                model.ExpiryDate = performanceSecurity.ExpiryDate;
                model.ScanDocument = performanceSecurity.ScanDocument;
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PerformanceSecurityVm model)
        {
            var projectWork = await _projectWorkService.GetByIdAsync(model.ProjectWorkId);
            string fileName = null;
            try
            {
                if (model.PerformanceSecurityId > 0)
                {
                    if (model.DocumentFile != null && model.DocumentFile.ContentLength > 0)
                    {
                        //delete existing file
                        FileExtention.DeleteFile(model.ScanDocument, fileStorePath);

                        fileName = FileExtention.UploadFile(model.DocumentFile, fileStorePath);
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
                    await _performanceSecurityService.UpdateAsync(_mapper.Map<PerformanceSecurity>(model));
                    TempData["Message"] = Messages.Success(MessageType.Update.ToString());
                }
                else
                {
                    if (model.DocumentFile != null && model.DocumentFile.ContentLength > 0)
                    {
                        fileName = FileExtention.UploadFile(model.DocumentFile, fileStorePath);
                        if (fileName != null)
                        {
                            model.ScanDocument = fileName;
                        }
                        else
                        {
                            TempData["Message"] = Messages.FileUploadFailed(MessageType.Create.ToString());
                            return View(model);
                        }
                        await _performanceSecurityService.CreateAsync(_mapper.Map<PerformanceSecurity>(model));
                        projectWork.IsPerformanceSecuritySubmited = true;
                        await _projectWorkService.UpdateAsync(projectWork);
                        TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                    }
                }
                return RedirectToAction("details/" + model.ProjectWorkId, "ProjectWork");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), $"An error occurred while processing your request.{exception.InnerException.Message}");
                return View(model);
            }

        }

        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _performanceSecurityService.GetByIdAsync(id);

            if (entity == null)
            {
                TempData["Message"] = "The requested record was not found.";
                return RedirectToAction("details/" + entity.ProjectWorkId, "ProjectWork");
            }

            var model = _mapper.Map<PerformanceSecurityVm>(entity);
            model.ProjectWorkTitleB = await _projectWorkService.GetProjectWorkTitle(entity.ProjectWorkId);
            return View(model); // Load the delete confirmation view
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(PerformanceSecurityVm model)
        {
            var entity = await _performanceSecurityService.GetByIdAsync(model.PerformanceSecurityId);
            try
            {

                if (entity == null)
                {
                    TempData["Message"] = "Record Not Found";
                    return RedirectToAction("Details/" + entity.ProjectWorkId, "ProjetWork");
                }

                await _performanceSecurityService.DeleteAsync(entity);

                TempData["Message"] = Messages.Success(MessageType.Delete.ToString());
                return RedirectToAction("Details/" + entity.ProjectWorkId, "ProjectWork");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), exception.InnerException?.Message);
                return RedirectToAction("Details/" + entity.ProjectWorkId, "ProjectWork"); // Avoids null reference
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