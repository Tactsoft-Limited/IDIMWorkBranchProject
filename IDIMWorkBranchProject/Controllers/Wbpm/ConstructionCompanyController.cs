using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

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
                if (!ModelState.IsValid)
                {
                    SetResponseMessage(string.Format(DefaultMsg.InvalidInput), ResponseType.Error);
                    return View(model);  // Reset model after success
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
        public async Task<ActionResult> Edit(ConstructionCompanyVm model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    SetResponseMessage(string.Format(DefaultMsg.InvalidInput), ResponseType.Error);
                    return View(model);  // Reset model after success
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
                await _constructionCompanyService.DeleteAsync(id);
                SetResponseMessage(string.Format(DefaultMsg.DeleteSuccess, "Construction Company"), ResponseType.Success);
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.DeleteFailed, "Construction Company", ex.Message), ResponseType.Error);
            }
            return RedirectToAction("Index");
        }
    }
}