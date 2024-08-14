using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.WBP;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.WBP;

namespace IDIMWorkBranchProject.Controllers.WBP
{
    public class ReceiptPaymentController : BaseController
    {
        protected IBillTypeService BillTypeService { get; set; }
        protected IReceiptPaymentService ReceiptPaymentService { get; set; }
        protected IFiscalYearService FiscalYearService { get; set; }
        protected IProjectService ProjectService { get; set; }
        protected IQuarterService QuarterService { get; set; }
        protected IUnitService UnitService { get; set; }

        public ReceiptPaymentController(IActivityLogService activityLogService, IBillTypeService billTypeService, IReceiptPaymentService receiptPaymentService, IFiscalYearService fiscalYearService, IProjectService projectService, IQuarterService quarterService, IUnitService unitService) : base(activityLogService)
        {
            BillTypeService = billTypeService;
            ReceiptPaymentService = receiptPaymentService;
            FiscalYearService = fiscalYearService;
            ProjectService = projectService;
            QuarterService = quarterService;
            UnitService = unitService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public async Task<ActionResult> List()
        {
            var model = new ReceiptPaymentSearchVm
            {
                ReceiptPayments = await ReceiptPaymentService.GetByAsync(),
                FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(),
                QuarterDropdown = await QuarterService.GetDropdownAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> List(ReceiptPaymentSearchVm model)
        {
            model.ReceiptPayments = await ReceiptPaymentService.GetByAsync(model);
            model.FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(model.FiscalYearId);
            model.QuarterDropdown = await QuarterService.GetDropdownAsync(model.QuarterId);

            return View(model);
        }

        public async Task<ActionResult> Create(int id)
        {
            var project = await ProjectService.GetByIdAsync(id);
            if (project == null)
                return HttpNotFound();

            var model = new ReceiptPaymentVm
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(project.FiscalYearId),
                QuarterDropdown = await QuarterService.GetDropdownAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ReceiptPaymentVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await ReceiptPaymentService.InsertAsync(model);

                    ModelState.Clear();
                    model = new ReceiptPaymentVm
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

            model.FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(model.FiscalYearId);
            model.QuarterDropdown = await QuarterService.GetDropdownAsync(model.QuarterId);
            ViewBag.Message = message;

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await ReceiptPaymentService.GetByIdAsync(id);

            if (model == null)
                return HttpNotFound();

            model.FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(model.FiscalYearId);
            model.QuarterDropdown = await QuarterService.GetDropdownAsync(model.QuarterId);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ReceiptPaymentVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await ReceiptPaymentService.UpdateAsync(model);

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

            model.FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(model.FiscalYearId);
            model.QuarterDropdown = await QuarterService.GetDropdownAsync(model.QuarterId);
            ViewBag.Message = message;

            return View(model);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
                return HttpNotFound();

            int receiptPaymentId;
            int.TryParse(id, out receiptPaymentId);

            var model = await ReceiptPaymentService.GetByIdAsync(receiptPaymentId);


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await ReceiptPaymentService.DeleteAsync(id);
            }
            catch (Exception exception)
            {
                var message = exception.Message;
                var sqlException = exception.GetBaseException() as SqlException;
                if (sqlException?.Number == 547)
                {
                    message = "Can not delete due to reference table.";
                }

                var model = await ReceiptPaymentService.GetByIdAsync(id);


                ViewBag.Message = Messages.Failed(MessageType.Delete.ToString(), message);

                return View(model);
            }

            return RedirectToAction("List");
        }
    }
}
