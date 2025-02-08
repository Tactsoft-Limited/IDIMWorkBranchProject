using System;
using System.ComponentModel.DataAnnotations;

namespace IDIMWorkBranchProject.Models.User
{
    public class MenuVm
    {
        public int MenuId { get; set; }

        public string Title { get; set; }

        public string ControllerName { get; set; }

        public int ApplicationId { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        [Display(Name = "Type")]
        public MenuType MenuType { get; set; }

        public int Priority { get; set; }

        public string Icon { get; set; } = "check";

    }
}