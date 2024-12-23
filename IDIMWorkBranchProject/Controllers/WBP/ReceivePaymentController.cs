using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.WBP;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.WBP;

namespace IDIMWorkBranchProject.Controllers.WBP
{
    public class ReceivePaymentController : BaseController
    {
        protected IReceivePaymentService _receiptPaymentService;
        protected IBillTypeService _billTypeService;
        protected IFiscalYearService _fiscalYearService;
        protected IProjectService _projectService;
        protected IConstructionFirmService _constructionFirmService;
        protected ISubProjectService _subProjectService;
        protected IQuarterService _quarterService;
        protected IUnitService _unitService;


        public ReceivePaymentController(IActivityLogService activityLogService, IBillTypeService billTypeService,
            IReceivePaymentService receiptPaymentService, IFiscalYearService fiscalYearService, IProjectService projectService,
            IConstructionFirmService constructionFirmService, ISubProjectService subProjectService, IQuarterService quarterService,
            IUnitService unitService) : base(activityLogService)
        {
            _billTypeService = billTypeService;
            _receiptPaymentService = receiptPaymentService;
            _fiscalYearService = fiscalYearService;
            _projectService = projectService;
            _constructionFirmService = constructionFirmService;
            _subProjectService = subProjectService;
            _quarterService = quarterService;
            _unitService = unitService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public async Task<ActionResult> List()
        {
            var model = new ReceivePaymentSearchVm
            {
                ProjectDropdown = await _projectService.GetDropdownAsync(),
                ConstructionFirmDropdown = await _constructionFirmService.GetDropdownAsync(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LoadData(ReceivePaymentSearchVm model)
        {
            try
            {
                var data = await _receiptPaymentService.GetAllAsync(model);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during data fetching
                return Json(new { error = ex.Message });
            }
        }

        public async Task<ActionResult> Create(int id)
        {
            // Fetch the project by ID
            var project = await _projectService.GetByIdAsync(id);

            if (project == null)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), "Project not found. Please ensure the project exists.");
                return RedirectToAction("Index");
            }

            // Fetch subproject by project ID
            var subproject = await _subProjectService.GetByProjectIdAsync(id);

            if (subproject == null)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), "No subproject found. Please create a subproject before proceeding with the payment.");
                return RedirectToAction("Create", "Subproject", new { id = id });
            }

            var model = new ReceivePaymentVm
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                Budget = await _projectService.GetEstimatedExpenses(id),
                BillNumber = await _receiptPaymentService.GetTotalPaymentReceiveAsync(id),
                ConstructionFirmDropdown = await _subProjectService.GetAllFirmByProject(id),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ReceivePaymentVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await _receiptPaymentService.InsertAsync(model);

                    ModelState.Clear();
                    model = new ReceivePaymentVm
                    {
                        ProjectId = model.ProjectId,
                        ProjectName = model.ProjectName,
                        Budget = await _projectService.GetEstimatedExpenses(model.ProjectId),
                        BillNumber = await _receiptPaymentService.GetTotalPaymentReceiveAsync(model.ProjectId),
                        ConstructionFirmDropdown = await _subProjectService.GetAllFirmByProject(model.ProjectId),
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

            TempData["Message"] = message;
            model.ConstructionFirmDropdown = await _subProjectService.GetAllFirmByProject(model.ProjectId);
            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await _receiptPaymentService.GetByIdAsync(id);
            model.ConstructionFirmDropdown = await _subProjectService.GetAllFirmByProject(model.ProjectId);

            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ReceivePaymentVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await _receiptPaymentService.UpdateAsync(model);

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

            TempData["Message"] = message;
            model.ConstructionFirmDropdown = await _subProjectService.GetAllFirmByProject(model.ProjectId);
            return View(model);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
                return HttpNotFound();

            int receiptPaymentId;
            int.TryParse(id, out receiptPaymentId);

            var model = await _receiptPaymentService.GetByIdAsync(receiptPaymentId);


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _receiptPaymentService.DeleteAsync(id);
            }
            catch (Exception exception)
            {
                var message = exception.Message;
                var sqlException = exception.GetBaseException() as SqlException;
                if (sqlException?.Number == 547)
                {
                    message = "Can not delete due to reference table.";
                }

                var model = await _receiptPaymentService.GetByIdAsync(id);


                TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), message);

                return View(model);
            }

            return RedirectToAction("List");
        }
    }
}
