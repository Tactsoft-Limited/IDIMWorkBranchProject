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
            var model = new ProjectDirectorVm
            {
                ADPProjectId = adpProject.ADPProjectId,
                ProjectTitle = adpProject.ProjectTitle,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProjectDirectorVm model)
        {
            string fileName = null;

            try
            {
                if (ModelState.IsValid)
                {
                    // Step 1: Check if file is uploaded
                    if (model.PDDocumentFile != null && model.PDDocumentFile.ContentLength > 0)
                    {
                        fileName = FileExtention.UploadFile(model.PDDocumentFile, fileStorePath);

                        // If file is successfully uploaded, save the file name to the model
                        if (fileName != null)
                        {
                            model.PDDocument = fileName;
                        }
                        else
                        {
                            TempData["Message"] = Messages.FileUploadFailed(MessageType.Create.ToString());
                            return View(model);
                        }
                    }

                    // Step 2: Map the model to the entity
                    var entity = _mapper.Map<ProjectDirector>(model);

                    // Step 3: Attempt to save the entity to the database
                    await _projectDirectorService.CreateAsync(entity);

                    // Show success message and reset the form
                    TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                    return View(new ProjectDirectorVm());  // Reset model after success
                }

                // If the model state is not valid
                TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
            }
            catch (Exception exception)
            {
                if (fileName != null)
                {
                    FileExtention.DeleteFile(fileStorePath, fileName);
                }

                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), exception.Message);
            }

            // Return the model to the view
            return View(model);
        }


        public async Task<ActionResult> Edit(int id)
        {
            var model = _mapper.Map<ProjectDirectorVm>(await _projectDirectorService.GetByIdAsync(id));
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ProjectDirectorVm model)
        {
            string fileName = null;
            try
            {
                if (ModelState.IsValid)
                {
                    // Step 1: Check if file is uploaded
                    if (model.PDDocumentFile != null && model.PDDocumentFile.ContentLength > 0)
                    {
                        //Delete Old File
                        FileExtention.DeleteFile( model.PDDocument, fileStorePath);

                        fileName = FileExtention.UploadFile(model.PDDocumentFile, fileStorePath);

                        // If file is successfully uploaded, save the file name to the model
                        if (fileName != null)
                        {
                            model.PDDocument = fileName;
                        }
                        else
                        {
                            TempData["Message"] = Messages.FileUploadFailed(MessageType.Create.ToString());
                            return View(model);
                        }
                    }

                    var entity = _mapper.Map<ProjectDirector>(model);
                    await _projectDirectorService.UpdateAsync(entity);
                    TempData["Message"] = Messages.Success(MessageType.Update.ToString());
                    return View(new ProjectDirectorVm());  // Reset model after success
                }

                TempData["Message"] = Messages.InvalidInput(MessageType.Update.ToString());
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Update.ToString(), exception.Message);
            }

            return View(model);
        }



    }
}