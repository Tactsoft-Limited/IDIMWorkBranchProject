using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class ADPProjectController : BaseController
    {
        private readonly IADPProjectService _aDPProjectService;
        private readonly IProjectDirectorService _projectDirectorService;
        private readonly IFinancialYearAllocationService _financialYearAllocationService;
        private readonly IFiscalYearExpenseService _financialYearExpenseService;
        private readonly IFormalMeetingService _formalMeetingService;
        private readonly IProjectWorkService _projectWorkService;
        private readonly IMapper _mapper;

        public ADPProjectController(IActivityLogService activityLogService, IADPProjectService aDPProjectService, IMapper mapper,
            IProjectDirectorService projectDirectorService, IFinancialYearAllocationService financialYearAllocationService,
            IFiscalYearExpenseService financialYearExpenseService, IFormalMeetingService formalMeetingService,
            IProjectWorkService projectWorkService) : base(activityLogService)
        {
            _aDPProjectService = aDPProjectService;
            _projectDirectorService = projectDirectorService;
            _financialYearAllocationService = financialYearAllocationService;
            _financialYearExpenseService = financialYearExpenseService;
            _formalMeetingService = formalMeetingService;
            _mapper = mapper;
            _projectWorkService = projectWorkService;
        }

        // GET: ProjectDirector
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new ADPProjectSearchVm();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LoadData(ADPProjectSearchVm model)
        {
            try
            {
                var data = await _aDPProjectService.GetPagedAsync(model);
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var model = new ADPProjectDetailVm
                {
                    ADPProjectDetail = _mapper.Map<ADPProjectVm>(await _aDPProjectService.GetByIdAsync(id)),
                    ProjectDirector = _mapper.Map<List<ProjectDirectorVm>>(await _projectDirectorService.GetAllByProjectId(id)),
                    FinancialYearAllocation = _mapper.Map<List<FinancialYearAllocationVm>>(await _financialYearAllocationService.GetAllByProjectId(id)),
                    FiscalYearExpense = _mapper.Map<List<FiscalYearExpenseVm>>(await _financialYearExpenseService.GetAllByProjectId(id)),
                    FormalMeeting = _mapper.Map<List<FormalMeetingVm>>(await _formalMeetingService.GetAllByProjectId(id)),
                    ProjectWorks = await _projectWorkService.GetAllByAdpProjectId(id),
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> Create()
        {
            var model = new ADPProjectVm
            {
                ProjectDirectorDropdown = await _projectDirectorService.DropdownAsync()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ADPProjectVm model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    SetResponseMessage(DefaultMsg.InvalidInput, ResponseType.Error);
                    return View(model);
                }

                var entity = _mapper.Map<ADPProject>(model);
                var result = await _aDPProjectService.CreateAsync(entity);
                SetResponseMessage(string.Format(DefaultMsg.SaveSuccess, result.ProjectTitle), ResponseType.Success);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.SaveFailed, "ADP Project", ex.Message), ResponseType.Error);
                return View(model);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = _mapper.Map<ADPProjectVm>(await _aDPProjectService.GetByIdAsync(id));
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ADPProjectVm model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    SetResponseMessage(DefaultMsg.InvalidInput, ResponseType.Error);
                    return View(model);
                }

                var entity = _mapper.Map<ADPProject>(model);
                var result = await _aDPProjectService.UpdateAsync(entity);
                SetResponseMessage(string.Format(DefaultMsg.UpdateSuccess, result.ProjectTitle), ResponseType.Success);
                return RedirectToAction("Index");  // Redirect to list after success
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.UpdateFailed, "ADP Project", ex.Message), ResponseType.Error);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _aDPProjectService.DeleteAsync(id);
                SetResponseMessage(string.Format(DefaultMsg.DeleteSuccess, "ADP Project"), ResponseType.Success);
            }
            catch (Exception ex)
            {
                SetResponseMessage(string.Format(DefaultMsg.DeleteFailed, "ADP Project", ex.Message), ResponseType.Error);
            }
            return RedirectToAction("Index");
        }

    }
}