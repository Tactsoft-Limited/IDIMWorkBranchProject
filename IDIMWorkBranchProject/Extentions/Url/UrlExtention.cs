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

            var path = string.Concat(DefaultData.AvatarLocation, pictureName);
            var file = System.Web.HttpContext.Current.Server.MapPath(url);

            if (System.IO.File.Exists(file))
            {
                return path;
            }

            return url;
        }
    }
}