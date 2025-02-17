using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
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
        public RecruitmentCommitteeController(IActivityLogService activityLogService, IRecruitmentCommitteeService recruitmentCommitteeService, IMapper mapper) : base(activityLogService)
        {
            _recruitmentCommitteeService = recruitmentCommitteeService;
            _mapper = mapper;
        }

        // GET: RecruitmentCommittee
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
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
        public async Task<ActionResult> Create()
        {
            var model = new RecruitmentCommitteeVm();
           
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RecruitmentCommitteeVm model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
                    return View(model);
                }

                var entity = _mapper.Map<RecruitmentCommittee>(model);
                await _recruitmentCommitteeService.CreateAsync(entity);
                TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                return RedirectToAction("List");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), exception.Message);
                return View(model);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {

            var model = _mapper.Map<RecruitmentCommitteeVm>(await _recruitmentCommitteeService.GetByIdAsync(id));
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RecruitmentCommitteeVm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = _mapper.Map<RecruitmentCommittee>(model);
                    await _recruitmentCommitteeService.UpdateAsync(entity);
                    TempData["Message"] = Messages.Success(MessageType.Update.ToString());
                    return RedirectToAction("List", "RecruitmentCommittee");  // Reset model after success
                }

                TempData["Message"] = Messages.InvalidInput(MessageType.Update.ToString());
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Update.ToString(), exception.Message);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _recruitmentCommitteeService.GetByIdAsync(id);

            if (entity == null)
            {
                TempData["Message"] = "The requested record was not found.";
                return RedirectToAction("List", "RecruitmentCommittee");
            }
            var model = _mapper.Map<RecruitmentCommitteeVm>(entity);
            return View(model); // Load the delete confirmation view
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(RecruitmentCommitteeVm model)
        {
            var entity = await _recruitmentCommitteeService.GetByIdAsync(model.RecruitmentCommitteeId);
            try
            {

                if (entity == null)
                {
                    TempData["Message"] = "Record Not Found";
                    return RedirectToAction("List", "RecruitmentCommittee");
                }

                await _recruitmentCommitteeService.DeleteAsync(entity);

                TempData["Message"] = Messages.Success(MessageType.Delete.ToString());
                return RedirectToAction("List", "RecruitmentCommittee");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Delete.ToString(), exception.InnerException?.Message);
                return RedirectToAction("List", "RecruitmentCommittee"); // Avoids null reference
            }
        }
    }
}