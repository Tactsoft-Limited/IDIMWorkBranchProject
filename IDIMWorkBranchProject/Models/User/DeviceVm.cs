using System;
using System.ComponentModel;
using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.User;

namespace IDIMWorkBranchProject.Models.User
{
    [Validator(typeof(DeviceVmValidator))]
    public class DeviceVm
    {
        [DisplayName("Id")]
        public int DeviceId { get; set; }

        [DisplayName("Name")]
        public string DeviceName { get; set; }

        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }

        //public virtual ICollection<UserDeviceVm> UserDevices { get; set; }
        //public virtual ICollection<UserLoginDeviceVm> UserLoginDevices { get; set; }
    }
}