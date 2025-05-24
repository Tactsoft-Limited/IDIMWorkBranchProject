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
    public class SignatoryAuthorityController : BaseController
    {
        private readonly ISignatoryAuthorityService _signatoryAuthorityService;
        private readonly IMapper _mapper;

        public SignatoryAuthorityController(
            IActivityLogService activityLogService,
            ISignatoryAuthorityService signatoryAuthorityService,
            IMapper mapper
        ) : base(activityLogService)
        {
            _signatoryAuthorityService = signatoryAuthorityService;
            _mapper = mapper;
        }

        public ActionResult Index() => RedirectToAction(nameof(List));

        public ActionResult List()
        {
            var model = new SignatoryAuthorityVm();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LoadData(SignatoryAuthoritySearchVm model)
        {
            try
            {
                var data = await _signatoryAuthorityService.GetPagedAsync(model);
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        public ActionResult Create()
        {
            var model = new SignatoryAuthorityVm();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SignatoryAuthorityVm model)
        {
            if (!ModelState.IsValid)
            {
                SetResponseMessage(DefaultMsg.InvalidInput, ResponseType.Error);
                return View(model);
            }

            try
            {
                var entity = _mapper.Map<SignatoryAuthority>(model);
                await _signatoryAuthorityService.CreateAsync(entity);

                SetResponseMessage(DefaultMsg.SaveSuccess, ResponseType.Success);
                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.SaveFailed, "Signatory Authority", ex.Message), ResponseType.Error);
                return View(model);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var entity = await _signatoryAuthorityService.GetByIdAsync(id);
            if (entity == null)
            {
                SetResponseMessage("The requested record was not found.", ResponseType.Error);
                return RedirectToAction(nameof(List));
            }

            var model = _mapper.Map<SignatoryAuthorityVm>(entity);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SignatoryAuthorityVm model)
        {
            if (!ModelState.IsValid)
            {
                SetResponseMessage(DefaultMsg.InvalidInput, ResponseType.Error);
                return View(model);
            }

            try
            {
                var entity = _mapper.Map<SignatoryAuthority>(model);
                await _signatoryAuthorityService.UpdateAsync(entity);

                SetResponseMessage(DefaultMsg.UpdateSuccess, ResponseType.Success);
                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.SaveFailed, "Signatory Authority", ex.Message), ResponseType.Error);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _signatoryAuthorityService.DeleteAsync(id);
                SetResponseMessage(string.Format(DefaultMsg.DeleteSuccess, "Signatory Authority"), ResponseType.Success);
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.DeleteFailed, "Signatory Authority", ex.Message), ResponseType.Error);
            }

            return RedirectToAction(nameof(List));
        }
    }
}
