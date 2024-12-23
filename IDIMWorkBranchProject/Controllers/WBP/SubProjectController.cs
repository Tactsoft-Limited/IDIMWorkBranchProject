using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.WBP;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.WBP;

namespace IDIMWorkBranchProject.Controllers.WBP
{
    public class SubProjectController : BaseController
    {
        protected IConstructionFirmService ConstructionFirmService { get; set; }
        protected IFiscalYearService FiscalYearService { get; set; }
        protected IGeneralInformationService GeneralInformationService { get; set; }
        protected IProjectService ProjectService { get; set; }
        protected ISubProjectService SubProjectService { get; set; }
        protected IUnitService UnitService { get; set; }

        public SubProjectController(IActivityLogService activityLogService, IConstructionFirmService constructionFirmService, IFiscalYearService fiscalYearService, IGeneralInformationService generalInformationService, IProjectService projectService, ISubProjectService subProjectService, IUnitService unitService) : base(activityLogService)
        {
            ConstructionFirmService = constructionFirmService;
            FiscalYearService = fiscalYearService;
            GeneralInformationService = generalInformationService;
            ProjectService = projectService;
            SubProjectService = subProjectService;
            UnitService = unitService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public async Task<ActionResult> List()
        {
            var model = new SubProjectSearchVm
            {
                ConstructionFirmDropdown = await ConstructionFirmService.GetDropdownAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LoadData(SubProjectSearchVm model)
        {
            try
            {
                var data = await SubProjectService.GetAllAsync(model);
                return Json(data);
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during data fetching
                return Json(new { error = ex.Message });
            }
        }


        public async Task<ActionResult> Create(int id)
        {
            var project = await ProjectService.GetByIdAsync(id);

            if (project == null)
                return HttpNotFound();

            var model = new SubProjectVm
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                ConstructionFirmDropdown = await ConstructionFirmService.GetDropdownAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(SubProjectVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    // Process each file upload (BankGuarantee, NOA, Agreement, WorkOrder)
                    ProcessFileUpload(model.BankGuaranteeFile, "BankGuarantee", model);
                    ProcessFileUpload(model.NOAFile, "NOA", model);
                    ProcessFileUpload(model.AgreementFile, "Agreement", model);
                    ProcessFileUpload(model.WorkOrderFile, "WorkOrder", model);

                    await SubProjectService.InsertAsync(model);

                    ModelState.Clear();
                    model = new SubProjectVm
                    {
                        ProjectId = model.ProjectId,
                        ProjectName = model.ProjectName
                    };
                    message = Messages.Success(MessageType.Create.ToString());
                }
                else
                {
                    message = Messages.InvalidInput(MessageType.Create.ToString());
                }
            }
            catch (Exception exception)
            {
                message = Messages.Failed(MessageType.Create.ToString(), exception.Message);
            }

            model.ConstructionFirmDropdown = await ConstructionFirmService.GetDropdownAsync(model.ConstructionFirmId);
            TempData["Message"] = message;

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await SubProjectService.GetByIdAsync(id);

            if (model == null)
                return HttpNotFound();

            model.ConstructionFirmDropdown = await ConstructionFirmService.GetDropdownAsync(model.ConstructionFirmId);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(SubProjectVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await SubProjectService.UpdateAsync(model);

                    message = Messages.Success(MessageType.Update.ToString());
                }
                else
                {
                    message = Messages.InvalidInput(MessageType.Update.ToString());
                }
            }
            catch (Exception exception)
            {
                message = Messages.Failed(MessageType.Update.ToString(), exception.Message);
            }

            model.ConstructionFirmDropdown = await ConstructionFirmService.GetDropdownAsync(model.ConstructionFirmId);

            TempData["Message"] = message;

            return View(model);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
                return HttpNotFound();

            int projectId;
            int.TryParse(id, out projectId);

            var model = await SubProjectService.GetByIdAsync(projectId);


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await SubProjectService.DeleteAsync(id);
            }
            catch (Exception exception)
            {
                var message = exception.Message;
                var sqlException = exception.GetBaseException() as SqlException;
                if (sqlException?.Number == 547)
                {
                    message = "Can not delete due to reference table.";
                }

                var model = await SubProjectService.GetByIdAsync(id);


                TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), message);

                return View(model);
            }

            return RedirectToAction("List");
        }


        public void ProcessFileUpload(HttpPostedFileBase file, string filePrefix, SubProjectVm model)
        {
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    var fileExtension = Path.GetExtension(file.FileName);
                    string currentDate = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    var fileName = $"{currentDate}_{filePrefix}{fileExtension}";
                    var uploadsFolder = Server.MapPath("~/Content/Documents");

                    // Check if the directory exists; if not, create it
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var path = Path.Combine(uploadsFolder, fileName);
                    file.SaveAs(path); // Save the file to the specified path

                    // Assign the saved file name to the respective property on the model
                    typeof(SubProjectVm).GetProperty(filePrefix)?.SetValue(model, fileName);
                }
                catch (Exception ex)
                {
                    TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), ex.Message);
                    throw; // Re-throw the exception to handle it in the calling method
                }
            }
        }
    }
}
