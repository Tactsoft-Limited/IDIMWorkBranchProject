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
            return View();
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
                return View(model);
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), $"An error occurred while processing your request.{exception.InnerException.Message}");
                return View(model);
            }

        }

    }
}