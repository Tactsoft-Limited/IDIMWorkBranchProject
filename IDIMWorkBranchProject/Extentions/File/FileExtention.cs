using System.IO;
using System;
using System.Net;
using System.Web;

namespace IDIMWorkBranchProject.Extentions.File
{
    public static class FileExtention
    {
        /// <summary>
        /// Upload file to folder and save the file name to the database
        /// </summary>
        /// <param name="file">The file to be uploaded</param>
        /// <param name="uploadFolder">Folder to upload the file to</param>
        /// <param name="connectionString">Connection string to the database</param>
        /// <returns>File name if successful, or an error message</returns>
        public static string UploadFile(this HttpPostedFileBase file, string uploadFolder)
        {
            if (file == null || file.ContentLength == 0)
            {
                return "No file selected.";
            }

            try
            {
                uploadFolder = uploadFolder.TrimEnd(Path.DirectorySeparatorChar);
                uploadFolder.ToFolder();

                string originalFileName = Path.GetFileName(file.FileName);
                string fileExtension = Path.GetExtension(originalFileName);
                string dateTimeString = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string fileName = $"{Path.GetFileNameWithoutExtension(originalFileName)}_{dateTimeString}{fileExtension}";

                string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/" + uploadFolder), fileName);
                file.SaveAs(filePath);
                return fileName;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }


        public static string DeleteFile(string fileName, string uploadFolder)
        {
            try
            {
                uploadFolder = uploadFolder.TrimEnd(Path.DirectorySeparatorChar);
                string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/" + uploadFolder), fileName);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    return "File deleted successfully.";
                }
                else
                {
                    return "File not found.";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }



        /// <summary>
        /// Create folder from path if not exists
        /// </summary>
        /// <param name="path">file path</param>
        public static void ToFolder(this string path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            string fullPath = HttpContext.Current.Server.MapPath("~/" + path);

            var exists = System.IO.Directory.Exists(fullPath);

            if (!exists)
                System.IO.Directory.CreateDirectory(fullPath);
        }


        /// <summary>
        /// Check file exists in http path
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsHttpFileExists(this string value)
        {
            try
            {
                //var request = (HttpWebRequest)WebRequest.Create(value);
                //request.Method = "HEAD";

                //var response = (HttpWebResponse)request.GetResponse();
                //response.Close();
                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }

        /// <summary>
        /// Get full path of thumbnail from file name
        /// </summary>
        /// <param name="value">file name with extention</param>
        /// <returns></returns>
        public static string ToThumbnail(this string value)
        {
            var thumb = DefaultData.DefaultAvatar;

            if (!string.IsNullOrEmpty(value))
            {
                thumb = IsHttpFileExists($"{DefaultData.HttpAvatar}{value}")
                    ? $"{DefaultData.HttpAvatar}{value}"
                    : thumb;
            }

            return thumb;
        }
    }
}