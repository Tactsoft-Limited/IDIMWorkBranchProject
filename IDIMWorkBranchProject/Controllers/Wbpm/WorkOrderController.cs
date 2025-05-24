using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.Collections.Generic;
using System.IdentityModel.Metadata;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms.VisualStyles;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class WorkOrderController : BaseController
    {
        private readonly IWorkOrderService _workOrderService;
        private readonly IProjectWorkService _projectWorkService;
        private readonly IMapper _mapper;
        private readonly string fileStorePath = "Documents/WorkOrderFiles";

        public WorkOrderController(IActivityLogService activityLogService, IWorkOrderService workOrderService, IProjectWorkService projectWorkService, IMapper mapper) : base(activityLogService)
        {
            _workOrderService = workOrderService;
            _projectWorkService = projectWorkService;
            _mapper = mapper;
        }

        // GET: WorkOrder
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            var model = new WorkOrderSearchVm();
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> LoadData(WorkOrderSearchVm model)
        {
            try
            {
                var data = await _workOrderService.GetPagedAsync(model);
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

            var model = new WorkOrderVm
            {
                ProjectWorkId = projectWork.ProjectWorkId,
                ProjectWorkTitleB = projectWork.ProjectWorkTitleB,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(WorkOrderVm model)
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

                await _workOrderService.CreateAsync(_mapper.Map<WorkOrder>(model));
                projectWork.IsWorkOrderCompleted = true;
                await _projectWorkService.UpdateAsync(projectWork);

                SetResponseMessage(string.Format(DefaultMsg.SaveSuccess, "Work Order"), ResponseType.Success);

                return RedirectToAction("Details", "ProjectWork", new { id = model.ProjectWorkId });
            }
            catch (Exception exception)
            {
                SetResponseMessage(string.Format(DefaultMsg.SaveFailed, "Work Order", exception.Message), ResponseType.Error);
                return View(model);
            }
        }
        public async Task<ActionResult> Edit(int id)
        {
            var workOrder = await _workOrderService.GetByIdAsync(id);
            var projectWork = await _projectWorkService.GetByIdAsync(workOrder.ProjectWorkId);
            var model = _mapper.Map<WorkOrderVm>(await _workOrderService.GetByIdAsync(id));
            model.ProjectWorkId = projectWork.ProjectWorkId;
            model.ProjectWorkTitleB = projectWork.ProjectWorkTitleB;
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(WorkOrderVm model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = Messages.InvalidInput(MessageType.Update.ToString());
                return View(model);
            }
            try
            {
                string fileName = null;
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
                await _workOrderService.UpdateAsync(_mapper.Map<WorkOrder>(model));
                SetResponseMessage(string.Format(DefaultMsg.UpdateSuccess, "Work Order"), ResponseType.Success);

                return RedirectToAction("Details", "ProjectWork", new { id = model.ProjectWorkId });
            }
            catch (Exception exception)
            {
                SetResponseMessage(string.Format(DefaultMsg.SaveFailed, "Work Order", exception.Message), ResponseType.Error);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _workOrderService.DeleteAsync(id);
                SetResponseMessage(string.Format(DefaultMsg.DeleteSuccess, "Work Order"), ResponseType.Success);
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.DeleteFailed, "Work Order", ex.Message), ResponseType.Error);
            }

            return RedirectToAction("List");
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