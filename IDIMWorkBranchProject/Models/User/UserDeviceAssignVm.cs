using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IDIMWorkBranchProject.Models.User
{
    public class UserDeviceAssignVm
    {
        public UserDeviceAssignVm()
        {
            Devices = new List<DeviceAssignVm>();
        }

        [Display(Name = "Id")]
        public int UserId { get; set; }

        [Display(Name = "Regiment No.")]
        public string RegimentNo { get; set; }

        public string Username { get; set; }

        public IList<DeviceAssignVm> Devices { get; set; }
    }
}