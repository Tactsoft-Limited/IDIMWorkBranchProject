using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.User
{
    public class UserDeviceVm
    {
        public int UserDeviceId { get; set; }
        public int DeviceId { get; set; }
        public int UserId { get; set; }
        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }

        public IEnumerable<SelectListItem> UserDropdown { get; set; }
        public IEnumerable<SelectListItem> DeviceDropdown { get; set; }

        //public virtual DeviceVm Device { get; set; }
        //public virtual UserVm User { get; set; }
        
    }
}