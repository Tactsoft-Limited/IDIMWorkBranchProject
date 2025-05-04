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
    public class RevenueNohaController : BaseController
    {
        private readonly IProjectWorkService _projectWorkService;
        private readonly IRevenueNohaService _revenueNohaService;
        private readonly IMapper _mapper;
        private readonly string fileStorePath = "Documents/RevenuneNohaFiles";
        public RevenueNohaController(IActivityLogService activityLogService, IRevenueNohaService revenueNohaService, IMapper mapper, IProjectWorkService projectWorkService) : base(activityLogService)
        {
            _revenueNohaService = revenueNohaService;
            _mapper = mapper;
            _projectWorkService = projectWorkService;
        }

        // GET: RevenueNoha
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Create(int id)
        {
            var projectWork = await _projectWorkService.GetByIdAsync(id);
            var revenueNoha = await _revenueNohaService.GetByProjectWorkIdAsync(id);

            var model = new RevenueNohaVm
            {
                ProjectWorkId = projectWork.ProjectWorkId,
                ProjectWorkTitleB = projectWork.ProjectWorkTitleB,
            };
            if (revenueNoha != null)
            {
                model.RevenueNohaId = revenueNoha.RevenueNohaId;
                model.RevenueNohaDate = revenueNoha.RevenueNohaDate;
                model.LetterNo = revenueNoha.LetterNo;
                model.ScanDocument = revenueNoha.ScanDocument;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RevenueNohaVm model)
        {
            var projectWork = await _projectWorkService.GetByIdAsync(model.ProjectWorkId);
            try
            {
                string fileName = null;
                if (model.RevenueNohaId > 0)
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
                            model.ScanDocument = fileName;
                        }
                        else
                        {
                            TempData["Message"] = Messages.FileUploadFailed(MessageType.Create.ToString());
                            return View(model);
                        }
                        await _revenueNohaService.CreateAsync(_mapper.Map<RevenueNoha>(model));
                        ////projectWork.IsNoahCompleted = true;
                        //await _projectWorkService.UpdateAsync(projectWork);

                        TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                    }
                }
                return RedirectToAction(nameof(ProjectWorkController.Details), nameof(ProjectWork), new { id = model.ProjectWorkId });
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), $"An error occurred while processing your request.{exception.InnerException.Message}");
                return View(model);
            }

        }

    }
}