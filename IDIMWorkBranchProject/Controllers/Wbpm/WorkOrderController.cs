using AutoMapper;
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
            return View();
        }
        public async Task<ActionResult> Create(int id)
        {
            var projectWork = await _projectWorkService.GetByIdAsync(id);
            var workOrder = await _workOrderService.GetByProjectWorkIdAsync(id);

            var model = new WorkOrderVm
            {
                ProjectWorkId = projectWork.ProjectWorkId,
                ProjectWorkTitleB = projectWork.ProjectWorkTitleB,
            };
            if (workOrder != null)
            {
                model.WorkOrderId = workOrder.WorkOrderId;
                model.LetterNo = workOrder.LetterNo;
                model.WorkOrderDate = workOrder.WorkOrderDate;
                model.StartDate = workOrder.StartDate;
                model.EndDate = workOrder.EndDate;
                model.ScanDocument = workOrder.ScanDocument;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(WorkOrderVm model)
        {
            string fileName = null;
            try
            {
                if (model.WorkOrderId > 0)
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
                    await _workOrderService.UpdateAsync(_mapper.Map<WorkOrder>(model));
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
                        await _workOrderService.CreateAsync(_mapper.Map<WorkOrder>(model));
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
            var entity = await _workOrderService.GetByIdAsync(id);

            if (entity == null)
            {
                TempData["Message"] = "The requested record was not found.";
                return RedirectToAction("details/" + entity.ProjectWorkId, "ProjectWork");
            }

            var model = _mapper.Map<WorkOrderVm>(entity);
            model.ProjectWorkTitleB = await _projectWorkService.GetProjectWorkTitle(entity.ProjectWorkId);
            return View(model); // Load the delete confirmation view
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(WorkOrder model)
        {
            var entity = await _workOrderService.GetByIdAsync(model.WorkOrderId);
            try
            {

                if (entity == null)
                {
                    TempData["Message"] = "Record Not Found";
                    return RedirectToAction("Details/" + entity.ProjectWorkId, "ProjetWork");
                }

                await _workOrderService.DeleteAsync(entity);

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