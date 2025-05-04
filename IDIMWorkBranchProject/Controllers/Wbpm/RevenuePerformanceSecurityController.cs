using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System.Threading.Tasks;
using System;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class RevenuePerformanceSecurityController : BaseController
    {
        private readonly IRevenuePerformanceSecurityService _revenuePerformanceSecurityService;
        private readonly IMapper _mapper;
        private readonly IProjectWorkService _projectWorkService;
        private readonly string fileStorePath = "Documents/RevenuePerformanceSecurityFiles";
        public RevenuePerformanceSecurityController(IActivityLogService activityLogService, IRevenuePerformanceSecurityService revenuePerformanceSecurityService, IMapper mapper, IProjectWorkService projectWorkService) : base(activityLogService)
        {
            _revenuePerformanceSecurityService = revenuePerformanceSecurityService;
            _mapper = mapper;
            _projectWorkService = projectWorkService;
        }

        // GET: RevenuePerformanceSecurity
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Create(int id)
        {
            var projectWork = await _projectWorkService.GetByIdAsync(id);
            var revenuePerformanceSecurity = await _revenuePerformanceSecurityService.GetByProjectWorkIdAsync(id);

            var model = new RevenuePerformanceSecurityVm
            {
                ProjectWorkId = projectWork.ProjectWorkId,
                ProjectWorkTitleB = projectWork.ProjectWorkTitleB,
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
            var projectWork = await _projectWorkService.GetByIdAsync(model.ProjectWorkId);
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
                        //projectWork.IsPerformanceSecuritySubmited = true;
                        //await _projectWorkService.UpdateAsync(projectWork);
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
    }
}