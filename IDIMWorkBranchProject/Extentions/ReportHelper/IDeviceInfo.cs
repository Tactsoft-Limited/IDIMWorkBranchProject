namespace IDIMWorkBranchProject.Extentions.ReportHelper
{
	public interface IDeviceInfo
	{
		string Landscape();

		string Portrait();

		string LegalPortrait();

		string LegalLandscape();

		string Custom(DeviceInfoModel infoModel);
	}

}
