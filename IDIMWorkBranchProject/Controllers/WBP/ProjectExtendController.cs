using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.WBP;
using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.WBP;

namespace IDIMWorkBranchProject.Controllers.WBP
{
    public class ProjectExtendController : Controller
    {
        protected IBillTypeService BillTypeService { get; set; }
        protected IProjectExtendService ProjectExtendService { get; set; }
        protected IFiscalYearService FiscalYearService { get; set; }
        protected ISubProjectService SubProjectService { get; set; }
        protected IUnitService UnitService { get; set; }

        public ProjectExtendController(
            IBillTypeService billTypeService,
            IProjectExtendService projectExtendService,
            IFiscalYearService fiscalYearService,
            ISubProjectService subProjectService,
            IUnitService unitService)
        {
            BillTypeService = billTypeService;
            ProjectExtendService = projectExtendService;
            FiscalYearService = fiscalYearService;
            SubProjectService = subProjectService;
            UnitService = unitService;
        }

        public async Task<ActionResult> List()
        {
            var model = new ProjectExtendSearchVm
            {
                ProjectExtends = await ProjectExtendService.GetByAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> List(ProjectExtendSearchVm model)
        {
            model.ProjectExtends = await ProjectExtendService.GetByAsync(model);

            return View(model);
        }

        public async Task<ActionResult> Create(int id)
        {
            var subproject = await SubProjectService.GetByIdAsync(id);
            if (subproject == null)
                return HttpNotFound();

            var model = new ProjectExtendVm
            {
                SubProjectId = subproject.SubProjectId,
                SubProjectTitle = subproject.SubProjectTitle
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProjectExtendVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await ProjectExtendService.InsertAsync(model);

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

            ViewBag.Message = message;

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await ProjectExtendService.GetByIdAsync(id);

            if (model == null)
                return HttpNotFound();


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ProjectExtendVm model)
        {
            Message message;

            try
            {
                if (ModelState.IsValid)
                {
                    await ProjectExtendService.UpdateAsync(model);

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

            ViewBag.Message = message;

            return View(model);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
                return HttpNotFound();

            int projectExtendId;
            int.TryParse(id, out projectExtendId);

            var model = await ProjectExtendService.GetByIdAsync(projectExtendId);


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await ProjectExtendService.DeleteAsync(id);
            }
            catch (Exception exception)
            {
                var message = exception.Message;
                var sqlException = exception.GetBaseException() as SqlException;
                if (sqlException?.Number == 547)
                {
                    message = "Can not delete due to reference table.";
                }

                var model = await ProjectExtendService.GetByIdAsync(id);


                ViewBag.Message = Messages.Failed(MessageType.Delete.ToString(), message);

                return View(model);
            }

            return RedirectToAction("List");
        }
    }
}
