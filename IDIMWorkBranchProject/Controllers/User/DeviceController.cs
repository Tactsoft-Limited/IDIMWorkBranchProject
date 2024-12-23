using System.Web.Mvc;
using IDIMWorkBranchProject.Services.User;

namespace IDIMWorkBranchProject.Controllers.User
{
    public class DeviceController : Controller
    {
        protected IDeviceService DeviceService { get; set; }

        public DeviceController(IDeviceService deviceService)
        {
            DeviceService = deviceService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        //public async Task<ActionResult> List()
        //{
        //    var devices = await DeviceService.GetAllAsync();

        //    return View(devices);
        //}

        //public ActionResult Create()
        //{
        //    return View(new DeviceVm());
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create(DeviceVm model)
        //{
        //    Message message;
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            await DeviceService.InsertAsync(model);
        //            ModelState.Clear();
        //            model = new DeviceVm();
        //            message = Messages.Success(MessageType.Create.ToString());
        //        }
        //        catch (Exception exception)
        //        {
        //            message = Messages.Failed(MessageType.Create.ToString(), exception.Message);
        //        }
        //    }
        //    else
        //    {
        //        message = Messages.InvalidInput(MessageType.Create.ToString());
        //    }

        //    ViewBag.Message = message;

        //    return View(model);
        //}

        //public async Task<ActionResult> Edit(int id)
        //{
        //    var model = await DeviceService.GetByIdAsync(id);
        //    if (model == null)
        //        return HttpNotFound();

        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit(DeviceVm model)
        //{
        //    Message message;
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            await DeviceService.UpdateAsync(model);
        //            message = Messages.Success(MessageType.Update.ToString());
        //        }
        //        catch (Exception exception)
        //        {
        //            message = Messages.Failed(MessageType.Update.ToString(), exception.Message);
        //        }
        //    }
        //    else
        //    {
        //        message = Messages.InvalidInput(MessageType.Update.ToString());
        //    }

        //    ViewBag.Message = message;

        //    return View(model);
        //}

        //public async Task<ActionResult> Delete(string id)
        //{
        //    int.TryParse(id, out int deviceId);

        //    var model = await DeviceService.GetByIdAsync(deviceId);
        //    if (model == null)
        //        return HttpNotFound();

        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        await DeviceService.DeleteAsync(id);
        //    }
        //    catch (Exception exception)
        //    {
        //        var model = await DeviceService.GetByIdAsync(id);
        //        ViewBag.Message = Messages.Failed(MessageType.Delete.ToString(), exception.Message);

        //        return View(model);
        //    }

        //    return RedirectToAction("List");
        //}

        ///// <summary>
        ///// All devcies data
        ///// </summary>
        ///// <returns>All devices data in JSON format</returns>
        //public async Task<ActionResult> Get()
        //{
        //    var devices = await DeviceService.GetAllAsync();

        //    var data = new
        //    {
        //        data = devices
        //    };

        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
    }
}