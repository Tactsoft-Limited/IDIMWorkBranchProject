namespace IDIMWorkBranchProject.Extentions.ReportHelper
{
    public class DeviceInfo : IDeviceInfo
    {
        public string OutputFormat { get; set; }

        public DeviceInfo(string outputFormat = "PDF")
        {
            OutputFormat = outputFormat;
        }

        public string Landscape() => GetDeviceInfo("11.69in", "8.27in", "0.5in", "0.5in", "0.5in", "0.5in");

        public string Portrait() => GetDeviceInfo("8.27in", "11.69in", ".25in", ".5in", ".5in", ".25in");

        public string LegalPortrait() => GetDeviceInfo("8.5in", "14in", "0.5in", ".5in", ".5in", "0.5in");

        public string LegalLandscape() => GetDeviceInfo("14in", "8.5in", "0.5in", ".5in", ".5in", "0.5in");

        private string GetDeviceInfo(string pageWidth, string pageHeight, string marginTop, string marginLeft, string marginRight, string marginBottom)
        {
            var infoModel = new DeviceInfoModel
            {
                OutputFormat = OutputFormat,
                PageWidth = pageWidth,
                PageHeight = pageHeight,
                MarginTop = marginTop,
                MarginLeft = marginLeft,
                MarginRight = marginRight,
                MarginBottom = marginBottom
            };
            return Custom(infoModel);
        }

        public string Custom(DeviceInfoModel infoModel)
        {
            return
                $"<DeviceInfo>" +
                $"<OutputFormat>{OutputFormat}</OutputFormat>" +
                $"<PageWidth>{infoModel.PageWidth}</PageWidth>" +
                $"<PageHeight>{infoModel.PageHeight}</PageHeight>" +
                $"<MarginTop>{infoModel.MarginTop}</MarginTop>" +
                $"<MarginLeft>{infoModel.MarginLeft}</MarginLeft>" +
                $"<MarginRight>{infoModel.MarginRight}</MarginRight>" +
                $"<MarginBottom>{infoModel.MarginBottom}</MarginBottom>" +
                $"</DeviceInfo>";
        }
    }

}