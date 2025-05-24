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
    public class RecruitmentCommitteeController : BaseController
    {
        private readonly IRecruitmentCommitteeService _recruitmentCommitteeService;
        private readonly IMapper _mapper;

        public RecruitmentCommitteeController(
            IActivityLogService activityLogService,
            IRecruitmentCommitteeService recruitmentCommitteeService,
            IMapper mapper
        ) : base(activityLogService)
        {
            _recruitmentCommitteeService = recruitmentCommitteeService;
            _mapper = mapper;
        }

        public ActionResult Index() => RedirectToAction(nameof(List));

        public ActionResult List()
        {
            var model = new RecruitmentCommitteeVm();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LoadData(RecruitmentCommitteeSearchVm model)
        {
            try
            {
                var data = await _recruitmentCommitteeService.GetPagedAsync(model);
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        public ActionResult Create()
        {
            var model = new RecruitmentCommitteeVm();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RecruitmentCommitteeVm model)
        {
            if (!ModelState.IsValid)
            {
                SetResponseMessage(DefaultMsg.InvalidInput, ResponseType.Error);
                return View(model);
            }

            try
            {
                var entity = _mapper.Map<RecruitmentCommittee>(model);
                await _recruitmentCommitteeService.CreateAsync(entity);

                SetResponseMessage(string.Format(DefaultMsg.SaveSuccess, "Recruitment Committee"), ResponseType.Success);
                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.SaveFailed, "Recruitment Committee", ex.Message), ResponseType.Error);
                return View(model);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var entity = await _recruitmentCommitteeService.GetByIdAsync(id);
            if (entity == null)
            {
                SetResponseMessage("The requested record was not found.", ResponseType.Error);
                return RedirectToAction(nameof(List));
            }

            var model = _mapper.Map<RecruitmentCommitteeVm>(entity);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RecruitmentCommitteeVm model)
        {
            if (!ModelState.IsValid)
            {
                SetResponseMessage(DefaultMsg.InvalidInput, ResponseType.Error);
                return View(model);
            }

            try
            {
                var entity = _mapper.Map<RecruitmentCommittee>(model);
                await _recruitmentCommitteeService.UpdateAsync(entity);

                SetResponseMessage(string.Format(DefaultMsg.UpdateSuccess, "Recruitment Committee"), ResponseType.Success);
                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.SaveFailed, "Recruitment Committee", ex.Message), ResponseType.Error);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _recruitmentCommitteeService.DeleteAsync(id);
                SetResponseMessage(string.Format(DefaultMsg.DeleteSuccess, "Recruitment Committee"), ResponseType.Success);
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.DeleteFailed, "Recruitment Committee", ex.Message), ResponseType.Error);
            }

            return RedirectToAction("List");
        }
    }
}
