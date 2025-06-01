using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class FormalMeetingController : BaseController
    {
        protected readonly IFormalMeetingService _formalMeetingService;
        protected readonly IADPProjectService _aDPProjectService;
        protected readonly IMapper _mapper;
        protected readonly string fileStorePath = "Documents/MeetingFile";
        public FormalMeetingController(IActivityLogService activityLogService, IFormalMeetingService formalMeetingService, IMapper mapper, IADPProjectService aDPProjectService) : base(activityLogService)
        {
            _formalMeetingService = formalMeetingService;
            _mapper = mapper;
            _aDPProjectService = aDPProjectService;
        }

        // GET: FormalMeeting
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Create(int id)
        {
            var data = await _aDPProjectService.GetByIdAsync(id);
            var model = new FormalMeetingVm
            {
                ADPProjectId = data.ADPProjectId,
                ProjectTitle = data.ProjectTitle,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(FormalMeetingVm model)
        {
            if (!ModelState.IsValid)
            {
                SetResponseMessage(DefaultMsg.InvalidInput, ResponseType.Error);
                return View(model);
            }
            try
            {
                string fileName = null;

                // Upload new file if provided
                if (model.MeetingDocumentFile != null && model.MeetingDocumentFile.ContentLength > 0)
                {
                    fileName = HandleFileUpload(model.MeetingDocumentFile, model.MeetingDocument);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        SetResponseMessage("File upload failed", ResponseType.Error);
                        return View(model);
                    }
                    model.MeetingDocument = fileName;
                }

                var entity = _mapper.Map<FormalMeeting>(model);
                await _formalMeetingService.CreateAsync(entity);
                SetResponseMessage(string.Format(DefaultMsg.SaveSuccess, "Formal Meeting"), ResponseType.Success);
                return RedirectToAction("Details", "ADPProject", new { id = model.ADPProjectId });
            }
            catch (Exception exception)
            {
                SetResponseMessage(string.Format(DefaultMsg.SaveFailed, "Formal Meeting", exception.Message), ResponseType.Error);
                return View(model);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {

            var model = _mapper.Map<FormalMeetingVm>(await _formalMeetingService.GetByIdAsync(id));
            model.ProjectTitle = await _aDPProjectService.GetAdpProjectTitle(model.ADPProjectId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(FormalMeetingVm model)
        {
            if (!ModelState.IsValid)
            {
                SetResponseMessage(DefaultMsg.InvalidValue, ResponseType.Error);
                return View(model);
            }

            try
            {
                string fileName = null;

                // Upload new file if provided
                if (model.MeetingDocumentFile != null && model.MeetingDocumentFile.ContentLength > 0)
                {
                    fileName = HandleFileUpload(model.MeetingDocumentFile, model.MeetingDocument);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        SetResponseMessage("File upload failed", ResponseType.Error);
                        return View(model);
                    }
                    model.MeetingDocument = fileName;
                }

                var entity = _mapper.Map<FormalMeeting>(model);
                await _formalMeetingService.UpdateAsync(entity);

                SetResponseMessage(string.Format(DefaultMsg.UpdateSuccess, "Formal Meeting"), ResponseType.Success);
                return RedirectToAction("Details", "ADPProject", new { id = model.ADPProjectId });
            }
            catch (Exception exception)
            {
                SetResponseMessage(string.Format(DefaultMsg.SaveFailed, "Formal Meeting", exception.Message), ResponseType.Error);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var result = new FormalMeeting();
            try
            {
                result = await _formalMeetingService.DeleteAsync(id);
                if (result != null)
                {
                    FileExtention.DeleteFile(result.MeetingDocument, fileStorePath);
                }
                SetResponseMessage(string.Format(DefaultMsg.DeleteSuccess, "Formal Meeting"), ResponseType.Success);
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.DeleteFailed, "Formal Meeting", ex.Message), ResponseType.Error);
            }
            return RedirectToAction("Details", "ADPProject", new { id = result.ADPProjectId });
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