using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Extentions.Exceptions;
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
    public class ProjectDirectorController : BaseController
    {
        private readonly IProjectDirectorService _projectDirectorService;
        private readonly IADPProjectService _aDPProjectService;
        private readonly IMapper _mapper;
        private readonly string fileStorePath = "Documents/ProjectDirectorFiles";

        public ProjectDirectorController(IActivityLogService activityLogService,
            IProjectDirectorService projectDirectorService, IMapper mapper, IADPProjectService aDPProjectService) : base(activityLogService)
        {
            _projectDirectorService = projectDirectorService;
            _mapper = mapper;
            _aDPProjectService = aDPProjectService;
        }

        // GET: ProjectDirector
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new ProjectDirectorSearchVm();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LoadData(ProjectDirectorSearchVm model)
        {
            try
            {
                var data = await _projectDirectorService.GetPagedAsync(model);
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        public async Task<ActionResult> Create(int id)
        {
            var adpProject = await _aDPProjectService.GetByIdAsync(id);
            if (adpProject == null)
            {
                return HttpNotFound();
            }

            return View(new ProjectDirectorVm
            {
                ADPProjectId = adpProject.ADPProjectId,
                ProjectTitle = adpProject.ProjectTitle,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProjectDirectorVm model)
        {
            if (!ModelState.IsValid)
            {
                SetResponseMessage(DefaultMsg.InvalidInput, ResponseType.Error);
                return View(model);
            }

            try
            {
                if (_projectDirectorService.IsDuplicateProjectDirector(model.ProjectDirectorName))
                {
                    throw new DuplicateNameException(model.ProjectDirectorName);
                }

                string fileName = null;
                if (model.PDDocumentFile != null && model.PDDocumentFile.ContentLength > 0)
                {
                    fileName = HandleFileUpload(model.PDDocumentFile, model.PDDocument);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        SetResponseMessage("File upload failed", ResponseType.Error);
                        return View(model);
                    }
                    model.PDDocument = fileName;
                }

                var entity = _mapper.Map<ProjectDirector>(model);
                var result = await _projectDirectorService.CreateAsync(entity);

                SetResponseMessage(string.Format(DefaultMsg.SaveSuccess, result.ProjectDirectorName), ResponseType.Success);
                return RedirectToAction("Details", "ADPProject", new { id = model.ADPProjectId });
            }
            catch (Exception ex)
            {
                SetResponseMessage(ex.Message, ResponseType.Error);
                return View(model);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var entity = await _projectDirectorService.GetByIdAsync(id);
            if (entity == null)
            {
                return HttpNotFound();
            }

            return View(_mapper.Map<ProjectDirectorVm>(entity));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProjectDirectorVm model)
        {
            if (!ModelState.IsValid)
            {
                SetResponseMessage(DefaultMsg.InvalidInput, ResponseType.Error);
                return View(model);
            }

            try
            {
                if (_projectDirectorService.IsDuplicateProjectDirector(model.ProjectDirectorName, model.ProjectDirectorId))
                {
                    throw new DuplicateNameException(model.ProjectDirectorName);
                }

                string fileName = null;
                if (model.PDDocumentFile != null && model.PDDocumentFile.ContentLength > 0)
                {
                    fileName = HandleFileUpload(model.PDDocumentFile, model.PDDocument);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        SetResponseMessage("File upload failed", ResponseType.Error);
                        return View(model);
                    }
                    model.PDDocument = fileName;
                }

                var entity = _mapper.Map<ProjectDirector>(model);
                var result = await _projectDirectorService.UpdateAsync(entity);

                SetResponseMessage(string.Format(DefaultMsg.UpdateSuccess, result.ProjectDirectorName), ResponseType.Success);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                SetResponseMessage(ex.Message, ResponseType.Error);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _projectDirectorService.DeleteAsync(id);
                SetResponseMessage(string.Format(DefaultMsg.DeleteSuccess, "Project Director"), ResponseType.Success);
            }
            catch (Exception ex)
            {
                SetResponseMessage(ex.Message, ResponseType.Error);
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

    }
}