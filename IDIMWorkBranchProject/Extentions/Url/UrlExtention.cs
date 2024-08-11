using System.IO;
using System.Web;

namespace IDIMWorkBranchProject.Extentions.Url
{
    public static class UrlExtention
    {
        public static string GetAvatar(string pictureName)
        {
            var url = DefaultData.DefaultAvatar;

            if (string.IsNullOrEmpty(pictureName))
            {
                return url;
            }

            var path = string.Concat(DefaultData.AvatarLocation, "/", "avatar.png");
           // var path = string.Concat(DefaultData.AvatarLocation, "/", pictureName);
            var file = HttpContext.Current.Server.MapPath(path);

            if (File.Exists(file))
            {
                return path;
            }

            return url;
        }
    }
}