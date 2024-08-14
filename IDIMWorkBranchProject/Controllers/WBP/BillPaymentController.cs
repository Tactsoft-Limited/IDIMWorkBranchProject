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
    public class BillPaymentController : BaseController
    {

        protected IBillTypeService BillTypeService { get; set; }
        protected IBillPaymentService BillPaymentService { get; set; }
        protected IFiscalYearService FiscalYearService { get; set; }
        protected ISubProjectService SubProjectService { get; set; }
        protected IUnitService UnitService { get; set; }

        public BillPaymentController(IActivityLogService activityLogService, IBillTypeService billTypeService, IBillPaymentService billPaymentService, IFiscalYearService fiscalYearService, ISubProjectService subProjectService, IUnitService unitService) : base(activityLogService)
        {
            BillTypeService = billTypeService;
            BillPaymentService = billPaymentService;
            FiscalYearService = fiscalYearService;
            SubProjectService = subProjectService;
            UnitService = unitService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public async Task<ActionResult> List()
        {
            var model = new BillPaymentSearchVm
            {
                BillTypeDropdown = await BillTypeService.GetDropdownAsync(),
                FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(),
                BillPayments = await BillPaymentService.GetByAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> List(BillPaymentSearchVm model)
        {
            model.BillTypeDropdown = await BillTypeService.GetDropdownAsync(model.BillTypeId);
            model.FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(model.FiscalYearId);
            model.BillPayments = await BillPaymentService.GetByAsync(model);

            return View(model);
        }

        public async Task<ActionResult> Create(int id)
        {
            var subproject = await SubProjectService.GetByIdAsync(id);
            if (subproject == null)
                return HttpNotFound();

            var model = new BillPaymentVm
            {
                SubProjectId = subproject.SubProjectId,
                SubProjectTitle = subproject.SubProjectTitle,
                BillTypeDropdown = await BillTypeService.GetDropdownAsync(),
                FiscalYearDropdown = await FiscalYearService.GetDropdownAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(BillPaymentVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await BillPaymentService.InsertAsync(model);

                    ModelState.Clear();
                    model = new BillPaymentVm
                    {
                        SubProjectId = model.SubProjectId,
                        SubProjectTitle = model.SubProjectTitle
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

            model.BillTypeDropdown = await BillTypeService.GetDropdownAsync(model.BillTypeId);
            model.FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(model.FiscalYearId);
            ViewBag.Message = message;

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await BillPaymentService.GetByIdAsync(id);

            if (model == null)
                return HttpNotFound();

            model.BillTypeDropdown = await BillTypeService.GetDropdownAsync(model.BillTypeId);
            model.FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(model.FiscalYearId);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(BillPaymentVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await BillPaymentService.UpdateAsync(model);

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

            model.BillTypeDropdown = await BillTypeService.GetDropdownAsync(model.BillTypeId);
            model.FiscalYearDropdown = await FiscalYearService.GetDropdownAsync(model.FiscalYearId);
            ViewBag.Message = message;

            return View(model);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
                return HttpNotFound();

            int billPaymentId;
            int.TryParse(id, out billPaymentId);

            var model = await BillPaymentService.GetByIdAsync(billPaymentId);


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await BillPaymentService.DeleteAsync(id);
            }
            catch (Exception exception)
            {
                var message = exception.Message;
                var sqlException = exception.GetBaseException() as SqlException;
                if (sqlException?.Number == 547)
                {
                    message = "Can not delete due to reference table.";
                }

                var model = await BillPaymentService.GetByIdAsync(id);


                ViewBag.Message = Messages.Failed(MessageType.Delete.ToString(), message);

                return View(model);
            }

            return RedirectToAction("List");
        }
    }
}
