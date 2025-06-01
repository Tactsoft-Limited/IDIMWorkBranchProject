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
            if (!ModelState.IsValid)
            {
                SetResponseMessage(DefaultMsg.InvalidInput, ResponseType.Error);
                return View(model);
            }

            try
            {
                var projectWork = await _projectWorkService.GetByIdAsync(model.ProjectWorkId);
                string fileName = null;

                // Upload new file if provided
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

                if (model.PerformanceSecurityId > 0)
                {
                    await _performanceSecurityService.UpdateAsync(_mapper.Map<PerformanceSecurity>(model));
                    SetResponseMessage(string.Format(DefaultMsg.SaveSuccess, "Performance Security"), ResponseType.Success);
                }
                else
                {
                    await _performanceSecurityService.CreateAsync(_mapper.Map<PerformanceSecurity>(model));
                    projectWork.IsPerformanceSecuritySubmited = true;
                    await _projectWorkService.UpdateAsync(projectWork);
                    SetResponseMessage(string.Format(DefaultMsg.UpdateSuccess, "Performance Security"), ResponseType.Success);
                }
                return RedirectToAction("Details", "ProjectWork", new { id = model.ProjectWorkId });
            }
            catch (Exception exception)
            {
                SetResponseMessage(string.Format(DefaultMsg.UpdateSuccess, "Performance Security", exception.Message), ResponseType.Error);
                return View(model);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _performanceSecurityService.DeleteAsync(id);
                SetResponseMessage(string.Format(DefaultMsg.DeleteSuccess, "Performance Security"), ResponseType.Success);
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.DeleteFailed, "Performance Security", ex.Message), ResponseType.Error);
            }

            return RedirectToAction("List");
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

        private string HandleFileUpload(HttpPostedFileBase file, string existingFileName)
        {
            if (file == null || file.ContentLength <= 0)
                return null;

            if (!string.IsNullOrWhiteSpace(existingFileName))
                FileExtention.DeleteFile(existingFileName, fileStorePath);

            return FileExtention.UploadFile(file, fileStorePath);
        }
    }
}