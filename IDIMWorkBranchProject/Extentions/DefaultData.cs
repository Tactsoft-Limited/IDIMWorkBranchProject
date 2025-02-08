using System.Collections.Generic;
using System.Configuration;
using System;

namespace IDIMWorkBranchProject.Extentions
{
    public static class DefaultData
    {
        public static bool OtpEnable = ConfigurationManager.AppSettings["OtpEnable"].ToBool();
        public static string AdServer = ConfigurationManager.AppSettings["ADServer"];
        public static string AdUsername = ConfigurationManager.AppSettings["ADUsername"];
        public static string AdPassword = ConfigurationManager.AppSettings["ADPassword"];
        public static string HttpAvatar = ConfigurationManager.AppSettings["HttpAvatar"];
        public static bool SmsEnable = ConfigurationManager.AppSettings["SmsEnable"].ToBool();

        //Email Configuration
        public static string EmailServer = ConfigurationManager.AppSettings["EmailServer"];
        public static int EmailPort = Convert.ToInt32(ConfigurationManager.AppSettings["EmailPort"]);
        public static string EmailUserName = ConfigurationManager.AppSettings["EmailUserName"];
        public static string EmailPassword = ConfigurationManager.AppSettings["EmailPassword"];
        public static string EmailForm = ConfigurationManager.AppSettings["EmailForm"];
        public static string EmailSubject = ConfigurationManager.AppSettings["EmailSubject"];
        public static string EmailBody = ConfigurationManager.AppSettings["EmailBody"];
        public static int OTPExpiredTime = Convert.ToInt32(ConfigurationManager.AppSettings["OTPExpiredTime"]);

        public static int ApplicationId = 7;
        public static string ApplicationName = "IDIMWorkBranchProject";
        //public static int CommStoreHeadUnitId = 228;
        public static string AvatarLocation = "/Images/Profile";
        public static string TempLocation = "/Images/Temp";

        public static string DefaultAvatar = "/Images/Others/avatar.png";
        public static string FamilyMemberLocation = "/Images/FamilyMember";
        public static string DefaultDateFormat = "dd-MMM-yyyy";
        public static int Take = 100;

        public static List<string> ExcludeMenu = new List<string>
        {
            "Login",
            "Account",
            "Manage",
            "Error",
            "Profile",
            "Setting",
            "Help"
        };



    }

}