using AutoMapper;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.Wbpm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Controllers.Wbpm
{
    public class CurrentBillCheckController : BaseController
    {
        private readonly ICurrentBillCheckService _currentBillCheckService;
        private readonly ISignatoryAuthorityService _signatoryAuthorityService;
        private readonly IProjectWorkService _projectWorkService;
        private readonly IMapper _mapper;
        public CurrentBillCheckController(IActivityLogService activityLogService, ICurrentBillCheckService currentBillCheckService, IMapper mapper, ISignatoryAuthorityService signatoryAuthorityService, IProjectWorkService projectWorkService) : base(activityLogService)
        {
            _currentBillCheckService = currentBillCheckService;
            _mapper = mapper;
            _signatoryAuthorityService = signatoryAuthorityService;
            _projectWorkService = projectWorkService;
        }

        // GET: CurrentBillCheck
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Create(int id)
        {
            var projectWork = await _projectWorkService.GetByIdAsync(id);
            var model = new CurrentBillCheckVm
            {
                ProjectWorkId = projectWork.ProjectWorkId,                
                ProjectWorkTitleB = projectWork.ProjectWorkTitleB,                           
                OfficersDropdown = await _signatoryAuthorityService.GetDropdownAsync(),
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CurrentBillCheckVm model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());                
                model.OfficersDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.OfficerId);
                return View(model);
            }

            try
            {

                var entity = _mapper.Map<CurrentBillCheck>(model);
                var result = await _currentBillCheckService.CreateAsync(entity);
                SetResponseMessage(string.Format(DefaultMsg.SaveSuccess, "Current Bill Check"), ResponseType.Success);
                return RedirectToAction("Details", "ProjectWork", new { id = model.ProjectWorkId });
            }
            catch (Exception exception)
            {
                SetResponseMessage(string.Format(DefaultMsg.SaveFailed, "Current Bill Check", exception.Message), ResponseType.Error);                
                model.OfficersDropdown = await _signatoryAuthorityService.GetDropdownAsync(model.OfficerId);
                return View(model);
            }
        }
    }
}