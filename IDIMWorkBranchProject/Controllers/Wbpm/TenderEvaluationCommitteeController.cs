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
    public class TenderEvaluationCommitteeController : BaseController
    {
        private ITenderEvaluationCommitteeService _tenderEvaluationCommitteeService;
        private IADPProjectService _aDPProjectService;
        private IRecruitmentCommitteeService _recruitmentCommitteeService;
        private IMapper _mapper;
        public TenderEvaluationCommitteeController(IActivityLogService activityLogService, ITenderEvaluationCommitteeService tenderEvaluationCommitteeService, IMapper mapper, IADPProjectService aDPProjectService, IRecruitmentCommitteeService recruitmentCommitteeService) : base(activityLogService)
        {
            _tenderEvaluationCommitteeService = tenderEvaluationCommitteeService;
            _mapper = mapper;
            _aDPProjectService = aDPProjectService;
            _recruitmentCommitteeService = recruitmentCommitteeService;
        }

        // GET: TenderEvaluationCommittee
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            var model = new TenderEvaluationCommitteeVm();
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> LoadData(TenderEvaluationCommitteeSearchVm model)
        {
            try
            {
                var data = await _tenderEvaluationCommitteeService.GetPagedAsync(model);
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        public async Task<ActionResult> Create(int id)
        {
            var aDPProject = await _aDPProjectService.GetByIdAsync(id);
            var tec = await _tenderEvaluationCommitteeService.GetByAdpProjectIdAsync(aDPProject.ADPProjectId);

            var model = new TenderEvaluationCommitteeVm
            {
                ADPProjectId = aDPProject.ADPProjectId,
                ADPProjectTitle = aDPProject.ProjectTitle,
                TenderEvaluationCommitteeId = tec?.TenderEvaluationCommitteeId ?? 0, // Null check for tec
            };

            // Populate dropdowns
            await PopulateDropdownsAsync(model, tec);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TenderEvaluationCommitteeVm model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
                    await PopulateDropdownsAsync(model, null); // Populate dropdowns with null check
                    return View(model);
                }

                var entity = _mapper.Map<TenderEvaluationCommittee>(model);

                // Handle creation or update based on the TenderEvaluationCommitteeId
                if (model.TenderEvaluationCommitteeId > 0)
                {
                    await _tenderEvaluationCommitteeService.UpdateAsync(entity);
                    TempData["Message"] = Messages.Success(MessageType.Update.ToString());
                }
                else
                {
                    await _tenderEvaluationCommitteeService.CreateAsync(entity);
                    TempData["Message"] = Messages.Success(MessageType.Create.ToString());
                }

                // Populate dropdowns before redirect
                await PopulateDropdownsAsync(model, null);

                return RedirectToAction("details/" + model.ADPProjectId, "ADPProject");
            }
            catch (Exception exception)
            {
                TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), $"An error occurred while processing your request.{exception.InnerException.Message}");

                // Populate dropdowns even if an error occurs
                await PopulateDropdownsAsync(model, null);

                return View(model);
            }
        }

        private async Task PopulateDropdownsAsync(TenderEvaluationCommitteeVm model, TenderEvaluationCommittee tec)
        {
            if (tec != null)
            {
                model.AddDGDropdown = await _recruitmentCommitteeService.GetDropdownAsync(tec.AddDG);
                model.DDGDropdown = await _recruitmentCommitteeService.GetDropdownAsync(tec.DDG);
                model.ProjectdirectorDropdown = await _recruitmentCommitteeService.GetDropdownAsync(tec.ProjectDirector);
                model.DirectorDropdown = await _recruitmentCommitteeService.GetDropdownAsync(tec.Director);
            }
            else
            {
                model.AddDGDropdown = await _recruitmentCommitteeService.GetDropdownAsync(model.AddDG);
                model.DDGDropdown = await _recruitmentCommitteeService.GetDropdownAsync(model.DDG);
                model.ProjectdirectorDropdown = await _recruitmentCommitteeService.GetDropdownAsync(model.ProjectDirector);
                model.DirectorDropdown = await _recruitmentCommitteeService.GetDropdownAsync(model.Director);
            }
        }

    }
}