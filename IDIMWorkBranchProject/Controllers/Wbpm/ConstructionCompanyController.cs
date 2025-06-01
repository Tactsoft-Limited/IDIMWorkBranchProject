using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Extentions.Exceptions;
using IDIMWorkBranchProject.Models;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class ConstructionCompanyController : BaseController
    {
        private readonly IConstructionCompanyService _constructionCompanyService;
        private readonly IMapper _mapper;
        private readonly string fileStorePath = "Documents/ConstructionCompanyFiles";
        public ConstructionCompanyController(IActivityLogService activityLogService,
            IConstructionCompanyService constructionCompanyService, IMapper mapper) : base(activityLogService)
        {
            _constructionCompanyService = constructionCompanyService;
            _mapper = mapper;
        }

        // GET: ConstructionCompany
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new ConstructionCompanySearchVm();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LoadData(ConstructionCompanySearchVm model)
        {
            try
            {
                var (data, total, totalDisplay) = await _constructionCompanyService.GetPagedAsync(model);
                var jsonData = new
                {
                    recordsTotal = total,
                    recordsFiltered = totalDisplay,
                    data = data.Select(record => new
                    {
                        ConstructionCompanyId = record.ConstructionCompanyId,
                        FirmName = HttpUtility.HtmlEncode(record.FirmName),
                        Owner = HttpUtility.HtmlEncode(record.OwnerName) + "<br/>" + HttpUtility.HtmlEncode(record.OwnerPhone),
                        AuthorizedPerson = HttpUtility.HtmlEncode(record.AuthorizedPersonName) + "<br/>" + HttpUtility.HtmlEncode(record.AuthorizedPersonNamePhone),
                        LicenceNumber = HttpUtility.HtmlEncode(record.LicenceNumber),
                        LicensingOrganization = HttpUtility.HtmlEncode(record.LicensingOrganization),
                        LicenceCategory = HttpUtility.HtmlEncode(record.LicenceCategory),
                        ExpiryDate = HttpUtility.HtmlEncode(record.ExpiryDate.ToNullableShortDateString()),
                        Action = record.ConstructionCompanyId,
                    })
                };
                return Json(jsonData);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            var cons = await _constructionCompanyService.GetByIdAsync(id);
            return View(cons);
        }

        public ActionResult Create()
        {
            var model = new ConstructionCompanyVm();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ConstructionCompanyVm model)
        {
            if (!ModelState.IsValid)
            {
                SetResponseMessage(string.Format(DefaultMsg.InvalidInput), ResponseType.Error);
                return View(model);  // Reset model after success
            }

            try
            {
                string fileName = null;

                if (_constructionCompanyService.IsDuplicateConstructionCompany(model.FirmName))
                    throw new DuplicateNameException(model.FirmName);

                // Upload new file if provided
                if (model.OwnerPictureFile != null && model.OwnerPictureFile.ContentLength > 0)
                {
                    fileName = HandleFileUpload(model.OwnerPictureFile, model.OwnerPicture);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        SetResponseMessage("File upload failed", ResponseType.Error);
                        return View(model);
                    }
                    model.OwnerPicture = fileName;
                }

                // Upload new file if provided
                if (model.AuthorizationLetterFile != null && model.AuthorizationLetterFile.ContentLength > 0)
                {
                    fileName = HandleFileUpload(model.AuthorizationLetterFile, model.AuthorizationLetter);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        SetResponseMessage("File upload failed", ResponseType.Error);
                        return View(model);
                    }
                    model.AuthorizationLetter = fileName;
                }

                // Upload new file if provided
                if (model.LicenceDocumentFile != null && model.LicenceDocumentFile.ContentLength > 0)
                {
                    fileName = HandleFileUpload(model.LicenceDocumentFile, model.LicenceDocument);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        SetResponseMessage("File upload failed", ResponseType.Error);
                        return View(model);
                    }
                    model.LicenceDocument = fileName;
                }

                var entity = _mapper.Map<ConstructionCompany>(model);
                var result = await _constructionCompanyService.CreateAsync(entity);
                SetResponseMessage(string.Format(DefaultMsg.SaveSuccess, result.FirmName), ResponseType.Success);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                SetResponseMessage(string.Format(DefaultMsg.SaveFailed, "Construction Company", exception.Message), ResponseType.Error);
                return View(model);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = _mapper.Map<ConstructionCompanyVm>(await _constructionCompanyService.GetByIdAsync(id));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ConstructionCompanyVm model)
        {
            if (!ModelState.IsValid)
            {
                SetResponseMessage(string.Format(DefaultMsg.InvalidInput), ResponseType.Error);
                return View(model);  // Reset model after success
            }
            try
            {
                string fileName = null;
                if (_constructionCompanyService.IsDuplicateConstructionCompany(model.FirmName, model.ConstructionCompanyId))
                    throw new DuplicateNameException(model.FirmName);

                // Upload new file if provided
                if (model.OwnerPictureFile != null && model.OwnerPictureFile.ContentLength > 0)
                {
                    fileName = HandleFileUpload(model.OwnerPictureFile, model.OwnerPicture);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        SetResponseMessage("File upload failed", ResponseType.Error);
                        return View(model);
                    }
                    model.OwnerPicture = fileName;
                }

                // Upload new file if provided
                if (model.AuthorizationLetterFile != null && model.AuthorizationLetterFile.ContentLength > 0)
                {
                    fileName = HandleFileUpload(model.AuthorizationLetterFile, model.AuthorizationLetter);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        SetResponseMessage("File upload failed", ResponseType.Error);
                        return View(model);
                    }
                    model.AuthorizationLetter = fileName;
                }

                // Upload new file if provided
                if (model.LicenceDocumentFile != null && model.LicenceDocumentFile.ContentLength > 0)
                {
                    fileName = HandleFileUpload(model.LicenceDocumentFile, model.LicenceDocument);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        SetResponseMessage("File upload failed", ResponseType.Error);
                        return View(model);
                    }
                    model.LicenceDocument = fileName;
                }

                var entity = _mapper.Map<ConstructionCompany>(model);
                var result = await _constructionCompanyService.UpdateAsync(entity);
                SetResponseMessage(string.Format(DefaultMsg.UpdateSuccess, result.FirmName), ResponseType.Success);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                SetResponseMessage(string.Format(DefaultMsg.UpdateFailed, "Construction Company", exception.Message), ResponseType.Error);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _constructionCompanyService.DeleteAsync(id);

                if (result != null)
                {
                    FileExtention.DeleteFile(result.OwnerPicture, fileStorePath);
                    FileExtention.DeleteFile(result.LicenceDocument, fileStorePath);
                    FileExtention.DeleteFile(result.AuthorizationLetter, fileStorePath);
                }

                SetResponseMessage(string.Format(DefaultMsg.DeleteSuccess, "Construction Company"), ResponseType.Success);
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.DeleteFailed, "Construction Company", ex.Message), ResponseType.Error);
            }
            return RedirectToAction("Index");
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