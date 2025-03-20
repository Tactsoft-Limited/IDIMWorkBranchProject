using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public NohaController(IActivityLogService activityLogService, INohaService nohaService, IMapper mapper, IProjectWorkService projectWorkService) : base(activityLogService)
        {
            _nohaService = nohaService;
            _mapper = mapper;
            _projectWorkService = projectWorkService;
        }

        // GET: Noha
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            var model = new NohaSearchVm();
            return View(model);
        }
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
            var projectWork = await _projectWorkService.GetByIdAsync(id);
            var noha = await _nohaService.GetByProjectWorkIdAsync(id);

            var model = new NohaVm
            {
                ProjectWorkId = projectWork.ProjectWorkId,
                ProjectWorkTitleB = projectWork.ProjectWorkTitleB,
            };
            if (noha != null)
            {
                model.NohaId = noha.NohaId;
                model.NohaDate = noha.NohaDate;
                model.LetterNo = noha.LetterNo;
                model.ScanDocument = noha.ScanDocument;
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NohaVm model)
        {
            var projectWork = await _projectWorkService.GetByIdAsync(model.ProjectWorkId);
            try
            {
                string fileName = null;
                if (model.NohaId > 0)
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
                    await _nohaService.UpdateAsync(_mapper.Map<Noha>(model));
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
                        await _nohaService.CreateAsync(_mapper.Map<Noha>(model));
                        projectWork.IsNoahCompleted = true;
                        await _projectWorkService.UpdateAsync(projectWork);

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

        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _nohaService.GetByIdAsync(id);

            if (entity == null)
            {
                TempData["Message"] = "The requested record was not found.";
                return RedirectToAction(nameof(ProjectWorkController.Details), nameof(ProjectWork), new { id = entity.ProjectWorkId });
            }

            var model = _mapper.Map<NohaVm>(entity);
            model.ProjectWorkTitleB = await _projectWorkService.GetProjectWorkTitle(entity.ProjectWorkId);
            return View(model); // Load the delete confirmation view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(NohaVm model)
        {
            try
            {
                var entity = await _nohaService.GetByIdAsync(model.NohaId);

                if (entity == null)
                {
                    TempData["Message"] = "Record Not Found";
                    return RedirectToAction(nameof(ProjectWorkController.Details), nameof(ProjectWork), new { id = model.ProjectWorkId });
                }

                await _nohaService.DeleteAsync(entity);
                TempData["Message"] = Messages.Success(MessageType.Delete.ToString());
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), exception.InnerException?.Message ?? exception.Message);
            }

            return RedirectToAction(nameof(ProjectWorkController.Details), nameof(ProjectWork), new { id = model.ProjectWorkId });
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