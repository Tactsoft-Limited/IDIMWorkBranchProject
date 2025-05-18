using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System.Threading.Tasks;
using System;
using System.Web.Mvc;
using System.IO;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class RevenuePerformanceSecurityController : BaseController
    {
        private readonly IRevenuePerformanceSecurityService _revenuePerformanceSecurityService;
        private readonly IMapper _mapper;
        private readonly IRevenueService _revenueService;
        private readonly string fileStorePath = "Documents/RevenuePerformanceSecurityFiles";
        public RevenuePerformanceSecurityController(IActivityLogService activityLogService, IRevenuePerformanceSecurityService revenuePerformanceSecurityService, IMapper mapper, IRevenueService revenueService) : base(activityLogService)
        {
            _revenuePerformanceSecurityService = revenuePerformanceSecurityService;
            _mapper = mapper;
            _revenueService = revenueService;
        }

        // GET: RevenuePerformanceSecurity
        public ActionResult Index()
        {
            return RedirectToAction("List", "Revenue");
        }
        public async Task<ActionResult> Create(int id)
        {
            var revenue = await _revenueService.GetByIdAsync(id);
            var revenuePerformanceSecurity = await _revenuePerformanceSecurityService.GetByRevenueIdAsync(id);

            var model = new RevenuePerformanceSecurityVm
            {
                RevenueId = revenue.RevenueId,
                RevenueWorkTitleB = revenue.WorkTitleB,
            };
            if (revenuePerformanceSecurity != null)
            {
                model.RevenuePerformanceSecurityId = revenuePerformanceSecurity.RevenuePerformanceSecurityId;
                model.RevenuePerformanceSecuritySubmissionDate = revenuePerformanceSecurity.RevenuePerformanceSecuritySubmissionDate;
                model.RevenuePerformanceSecurityExpiryDate = revenuePerformanceSecurity.RevenuePerformanceSecurityExpiryDate;
                model.RevenuePerformanceSecurityScanDocument = revenuePerformanceSecurity.RevenuePerformanceSecurityScanDocument;
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RevenuePerformanceSecurityVm model)
        {
            var revenue = await _revenueService.GetByIdAsync(model.RevenueId);
            string fileName = null;
            try
            {
                if (model.RevenuePerformanceSecurityId > 0)
                {
                    if (model.DocumentFile != null && model.DocumentFile.ContentLength > 0)
                    {
                        //delete existing file
                        FileExtention.DeleteFile(model.RevenuePerformanceSecurityScanDocument, fileStorePath);

                        fileName = FileExtention.UploadFile(model.DocumentFile, fileStorePath);
                        if (fileName != null)
                        {
                            model.RevenuePerformanceSecurityScanDocument = fileName;
                        }
                        else
                        {
                            TempData["Message"] = Messages.FileUploadFailed(MessageType.Create.ToString());
                            return View(model);
                        }
                    }
                    await _revenuePerformanceSecurityService.UpdateAsync(_mapper.Map<RevenuePerformanceSecurity>(model));
                    TempData["Message"] = Messages.Success(MessageType.Update.ToString());
                }
                else
                {
                    if (model.DocumentFile != null && model.DocumentFile.ContentLength > 0)
                    {
                        fileName = FileExtention.UploadFile(model.DocumentFile, fileStorePath);
                        if (fileName != null)
                        {
                            model.RevenuePerformanceSecurityScanDocument = fileName;
                        }
                        else
                        {
                            TempData["Message"] = Messages.FileUploadFailed(MessageType.Create.ToString());
                            return View(model);
                        }
                        await _revenuePerformanceSecurityService.CreateAsync(_mapper.Map<RevenuePerformanceSecurity>(model));
                        revenue.IsPerformanceSecuritySubmited = true;
                        await _revenueService.UpdateAsync(revenue);
                        TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                    }
                }
                return RedirectToAction("List", "Revenue");
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
    }
}