namespace IDIMWorkBranchProject.Extentions.ReportHelper
{
	public class DeviceInfo : IDeviceInfo
	{
		public string OutputFormat { get; set; }

		public DeviceInfo(string outputFormat = "PDF")
		{
			OutputFormat = outputFormat;
		}

		public string Landscape()
		{
			DeviceInfoModel infoModel = new DeviceInfoModel
			{
				OutputFormat = OutputFormat,
				PageWidth = "11.69in",
				PageHeight = "8.27in",
				MarginTop = "0.5in",
				MarginLeft = "0.5in",
				MarginRight = "0.5in",
				MarginBottom = "0.5in"
			};
			return Custom(infoModel);
		}

		public string Portrait()
		{
			DeviceInfoModel infoModel = new DeviceInfoModel
			{
				OutputFormat = OutputFormat,
				PageWidth = "8.27in",
				PageHeight = "11.69in",
				MarginTop = ".25in",
				MarginLeft = ".5in",
				MarginRight = ".5in",
				MarginBottom = ".25in"
			};
			return Custom(infoModel);
		}

		public string LegalPortrait()
		{
			DeviceInfoModel infoModel = new DeviceInfoModel
			{
				OutputFormat = OutputFormat,
				PageWidth = "8.5in",
				PageHeight = "14in",
				MarginTop = "0.5in",
				MarginLeft = ".5in",
				MarginRight = ".5in",
				MarginBottom = "0.5in"
			};
			return Custom(infoModel);
		}

		public string LegalLandscape()
		{
			DeviceInfoModel infoModel = new DeviceInfoModel
			{
				OutputFormat = OutputFormat,
				PageWidth = "14in",
				PageHeight = "8.5in",
				MarginTop = "0.5in",
				MarginLeft = ".5in",
				MarginRight = ".5in",
				MarginBottom = "0.5in"
			};
			return Custom(infoModel);
		}

		public string Custom(DeviceInfoModel infoModel)
		{
			return "<DeviceInfo>  <OutputFormat>" + OutputFormat + "</OutputFormat>  <PageWidth>" + infoModel.PageWidth + "</PageWidth>  <PageHeight>" + infoModel.PageHeight + "</PageHeight>  <MarginTop>" + infoModel.MarginTop + "</MarginTop>  <MarginLeft>" + infoModel.MarginLeft + "</MarginLeft>  <MarginRight>" + infoModel.MarginRight + "</MarginRight>  <MarginBottom>" + infoModel.MarginBottom + "</MarginBottom></DeviceInfo>";
		}
	}
}