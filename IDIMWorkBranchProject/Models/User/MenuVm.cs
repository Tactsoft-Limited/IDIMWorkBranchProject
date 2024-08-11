using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.User
{
    public class MenuVm
    {
        [DisplayName("Id")]
        public int MenuId { get; set; }

        public string Title { get; set; }

        [DisplayName("Controller")]
        public string ControllerName { get; set; }

        public string Icon { get; set; }

        [DisplayName("Application")]
        public int ApplicationId { get; set; }
        [DisplayName("Application")]
        public string ApplicationName { get; set; }

        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }

        public IEnumerable<SelectListItem> ApplicationDropdown { get; set; }

    }
}