using System.ComponentModel.DataAnnotations;

namespace IDIMWorkBranchProject.Models.User
{
    public class DeviceAssignVm
    {
        public DeviceAssignVm()
        {
            Device = new DeviceVm();
        }

        [Display(Name = "Is Assigned")]
        public bool IsAssigned { get; set; }

        public DeviceVm Device { get; set; }
    }
}