using Newtonsoft.Json;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Extentions.Utilities
{
    public static class TempDataExtension
    {
        public static void Put<T>(this TempDataDictionary tempData, string key, T value)
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this TempDataDictionary tempData, string key) where T : class
        {
            if (tempData.ContainsKey(key))
            {
                var obj = tempData[key] as string;
                return string.IsNullOrEmpty(obj) ? null : JsonConvert.DeserializeObject<T>(obj);
            }
            return null;
        }

        public static T Peek<T>(this TempDataDictionary tempData, string key) where T : class
        {
            var obj = tempData.Peek(key) as string;
            return string.IsNullOrEmpty(obj) ? null : JsonConvert.DeserializeObject<T>(obj);
        }
    }
}