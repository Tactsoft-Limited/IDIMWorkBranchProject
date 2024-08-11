using System.ComponentModel.DataAnnotations;

namespace IDIMWorkBranchProject.Models.User
{
    public class MenuAssignVm
    {
        [Display(Name = "Is Assigned")]
        public bool IsAssigned { get; set; }

        public MenuVm Menu { get; set; }
    }
}