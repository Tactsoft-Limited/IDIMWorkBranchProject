using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
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
            var projectWork = await _projectWorkService.GetByIdAsync(model.ProjectWorkId);
            string fileName = null;
            try
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
                        await _workOrderService.CreateAsync(_mapper.Map<WorkOrder>(model));
                        projectWork.IsWorkOrderCompleted = true;
                        await _projectWorkService.UpdateAsync(projectWork);
                        TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                    }
                
                return RedirectToAction(nameof(ProjectWorkController.Details), nameof(ProjectWork), new { id = model.ProjectWorkId });
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), $"An error occurred while processing your request.{exception.InnerException.Message}");
                return View(model);
            }
        }
        public async Task<ActionResult> Edit(int id)
        {
            var workOrder = await _workOrderService.GetByIdAsync(id);
            var projectWork = await _projectWorkService.GetByIdAsync(workOrder.ProjectWorkId);
            var model = _mapper.Map<WorkOrderVm>(await _workOrderService.GetByIdAsync(id));
            model.ProjectWorkId=projectWork.ProjectWorkId;
            model.ProjectWorkTitleB=projectWork.ProjectWorkTitleB;
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(WorkOrderVm model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = Messages.InvalidInput(MessageType.Update.ToString());
                    return View(model);
                }
                string fileName = null;
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
                await _workOrderService.UpdateAsync(_mapper.Map<WorkOrder>(model));
                TempData["Message"] = Messages.Success(MessageType.Update.ToString());

                return RedirectToAction(nameof(ProjectWorkController.Details), nameof(ProjectWork), new { id = model.ProjectWorkId }); // Redirect to list after success
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Update.ToString(), exception.Message);
                return View(model);
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _workOrderService.GetByIdAsync(id);

            if (entity == null)
            {
                TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), "The requested record was not found.");
                return RedirectToAction(nameof(ProjectWorkController.Details), nameof(ProjectWork), new { id = entity.ProjectWorkId });
            }

            var model = _mapper.Map<WorkOrderVm>(entity);
            model.ProjectWorkTitleB = await _projectWorkService.GetProjectWorkTitle(entity.ProjectWorkId);
            return View(model); // Load the delete confirmation view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(WorkOrderVm model)
        {
            var entity = await _workOrderService.GetByIdAsync(model.WorkOrderId);
            try
            {

                if (entity == null)
                {
                    TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), "Record Not Found");
                    return RedirectToAction("Details/" + entity.ProjectWorkId, "ProjectWork");
                }

                await _workOrderService.DeleteAsync(entity);

                TempData["Message"] = Messages.Success(MessageType.Delete.ToString());
                return RedirectToAction("Details/" + entity.ProjectWorkId, "ProjectWork");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), exception.InnerException?.Message);
                return RedirectToAction("Details/" + entity.ProjectWorkId, "ProjectWork");
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