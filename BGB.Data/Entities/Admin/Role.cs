using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Admin
{
    [Table("admin.Roles")]
    public partial class Role
    {
        public Role()
        {
            RolePrivileges = new HashSet<RolePrivilege>();
            UserPrivileges = new HashSet<UserPrivilege>();
        }

        public int RoleId { get; set; }

        public int ApplicationId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool? IsActive { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int? UpdateNo { get; set; }

        public virtual Application Application { get; set; }

        public virtual ICollection<RolePrivilege> RolePrivileges { get; set; }

        public virtual ICollection<UserPrivilege> UserPrivileges { get; set; }
    }
}
