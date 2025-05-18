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
    public class RevenueWorkOrderController : BaseController
    {
        private readonly IRevenueWorkOrderService _revenueWorkOrderService;
        private readonly IRevenueService _revenueService;
        private readonly IMapper _mapper;        
        private readonly string fileStorePath = "Documents/RevenueWorkOrderFiles";
        public RevenueWorkOrderController(IActivityLogService activityLogService, IRevenueWorkOrderService revenueWorkOrderService, IMapper mapper, IRevenueService revenueService) : base(activityLogService)
        {
            _revenueWorkOrderService = revenueWorkOrderService;
            _mapper = mapper;
            _revenueService = revenueService;
        }

        // GET: RevenueWorkOrder
        public ActionResult Index()
        {
            return RedirectToAction("List", "Revenue");
        }

        public async Task<ActionResult> Create(int id)
        {
            var revenue = await _revenueService.GetByIdAsync(id);

            var model = new RevenueWorkOrderVm
            {
                RevenueId = revenue.RevenueId,
                RevenueWorkTitleB = revenue.WorkTitleB,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RevenueWorkOrderVm model)
        {
            var revenue = await _revenueService.GetByIdAsync(model.RevenueId);
            string fileName = null;
            try
            {
                if (model.RevenueWorkOrderDocumentFile != null && model.RevenueWorkOrderDocumentFile.ContentLength > 0)
                {
                    fileName = FileExtention.UploadFile(model.RevenueWorkOrderDocumentFile, fileStorePath);
                    if (fileName != null)
                    {
                        model.RevenueWorkOrderScanDocument = fileName;
                    }
                    else
                    {
                        TempData["Message"] = Messages.FileUploadFailed(MessageType.Create.ToString());
                        return View(model);
                    }
                    await _revenueWorkOrderService.CreateAsync(_mapper.Map<RevenueWorkOrder>(model));
                    revenue.IsWorkOrderComplited = true;
                    await _revenueService.UpdateAsync(revenue);
                    TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                }

                return RedirectToAction("List", "Revenue");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), $"An error occurred while processing your request.{exception.InnerException.Message}");
                return View(model);
            }
        }
        public async Task<ActionResult> Edit(int id)
        {
            var workOrder = await _revenueWorkOrderService.GetByIdAsync(id);
            var revenue = await _revenueService.GetByIdAsync(workOrder.RevenueId);
            var model = _mapper.Map<RevenueWorkOrderVm>(await _revenueWorkOrderService.GetByIdAsync(id));

            model.RevenueId = revenue.RevenueId;
            model.RevenueWorkTitleB = revenue.WorkTitleB;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RevenueWorkOrderVm model)
        {
            string fileName = null;
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = Messages.InvalidInput(MessageType.Update.ToString());
                    return View(model);
                }
                if (model.RevenueWorkOrderDocumentFile != null && model.RevenueWorkOrderDocumentFile.ContentLength > 0)
                {
                    //delete existing file
                    FileExtention.DeleteFile(model.RevenueWorkOrderScanDocument, fileStorePath);

                    fileName = FileExtention.UploadFile(model.RevenueWorkOrderDocumentFile, fileStorePath);
                    if (fileName != null)
                    {
                        model.RevenueWorkOrderScanDocument = fileName;
                    }
                    else
                    {
                        TempData["Message"] = Messages.FileUploadFailed(MessageType.Create.ToString());
                        return View(model);
                    }
                }
                await _revenueWorkOrderService.UpdateAsync(_mapper.Map<RevenueWorkOrder>(model));
                TempData["Message"] = Messages.Success(MessageType.Update.ToString());

                return RedirectToAction("List", "Revenue"); // Redirect to list after success
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Update.ToString(), exception.Message);
                return View(model);
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _revenueWorkOrderService.GetByIdAsync(id);

            if (entity == null)
            {
                TempData["Message"] = "The requested record was not found.";
                return RedirectToAction("List", "RevenueWorkOrder");
            }

            var model = _mapper.Map<RevenueWorkOrderVm>(entity);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(RevenueWorkOrderVm model)
        {
            var entity = await _revenueWorkOrderService.GetByIdAsync(model.RevenueWorkOrderId);
            try
            {

                if (entity == null)
                {
                    TempData["Message"] = "Record Not Found";
                    return RedirectToAction("List", "RevenueWorkOrder");
                }

                await _revenueService.DeleteAsync(entity);

                TempData["Message"] = Messages.Success(MessageType.Delete.ToString());
                return RedirectToAction("List", "RevenueWorkOrder");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), exception.InnerException?.Message);
                return RedirectToAction("List", "RevenueWorkOrder"); // Avoids null reference
            }
        }
    }
}