using Microsoft.Reporting.WebForms;
using System.IO;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Extentions.Healper
{
    public static class ReportHelper
    {
        public static ActionResult GenerateReport(string reportPath, string dataSourceName, object data, bool isLandscape, string format)
        {
            if (!System.IO.File.Exists(reportPath))
            {
                throw new FileNotFoundException("File not found", reportPath);
            }

            var lr = new LocalReport { ReportPath = reportPath };
            lr.DataSources.Add(new ReportDataSource(dataSourceName, data));

            string deviceInfo = GenerateDeviceInfo(isLandscape);
            byte[] renderedBytes = RenderReport(lr, format, deviceInfo, out string mimeType);

            return new FileContentResult(renderedBytes, mimeType);
        }

        public static ActionResult GenerateReportSync(string reportPath, string dataSourceName, object data, bool isLandscape, string format)
        {
            if (!System.IO.File.Exists(reportPath))
            {
                throw new FileNotFoundException("File not found", reportPath);
            }

            var lr = new LocalReport { ReportPath = reportPath };
            lr.DataSources.Add(new ReportDataSource(dataSourceName, data));

            string deviceInfo = GenerateDeviceInfo(isLandscape);

            byte[] renderedBytes = RenderReport(lr, format, deviceInfo, out string mimeType);

            return new FileContentResult(renderedBytes, mimeType);
        }

        private static byte[] RenderReport(LocalReport lr, string format, string deviceInfo, out string mimeType)
        {
            byte[] renderedBytes = lr.Render(
                format, deviceInfo, out mimeType,
                out string encoding,
                out string fileNameExtension,
                out string[] streams,
                out Warning[] warnings);
            return renderedBytes;
        }

        private static string GenerateDeviceInfo(bool isLandscape)
        {
            string pageWidth = isLandscape ? "11in" : "8.5in";
            string pageHeight = isLandscape ? "8.5in" : "11in";

            return
                "<DeviceInfo>" +
                "  <OutputFormat>pdf</OutputFormat>" +
                $"  <PageWidth>{pageWidth}</PageWidth>" +
                $"  <PageHeight>{pageHeight}</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>.50in</MarginLeft>" +
                "  <MarginRight>.30in</MarginRight>" +
                "  <MarginBottom>0.40in</MarginBottom>" +
                "</DeviceInfo>";
        }

    }
}