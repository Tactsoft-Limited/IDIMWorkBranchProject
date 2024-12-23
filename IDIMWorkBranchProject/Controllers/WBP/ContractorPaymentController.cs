using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.WBP;
using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.WBP;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Web.Mvc;
using IDIMWorkBranchProject.Services;

namespace IDIMWorkBranchProject.Controllers.WBP
{
    public class ContractorPaymentController : BaseController
    {
        protected IContractorPaymentService _contractorPaymentService;
        protected ISubProjectService _subProjectService;
        protected IConstructionFirmService _constructionFirmService;

        public ContractorPaymentController(IActivityLogService activityLogService, IContractorPaymentService contractorPaymentService,
            ISubProjectService subProjectService, IConstructionFirmService constructionFirmService) : base(activityLogService)
        {
            _contractorPaymentService = contractorPaymentService;
            _subProjectService = subProjectService;
            _constructionFirmService = constructionFirmService;
        }

        // GET: ContractorPayment
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public async Task<ActionResult> List()
        {
            var model = new ContractorPaymentSearchVm
            {
                SubProjectDropdown = await _subProjectService.GetDropdownAsync(),
                ConstructionFirmDropdown = await _constructionFirmService.GetDropdownAsync()
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
            var model = new ContractorPaymentSearchVm()
            {
                PageIndex = start != null ? Convert.ToInt32(start) / Convert.ToInt32(length) : 0,
                PageSize = length != null ? Convert.ToInt32(length) : 10,  // Default to 10 if no length is provided
                //ConstructionFirmId = !string.IsNullOrEmpty(constructionFirmId) ? Convert.ToInt32(constructionFirmId) : (int?)null,
                //StartDate = !string.IsNullOrEmpty(startDate) ? DateTime.Parse(startDate) : (DateTime?)null,
                //EndDate = !string.IsNullOrEmpty(endDate) ? DateTime.Parse(endDate) : (DateTime?)null,
            };

            try
            {
                var data = await _contractorPaymentService.GetByAsync(model);

                // Return the JSON result
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
            var subProject = await _subProjectService.GetByIdAsync(id);

            if (subProject == null)
                return HttpNotFound();

            var model = new ContractorPaymentVm
            {
                SubProjectId = subProject.SubProjectId,
                SubProjectTitle = subProject.SubProjectTitle,
                ConstructionFirmId = subProject.ConstructionFirmId,
                ConstructionFirmName = subProject.ConstructionFirmName,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ContractorPaymentVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await _contractorPaymentService.InsertAsync(model);

                    ModelState.Clear();
                    model = new ContractorPaymentVm
                    {
                        SubProjectId = model.SubProjectId,
                        SubProjectTitle = model.SubProjectTitle,
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

            model.ConstructionFirmDropdown = await _constructionFirmService.GetDropdownAsync();
            TempData["Message"] = message;

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await _contractorPaymentService.GetByIdAsync(id);

            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ContractorPaymentVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await _contractorPaymentService.UpdateAsync(model);

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

            int contractorPaymentId;
            int.TryParse(id, out contractorPaymentId);

            var model = await _contractorPaymentService.GetByIdAsync(contractorPaymentId);


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _contractorPaymentService.DeleteAsync(id);
            }
            catch (Exception exception)
            {
                var message = exception.Message;
                var sqlException = exception.GetBaseException() as SqlException;
                if (sqlException?.Number == 547)
                {
                    message = "Can not delete due to reference table.";
                }

                var model = await _contractorPaymentService.GetByIdAsync(id);

                TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), message);

                return View(model);
            }

            return RedirectToAction("List");
        }



    }
}