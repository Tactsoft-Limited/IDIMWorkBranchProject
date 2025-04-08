using AutoMapper;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System.Threading.Tasks;
using System;
using System.Web.Mvc;
using BGB.Data.Entities.Wbpm;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class ConstructionCompanyController : BaseController
    {
        private readonly IConstructionCompanyService _constructionCompanyService;
        private readonly IMapper _mapper;

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
                var data = await _constructionCompanyService.GetPagedAsync(model);
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
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
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = _mapper.Map<ConstructionCompany>(model);
                    await _constructionCompanyService.CreateAsync(entity);
                    TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                    return View(new ConstructionCompanyVm());  // Reset model after success
                }

                TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), exception.Message);
            }

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = _mapper.Map<ConstructionCompanyVm>(await _constructionCompanyService.GetByIdAsync(id));
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ConstructionCompanyVm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = _mapper.Map<ConstructionCompany>(model);
                    await _constructionCompanyService.UpdateAsync(entity);
                    TempData["Message"] = Messages.Success(MessageType.Update.ToString());
                    return RedirectToAction("Index");  // Reset model after success
                }

                TempData["Message"] = Messages.InvalidInput(MessageType.Update.ToString());
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Update.ToString(), exception.Message);
            }

            return View(model);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _constructionCompanyService.GetByIdAsync(id);

            if (entity == null)
            {
                TempData["Message"] = "The requested record was not found.";
                return RedirectToAction("List", "ConstructionCompany");
            }

            var model = _mapper.Map<ConstructionCompanyVm>(entity);
            return View(model); // Load the delete confirmation view
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(ConstructionCompanyVm model)
        {
            var entity = await _constructionCompanyService.GetByIdAsync(model.ConstructionCompanyId);
            try
            {

                if (entity == null)
                {
                    TempData["Message"] = "Record Not Found";
                    return RedirectToAction("List", "ConstructionCompany");
                }

                await _constructionCompanyService.DeleteAsync(entity);

                TempData["Message"] = Messages.Success(MessageType.Delete.ToString());
                return RedirectToAction("List", "ConstructionCompany");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), exception.InnerException?.Message);
                return RedirectToAction("List", "ConstructionCompany"); // Avoids null reference
            }
        }



    }
}