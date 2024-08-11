using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.User
{
    public class UserSearchVm
    {
        public UserSearchVm()
        {
            DeviceDropdown = new List<SelectListItem>();
            ApplicationDropdown = new List<SelectListItem>();
            Users = new List<UserVm>();
        }

        public string Username { get; set; }
        public string Regiment { get; set; }

        [DisplayName("Application")]
        public int? ApplicationId { get; set; }

        [DisplayName("Device")]
        public int? DeviceId { get; set; }

        [DisplayName("Active")]
        public Active IsActive { get; set; }

        public IEnumerable<SelectListItem> DeviceDropdown { get; set; }
        public IEnumerable<SelectListItem> ApplicationDropdown { get; set; }
        public IList<UserVm> Users { get; set; }
    }

    public enum Active
    {
        All,
        Active,

        [Display(Name = "Not Active")]
        NotActive
    }
}