using System;
using System.ComponentModel;
using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.User;

namespace IDIMWorkBranchProject.Models.User
{
    [Validator(typeof(UserVmValidator))]
    public class UserVm
    {
        [DisplayName("Id")]
        public int UserId { get; set; }

        [DisplayName("Regiment No.")]
        public int? ArmyId { get; set; }
        [DisplayName("Regiment No.")]
        public string RegimentNo { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        [DisplayName("Resource Type")]
        public int ResourceType { get; set; }

        [DisplayName("User Type")]
        public int UserType { get; set; }

        [DisplayName("Personnel Code")]
        public string PersonnelCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }
        
        //public virtual ICollection<Application> Applications { get; set; }
        //public virtual ICollection<UserDevice> UserDevices { get; set; }
        //public virtual ICollection<UserLoginDevice> UserLoginDevices { get; set; }
        //public virtual ICollection<UserPriviledge> UserPriviledges { get; set; }
    }
}