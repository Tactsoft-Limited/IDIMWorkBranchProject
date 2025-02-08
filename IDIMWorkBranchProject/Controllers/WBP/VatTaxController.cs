using IDIMWorkBranchProject.Models.WBP;
using IDIMWorkBranchProject.Services.WBP;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Web.Mvc;
using IDIMWorkBranchProject.Extentions;
using System.Data.SqlClient;

namespace IDIMWorkBranchProject.Controllers.WBP
{
    public class VatTaxController : Controller
    {
        protected IVatTaxService _vatTaxService;
        protected IProjectService _projectService;
        protected IReceivePaymentService _receivePaymentService;

        public VatTaxController(IVatTaxService taxService, IProjectService projectService, IReceivePaymentService receivePaymentService)
        {
            _vatTaxService = taxService;
            _projectService = projectService;
            _receivePaymentService = receivePaymentService;
        }

        // GET: VatTax
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public async Task<ActionResult> List()
        {
            var model = new VatTaxSearchVm
            {
                ProjectDropdown = await _projectService.GetDropdownAsync(),
                ReceivePaymentDropdown = await _receivePaymentService.GetDropdownAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LoadData()
        {
            // Fetch filter parameters from the request
            var length = Request.Form.GetValues("length")?.FirstOrDefault();
            var start = Request.Form.GetValues("start")?.FirstOrDefault();
            //var constructionFirmId = Request.Form.GetValues("constructionFirmId")?.FirstOrDefault();
            //var startDate = Request.Form.GetValues("startDate")?.FirstOrDefault();
            //var endDate = Request.Form.GetValues("endDate")?.FirstOrDefault();

            // Construct the view model with filters
            var model = new VatTaxSearchVm()
            {
                PageIndex = start != null ? Convert.ToInt32(start) / Convert.ToInt32(length) : 0,
                PageSize = length != null ? Convert.ToInt32(length) : 10,  // Default to 10 if no length is provided
                //ConstructionFirmId = !string.IsNullOrEmpty(constructionFirmId) ? Convert.ToInt32(constructionFirmId) : (int?)null,
                //StartDate = !string.IsNullOrEmpty(startDate) ? DateTime.Parse(startDate) : (DateTime?)null,
                //EndDate = !string.IsNullOrEmpty(endDate) ? DateTime.Parse(endDate) : (DateTime?)null,
            };

            try
            {
                var data = await _vatTaxService.GetByAsync(model);

                // Return the JSON result
                return Json(data);
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during data fetching
                return Json(new { error = ex.Message });
            }
        }

        public async Task<ActionResult> Create(int receivePaymentId, int projectId)
        {
            var project = await _projectService.GetByIdAsync(projectId);

            if (project == null)
                return HttpNotFound();

            var receivePayment = await _receivePaymentService.GetByIdAsync(receivePaymentId);

            if (receivePayment == null)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), "No Receive Payment found. Please create a receive payment before proceeding with the payment.");
                return RedirectToAction("Create", "ReceivePayment", new { id = projectId });
            }

            var vatTaxId = await _vatTaxService.GetByProjectAndPaymentAsync(projectId, receivePaymentId);

            if (vatTaxId > 0)
            {
                return RedirectToAction("Edit", "VatTax", new { id = vatTaxId });
            }

            var model = new VatTaxVm
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                ReceivePaymentId = receivePayment.ReceivePaymentId,
                LetterNumber = receivePayment.LetterNo,
                ReceiveAmount = receivePayment.BillAmount,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(VatTaxVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await _vatTaxService.InsertAsync(model);

                    ModelState.Clear();
                    model = new VatTaxVm
                    {
                        ProjectId = model.ProjectId,
                        ProjectName = model.ProjectName,
                        ReceivePaymentId = model.ReceivePaymentId,
                        LetterNumber = model.LetterNumber,
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

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await _vatTaxService.GetByIdAsync(id);

            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(VatTaxVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await _vatTaxService.UpdateAsync(model);

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

            return View(model);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
                return HttpNotFound();

            int projectId;
            int.TryParse(id, out projectId);

            var model = await _vatTaxService.GetByIdAsync(projectId);


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _vatTaxService.DeleteAsync(id);
            }
            catch (Exception exception)
            {
                var message = exception.Message;
                var sqlException = exception.GetBaseException() as SqlException;
                if (sqlException?.Number == 547)
                {
                    message = "Can not delete due to reference table.";
                }

                var model = await _vatTaxService.GetByIdAsync(id);


                TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), message);

                return View(model);
            }

            return RedirectToAction("List");
        }

    }
}