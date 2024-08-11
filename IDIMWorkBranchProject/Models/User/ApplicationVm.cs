using System;
using System.Collections.Generic;
using System.ComponentModel;
using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.User;

namespace IDIMWorkBranchProject.Models.User
{
    [Validator(typeof(ApplicationVmValidator))]
    public class ApplicationVm
    {
        [DisplayName("Id")]
        public int ApplicationId { get; set; }

        [DisplayName("Name")]
        public string ApplicationName { get; set; }

        [DisplayName("Code")]
        public string ApplicationCode { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool IsPublished { get; set; }

        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdateUser { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public int UpdateNo { get; set; }

        public virtual ICollection<MenuVm> Menus { get; set; }
        public virtual ICollection<UserApplicationVm> UserApplications { get; set; }
    }
}