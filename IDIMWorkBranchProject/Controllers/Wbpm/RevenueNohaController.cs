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
    public class RevenueNohaController : BaseController
    {
        private readonly IRevenueNohaService _revenueNohaService;
        private readonly IRevenueService _revenueService;
        private readonly IMapper _mapper;
        private readonly string fileStorePath = "Documents/RevenuneNohaFiles";
        public RevenueNohaController(IActivityLogService activityLogService, IRevenueNohaService revenueNohaService, IMapper mapper, IProjectWorkService projectWorkService, IRevenueService revenueService) : base(activityLogService)
        {
            _revenueNohaService = revenueNohaService;
            _mapper = mapper;
            _revenueService = revenueService;
        }

        // GET: RevenueNoha
        public ActionResult Index()
        {
            return RedirectToAction("List","Revenue");
        }

        public async Task<ActionResult> Create(int id)
        {
            var revenue = await _revenueService.GetByIdAsync(id);
            var revenueNoha = await _revenueNohaService.GetByRevenueIdAsync(id);

            var model = new RevenueNohaVm
            {
                RevenueId = revenue.RevenueId,
                RevenueWorkTitleB = revenue.WorkTitleB,
            };
            if (revenueNoha != null)
            {
                model.RevenueNohaId = revenueNoha.RevenueNohaId;
                model.RevenueNohaDate = revenueNoha.RevenueNohaDate;
                model.LetterNo = revenueNoha.LetterNo;                
                model.RevenueNohaScanDocument = revenueNoha.RevenueNohaScanDocument;                
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RevenueNohaVm model)
        {
            var revenue = await _revenueService.GetByIdAsync(model.RevenueId);
            try
            {
                string fileName = null;
                if (model.RevenueNohaId > 0)
                {                    
                    if (model.DocumentFile != null && model.DocumentFile.ContentLength > 0)
                    {
                        //delete existing file
                        FileExtention.DeleteFile(model.RevenueNohaScanDocument, fileStorePath);

                        fileName = FileExtention.UploadFile(model.DocumentFile, fileStorePath);
                        if (fileName != null)
                        {
                            model.RevenueNohaScanDocument = fileName;
                        }
                        else
                        {
                            TempData["Message"] = Messages.FileUploadFailed(MessageType.Create.ToString());
                            return View(model);
                        }
                    }
                    await _revenueNohaService.UpdateAsync(_mapper.Map<RevenueNoha>(model));
                    TempData["Message"] = Messages.Success(MessageType.Update.ToString());
                }

                else
                {
                    if (model.DocumentFile != null && model.DocumentFile.ContentLength > 0)
                    {
                        fileName = FileExtention.UploadFile(model.DocumentFile, fileStorePath);
                        if (fileName != null)
                        {
                            model.RevenueNohaScanDocument = fileName;
                        }
                        else
                        {
                            TempData["Message"] = Messages.FileUploadFailed(MessageType.Create.ToString());
                            return View(model);
                        }
                        await _revenueNohaService.CreateAsync(_mapper.Map<RevenueNoha>(model));
                        revenue.IsNoahCompleted = true;
                        await _revenueService.UpdateAsync(revenue);

                        TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                    }
                }
                return RedirectToAction("List","Revenue");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), $"An error occurred while processing your request.{exception.InnerException.Message}");
                return View(model);
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

        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _revenueNohaService.GetByIdAsync(id);

            if (entity == null)
            {
                TempData["Message"] = "The requested record was not found.";
                return RedirectToAction("List", "Revenue");
            }

            var model = _mapper.Map<RevenueNohaVm>(entity);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(RevenueNohaVm model)
        {
            var entity = await _revenueNohaService.GetByIdAsync(model.RevenueNohaId);

            if (entity == null)
            {
                TempData["Message"] = "Record Not Found";
                // Use model.RevenueId (assuming your ViewModel has it) instead of entity.RevenueId because entity is null
                return RedirectToAction("List", "Revenue");
            }

            try
            {
                await _revenueNohaService.DeleteAsync(entity);

                TempData["Message"] = Messages.Success(MessageType.Delete.ToString());
                return RedirectToAction("List", "Revenue");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), exception.InnerException?.Message ?? exception.Message);
                return RedirectToAction("List", "Revenue");
            }
        }


    }
}