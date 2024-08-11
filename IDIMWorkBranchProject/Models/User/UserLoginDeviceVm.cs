using System;
using System.ComponentModel;

namespace IDIMWorkBranchProject.Models.User
{
    public class UserLoginDeviceVm
    {
        [DisplayName("Id")]
        public int UserDeviceId { get; set; }

        [DisplayName("User")]
        public int UserId { get; set; }
        [DisplayName("User")]
        public string Username { get; set; }

        [DisplayName("Device")]
        public int DeviceId { get; set; }
        [DisplayName("Device")]
        public string DeviceName { get; set; }

        public string Session { get; set; }
        [DisplayName("Validation Code")]
        public string ValidationCode { get; set; }

        [DisplayName("In Time")]
        public DateTime LoggedIn { get; set; }

        [DisplayName("Out Time")]
        public DateTime? Loggedout { get; set; }

        public virtual DeviceVm Device { get; set; }
        public virtual UserVm User { get; set; }
    }
}