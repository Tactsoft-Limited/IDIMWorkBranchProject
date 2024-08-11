using System.Collections.Generic;

namespace IDIMWorkBranchProject.Models.User
{
    public class ApplicationDetailVm
    {
        public ApplicationDetailVm()
        {
            User = new List<UserVm>();
            Menus = new List<MenuVm>();
            Devices = new List<DeviceVm>();
        }

        public ApplicationVm Application { get; set; }
        public IList<UserVm> User { get; set; }
        public IList<MenuVm> Menus { get; set; }
        public IList<DeviceVm> Devices { get; set; }

    }
}