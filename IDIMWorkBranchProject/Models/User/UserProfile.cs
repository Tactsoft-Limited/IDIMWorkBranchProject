using IDIMWorkBranchProject.Models.Setup;
using System.Collections.Generic;

namespace IDIMWorkBranchProject.Models.User
{
    public class UserProfile
    {
        public UserVm User { get; set; }

        public GeneralInformationVm Regiment { get; set; }

        public IList<MenuGroupByVm> Menus { get; set; }

        public IList<ApplicationVm> Applications { get; set; }
    }
}