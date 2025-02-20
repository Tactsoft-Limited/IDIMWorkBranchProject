using System.Data;
using System.Web;
using System;

namespace IDIMWorkBranchProject.Extentions.ReportHelper
{
    public class ReportConfig
    {
        public string FileName { get; set; }
        public DataTable DataTable { get; set; }
        public string ReportFilePath { get; set; }
        public string ReportSourceName { get; set; }
        public string ReportType { get; set; }
        public string DeviceInfo { get; set; }
        public string PageType { get; set; }
        public bool IsDownloadable { get; set; }
        public HttpRequestBase Request { get; set; } = new HttpRequestWrapper(HttpContext.Current.Request);

        public ReportConfig()
        {
            ReportType = ReportType ?? Request.Params["type"] ?? "pdf";
            ReportType = (ReportType != "excel" && ReportType != "word") ? "pdf" : ReportType;
            IsDownloadable = Request.Params["download"] == "true";
            DeviceInfo = new DeviceInfo(ReportType).Portrait();
            FileName = $"{DateTime.UtcNow.AddHours(6.0):ddMMyyyyHHmmss}";
        }
    }
}