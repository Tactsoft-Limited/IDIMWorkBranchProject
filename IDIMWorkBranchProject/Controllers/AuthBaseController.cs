﻿using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Models.User;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;
using System;
using IDIMWorkBranchProject.Services;

namespace IDIMWorkBranchProject.Controllers
{
    public class AuthBaseController : Controller
    {
        protected IActivityLogService ActivityLogService { get; set; }

        public AuthBaseController(IActivityLogService activityLogService) { ActivityLogService = activityLogService; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
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
    }
}