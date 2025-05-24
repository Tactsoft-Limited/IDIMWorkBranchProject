using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class NohaController : BaseController
    {
        private readonly INohaService _nohaService;
        private readonly IProjectWorkService _projectWorkService;
        private readonly IMapper _mapper;
        private readonly string fileStorePath = "Documents/NohaFiles";

        public NohaController(
            IActivityLogService activityLogService,
            INohaService nohaService,
            IMapper mapper,
            IProjectWorkService projectWorkService
        ) : base(activityLogService)
        {
            _nohaService = nohaService;
            _mapper = mapper;
            _projectWorkService = projectWorkService;
        }

        public ActionResult Index() => RedirectToAction("List");

        public ActionResult List() => View(new NohaSearchVm());

        [HttpPost]
        public async Task<ActionResult> LoadData(NohaSearchVm model)
        {
            try
            {
                var data = await _nohaService.GetPagedAsync(model);
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        public async Task<ActionResult> Create(int id)
        {
            var project = await _projectWorkService.GetByIdAsync(id);
            var noha = await _nohaService.GetByProjectWorkIdAsync(id);

            var model = new NohaVm
            {
                ProjectWorkId = project.ProjectWorkId,
                ProjectWorkTitleB = project.ProjectWorkTitleB
            };

            if (noha != null)
            {
                _mapper.Map(noha, model);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NohaVm model)
        {
            if (!ModelState.IsValid)
            {
                SetResponseMessage(DefaultMsg.InvalidInput, ResponseType.Error);
                return View(model);
            }

            try
            {
                var project = await _projectWorkService.GetByIdAsync(model.ProjectWorkId);
                string fileName = null;

                // Upload new file if provided
                if (model.DocumentFile != null && model.DocumentFile.ContentLength > 0)
                {
                    fileName = HandleFileUpload(model.DocumentFile, model.ScanDocument);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        SetResponseMessage("File upload failed", ResponseType.Error);
                        return View(model);
                    }
                    model.ScanDocument = fileName;
                }

                if (model.NohaId > 0)
                {
                    await _nohaService.UpdateAsync(_mapper.Map<Noha>(model));
                    SetResponseMessage(string.Format(DefaultMsg.UpdateSuccess, "Noha"), ResponseType.Success);
                }
                else
                {
                    await _nohaService.CreateAsync(_mapper.Map<Noha>(model));
                    project.IsNoahCompleted = true;
                    await _projectWorkService.UpdateAsync(project);
                    SetResponseMessage(string.Format(DefaultMsg.SaveSuccess, "Noha"), ResponseType.Success);
                }

                return RedirectToAction("Details", "ProjectWork", new { id = model.ProjectWorkId });
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.SaveFailed, "Noha", ex.Message), ResponseType.Error);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _nohaService.DeleteAsync(id);
                SetResponseMessage(string.Format(DefaultMsg.DeleteSuccess, "Noha"), ResponseType.Success);
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.DeleteFailed, "Noha", ex.Message), ResponseType.Error);
            }

            return RedirectToAction("List");
        }

        public ActionResult PreviewDocument(string fileName)
        {
            var filePath = Path.Combine(Server.MapPath($"~/{fileStorePath}"), fileName);

            if (System.IO.File.Exists(filePath))
            {
                return File(filePath, "application/pdf");
            }

            return HttpNotFound();
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
