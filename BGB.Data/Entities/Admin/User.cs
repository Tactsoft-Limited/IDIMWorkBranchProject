using BGB.Data.Entities.Irms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Admin
{
    [Table("admin.Users")]
    public partial class User
    {
        public User()
        {
            ActivityLogs = new HashSet<ActivityLog>();
            UserPrivileges = new HashSet<UserPrivilege>();
        }

        [Key]
        public int UserId { get; set; }

        [ForeignKey(nameof(GeneralInformation))]
        public int? ArmyId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public int ResourceType { get; set; }

        [StringLength(50)]
        public string PersonnelCode { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [Column(TypeName = "text")]
        public string Remark { get; set; }

        public bool IsActive { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        [StringLength(100)]
        public string OtpCode { get; set; }

        public DateTime? OtpGeneratedDate { get; set; }

        public virtual ICollection<ActivityLog> ActivityLogs { get; set; }

        public virtual ICollection<UserPrivilege> UserPrivileges { get; set; }

        public virtual GeneralInformation GeneralInformation { get; set; }

        //public virtual GeneralInformation GeneralInformation1 { get; set; }
    }
}
