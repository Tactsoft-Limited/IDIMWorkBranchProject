using AutoMapper;

using BGB.Data.Entities.Wbpm;

using IDIMWorkBranchProject.Extentions;
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
    public class FormalMeetingController : BaseController
    {
        protected readonly IFormalMeetingService _formalMeetingService;
		protected readonly IADPProjectService _aDPProjectService;
        protected readonly IMapper _mapper;
		public FormalMeetingController(IActivityLogService activityLogService, IFormalMeetingService formalMeetingService, IMapper mapper, IADPProjectService aDPProjectService) : base(activityLogService)
		{
			_formalMeetingService = formalMeetingService;
			_mapper = mapper;
			_aDPProjectService = aDPProjectService;
		}

		// GET: FormalMeeting
		public ActionResult Index()
        {
            return View();
        }

		public async Task<ActionResult> Create(int id)
		{
			var data = await _aDPProjectService.GetByIdAsync(id);
			var model = new FormalMeetingVm
			{
				ADPProjectId = data.ADPProjectId,
				ProjectTitle = data.ProjectTitle,
			};
			return View(model);
		}

		[HttpPost]
		public async Task<ActionResult> Create(FormalMeetingVm model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					TempData["Message"] = Messages.InvalidInput(MessageType.Create.ToString());
					return View(model);
				}

				var entity = _mapper.Map<FormalMeeting>(model);
				await _formalMeetingService.CreateAsync(entity);
				TempData["Message"] = Messages.Success(MessageType.Create.ToString());
				return RedirectToAction("details/" + model.ADPProjectId, "ADPProject");
			}
			catch (Exception exception)
			{
				TempData["Message"] = Messages.Failed(MessageType.Create.ToString(), exception.Message);
				return View(model);
			}
		}

		public async Task<ActionResult> Edit(int id)
		{

			var model = _mapper.Map<FormalMeetingVm>(await _formalMeetingService.GetByIdAsync(id));
			model.ProjectTitle = await _aDPProjectService.GetAdpProjectTitle(model.ADPProjectId);
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(FinancialYearAllocationVm model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					TempData["Message"] = Messages.InvalidInput(MessageType.Update.ToString());
					return View(model);
				}

				var entity = _mapper.Map<FormalMeeting>(model);
				await _formalMeetingService.UpdateAsync(entity);

				TempData["Message"] = Messages.Success(MessageType.Update.ToString());
				return RedirectToAction("details/" + model.ADPProjectId, "ADPProject");  // Redirect to list after success
			}
			catch (Exception exception)
			{
				TempData["Message"] = Messages.Failed(MessageType.Update.ToString(), exception.InnerException.Message);
				return View(model);
			}
		}

	}
}