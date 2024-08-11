using System.Web;
using IDIMWorkBranchProject.Models.User;

namespace IDIMWorkBranchProject.Extentions.Session
{
    public static class UserExtention
    {
        public static T Get<T>(string key)
        {
            object sessionObject = HttpContext.Current.Session[key];
            if (sessionObject == null)
            {
                return default(T);
            }
            return (T)HttpContext.Current.Session[key];

        }

        public static T Get<T>(string key, T defaultValue)
        {
            object sessionObject = HttpContext.Current.Session[key];
            if (sessionObject == null)
            {
                HttpContext.Current.Session[key] = defaultValue;
            }

            return (T)HttpContext.Current.Session[key];
        }

        public static void Save<T>(string key, T entity)
        {
            HttpContext.Current.Session[key] = entity;
            HttpContext.Current.Session.Timeout = 60 * 24; // 1 day session
        }

        public static void Save(UserInformation userInformation)
        {
            Save(nameof(UserInformation), userInformation);
        }

        public static void Remove(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }

        public static int GetUserId()
        {
            return 1;//Get<UserInformation>(nameof(UserInformation)).UserId;
        }

        public static int? GetApplicationId()
        {
            return 5;//Get<UserInformation>(nameof(UserInformation)).ApplicationId;
        }

        public static int? GetDeviceId()
        {
            return Get<UserInformation>(nameof(UserInformation)).DeviceId;
        }

    }
}