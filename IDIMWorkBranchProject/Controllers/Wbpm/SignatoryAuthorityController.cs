using IDIMWorkBranchProject.Models.WBP;
using System.Threading.Tasks;
using System;
using System.Web.Mvc;
using IDIMWorkBranchProject.Extentions;
using System.Data.SqlClient;
using IDIMWorkBranchProject.Services.Wbpm;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using AutoMapper;
using BGB.Data.Entities.Wbpm;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class SignatoryAuthorityController : BaseController
    {
        protected readonly ISignatoryAuthorityService _signatoryAuthorityService;
        protected readonly IMapper _mapper;
        public SignatoryAuthorityController(IActivityLogService activityLogService, ISignatoryAuthorityService signatoryAuthorityService, IMapper mapper) : base(activityLogService)
        {
            _signatoryAuthorityService = signatoryAuthorityService;
            _mapper = mapper;
        }



        // GET: SignatoryAuthority
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            var model = new SignatoryAuthoritySearchVm();

            return View(model);
        }

        //[HttpPost]
        //public async Task<ActionResult> LoadData()
        //{
        //    // Fetch filter parameters from the request
        //    var length = Request.Form.GetValues("length")?.FirstOrDefault();
        //    var start = Request.Form.GetValues("start")?.FirstOrDefault();
        //    //var constructionFirmId = Request.Form.GetValues("constructionFirmId")?.FirstOrDefault();
        //    //var startDate = Request.Form.GetValues("startDate")?.FirstOrDefault();
        //    //var endDate = Request.Form.GetValues("endDate")?.FirstOrDefault();

        //    // Construct the view model with filters
        //    var model = new SignatoryAuthoritySearchVm()
        //    {
        //        PageIndex = start != null ? Convert.ToInt32(start) / Convert.ToInt32(length) : 0,
        //        PageSize = length != null ? Convert.ToInt32(length) : 10,  // Default to 10 if no length is provided
        //        //ConstructionFirmId = !string.IsNullOrEmpty(constructionFirmId) ? Convert.ToInt32(constructionFirmId) : (int?)null,
        //        //StartDate = !string.IsNullOrEmpty(startDate) ? DateTime.Parse(startDate) : (DateTime?)null,
        //        //EndDate = !string.IsNullOrEmpty(endDate) ? DateTime.Parse(endDate) : (DateTime?)null,
        //    };

        //    try
        //    {
        //        var data = await _signatoryAuthorityService.GetByAsync(model);

        //        // Return the JSON result
        //        return Json(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any errors that occur during data fetching
        //        return Json(new { error = ex.Message });
        //    }
        //}


        public ActionResult Create()
        {
            var model = new SignatoryAuthorityVm();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SignatoryAuthorityVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    var entity = _mapper.Map<SignatoryAuthority>(model);                    
                    await _signatoryAuthorityService.CreateAsync(entity);

                    ModelState.Clear();             

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

            //model.ProjectDropdown = await _projectService.GetDropdownAsync(model.ProjectId);
            //model.ReceivePaymentDropdown = await _receivePaymentService.GetDropdownAsync(model.ReceivePaymentId);

            TempData["Message"] = message;

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await _signatoryAuthorityService.GetByIdAsync(id);

            if (model == null)
                return HttpNotFound();

            

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(SignatoryAuthorityVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    var entity = _mapper.Map<SignatoryAuthority>(model);
                    await _signatoryAuthorityService.UpdateAsync(entity);

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

            var model = await _signatoryAuthorityService.GetByIdAsync(projectId);


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _signatoryAuthorityService.DeleteAsync(id);
            }
            catch (Exception exception)
            {
                var message = exception.Message;
                var sqlException = exception.GetBaseException() as SqlException;
                if (sqlException?.Number == 547)
                {
                    message = "Can not delete due to reference table.";
                }

                var model = await _signatoryAuthorityService.GetByIdAsync(id);


                TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), message);

                return View(model);
            }

            return RedirectToAction("List");
        }
    }
}