using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.User;
using IDIMWorkBranchProject.Services;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using System;
using System.Linq;
using IDIMWorkBranchProject.Models.Common;

namespace IDIMWorkBranchProject.Controllers
{
    public class BaseController : Controller
    {
        protected IActivityLogService ActivityLogService { get; set; }

        public BaseController(IActivityLogService activityLogService) { ActivityLogService = activityLogService; }

        public FileContentResult RenderReport(ReportConfig reportConfig)
        {
            ReportDataSource reportDataSource = new ReportDataSource(
                reportConfig.ReportSourceName,
                reportConfig.DataTable);
            return RenderReport(reportConfig, reportDataSource);
        }

        public FileContentResult RenderReport(ReportConfig reportConfig, ReportDataSource reportDataSource)
        {
            reportConfig.FileName += $"_{DateTime.UtcNow.AddHours(6):dMMMyyyy_hhmmtt}";

            var localReport = new LocalReport { ReportPath = reportConfig.ReportFilePath };
            localReport.DataSources.Add(reportDataSource);

            var deviceInfo = $@"
                            <DeviceInfo>
                                <OutputFormat>PDF</OutputFormat>
                                <PageWidth>{(reportConfig.IsPortrait ? 8.27 : 11.69)}in</PageWidth>
                                <PageHeight>{(reportConfig.IsPortrait ? 11.69 : 8.27)}in</PageHeight>
                                <MarginTop>0.5in</MarginTop>
                                <MarginLeft>.5in</MarginLeft>
                                <MarginRight>.27in</MarginRight>
                                <MarginBottom>0.2in</MarginBottom>
                            </DeviceInfo>";

            var renderedBytes = localReport.Render(
                reportConfig.ReportType,
                deviceInfo,
                out var mimeType,
                out var encoding,
                out var fileNameExtension,
                out _,
                out _);

            Response.AddHeader(
                "content-disposition",
                $"attachment; filename={reportConfig.FileName}.{fileNameExtension}");

            return File(renderedBytes, mimeType);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = Request.RequestContext.RouteData.Values["controller"].ToString().ToLower();
            var userInformation = UserExtention.Get<UserInformation>(nameof(UserInformation));

            if (userInformation == null)
            {
                filterContext.Result = RedirectToLogin(filterContext);
                return;
            }

            if (userInformation != null && DefaultData.OtpEnable && !userInformation.PinCodeValidate)
            {
                filterContext.Result = RedirectToPin(filterContext);
                return;
            }

            if (userInformation.Applications == null ||
                userInformation.Applications.All(app => app.ApplicationId != userInformation.ApplicationId))
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden, "no permission provide.");
                return;
            }

            var menus = userInformation?.Menus.SelectMany(e => e.Menus).ToList();

            if (menus.Any(e => e.ControllerName.ToLower() == controller) ||
                DefaultData.ExcludeMenu.Any(e => e.ToLower() == controller))
            {
                return;
            }
            filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden, "no permission provide.");

            filterContext.Result = RedirectToLogin(filterContext);
        }

        public UserInformation AppUser
        {
            get
            {
                var key = nameof(UserInformation);
                return Session[key] == null ? new UserInformation() : (UserInformation)Session[key];
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var userInformation = UserExtention.Get<UserInformation>(nameof(UserInformation));

            var formData = new Dictionary<string, string>();
            foreach (string key in Request.Form.AllKeys)
            {
                formData[key] = Request.Form[key];
            }
            string jsonData = JsonConvert.SerializeObject(formData);

            var activityModel = new ActivityLogVm()
            {
                ApplicationId = DefaultData.ApplicationId,
                UserRoleId = null,
                SessionId = Request.RequestContext.HttpContext.Session.SessionID,
                Controller = Request.RequestContext.RouteData.Values["controller"].ToString(),
                Action = Request.RequestContext.RouteData.Values["action"].ToString(),
                Url = Request.Url.ToString(),
                RequestType = HttpContext.Request.RequestType,
                ActivityData = jsonData,
                Agent = Request.RequestContext.HttpContext.Request.UserHostAddress,
                Browser = Request.Browser.Browser,
                ActivityTime = DateTime.Now,
            };

            if (userInformation != null)
            {
                activityModel.UserId = userInformation.UserId;
            }

            ActivityLogService.InsertAsync(activityModel);
        }

        private ActionResult RedirectToLogin(ControllerContext context)
        {
            return new RedirectToRouteResult(
                new RouteValueDictionary
                {
                { "controller", "Login" },
                { "action", "Index" },
                {
                    "r",
                    context.HttpContext.Request.Url?.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped)
                }
                });
        }

        private ActionResult RedirectToPin(ControllerContext context)
        {
            return new RedirectToRouteResult(
                new RouteValueDictionary
                {
                { "controller", "Login" },
                { "action", "Pin" },
                {
                    "r",
                    context.HttpContext.Request.Url?.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped)
                }
                });
        }
    }
}