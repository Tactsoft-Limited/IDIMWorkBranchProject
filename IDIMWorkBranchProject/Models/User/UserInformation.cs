using System.Collections.Generic;

namespace IDIMWorkBranchProject.Models.User
{
    public class UserInformation
    {
        public UserInformation() { MenuInformation = new List<MenuInformation>(); }

        public int UserId { get; set; }

        public int? ArmyId { get; set; }

        public int? UnitId { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string ResourceType { get; set; }

        public string UserType { get; set; }

        public string PersonnelCode { get; set; }

        public string Phone { get; set; }

        public string Avatar { get; set; }

        public bool IsActive { get; set; }
        public int? ActiveStatus { get; set; }

        public bool RememberMe { get; set; }

        public bool PinCodeValidate { get; set; }

        public int ApplicationId { get; set; }

        public int DeviceId { get; set; }

        public IList<MenuInformation> Menus { get; set; }

        public IList<MenuInformation> MenuInformation { get; set; }

        public IList<DeviceVm> Devices { get; set; }

        public IList<ApplicationVm> Applications { get; set; }
    }
}