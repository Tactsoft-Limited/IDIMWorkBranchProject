using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.WBP;
using IDIMWorkBranchProject.Services;
using IDIMWorkBranchProject.Services.WBP;

namespace IDIMWorkBranchProject.Controllers.WBP
{
    public class SubProjectDetailsController : BaseController
    {
        protected ISubProjectDetailsService SubProjectDetailsService { get; set; }
        protected ISubProjectService SubProjectService { get; set; }
        public SubProjectDetailsController(IActivityLogService activityLogService, ISubProjectDetailsService subProjectDetailsService, ISubProjectService subProjectService) : base(activityLogService)
        {
            SubProjectDetailsService = subProjectDetailsService;
            SubProjectService = subProjectService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public async Task<ActionResult> List()
        {

            var model = new SubProjectDetailsSearchVm
            {
                SubProjectDropdown = await SubProjectService.GetDropdownAsync(),
                SubProjectDetails = await SubProjectDetailsService.GetByAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> List(SubProjectDetailsSearchVm model)
        {
            model.SubProjectDropdown = await SubProjectService.GetDropdownAsync();
            model.SubProjectDetails = await SubProjectDetailsService.GetByAsync(model);

            return View(model);
        }

        public async Task<ActionResult> Create()
        {
            var model = new SubProjectDetailsVm
            {
                SubProjectDropdown = await SubProjectService.GetDropdownAsync(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(SubProjectDetailsVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await SubProjectDetailsService.InsertAsync(model);

                    ModelState.Clear();
                    model = new SubProjectDetailsVm();
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
            model.SubProjectDropdown = await SubProjectService.GetDropdownAsync();
            ViewBag.Message = message;

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await SubProjectDetailsService.GetByIdAsync(id);

            if (model == null)
                return HttpNotFound();
            model.SubProjectDropdown = await SubProjectService.GetDropdownAsync(model.SubProjectId);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(SubProjectDetailsVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await SubProjectDetailsService.UpdateAsync(model);

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

            model.SubProjectDropdown = await SubProjectService.GetDropdownAsync(model.SubProjectId);
            ViewBag.Message = message;

            return View(model);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
                return HttpNotFound();

            int subProjectDetailsId;
            int.TryParse(id, out subProjectDetailsId);

            var model = await SubProjectDetailsService.GetByIdAsync(subProjectDetailsId);


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await SubProjectDetailsService.DeleteAsync(id);
            }
            catch (Exception exception)
            {
                var message = exception.Message;
                var sqlException = exception.GetBaseException() as SqlException;
                if (sqlException?.Number == 547)
                {
                    message = "Can not delete due to reference table.";
                }

                var model = await SubProjectDetailsService.GetByIdAsync(id);


                ViewBag.Message = Messages.Failed(MessageType.Delete.ToString(), message);

                return View(model);
            }

            return RedirectToAction("List");
        }
    }
}