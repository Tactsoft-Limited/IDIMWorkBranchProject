using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Admin
{
    [Table("admin.RolePrivileges")]
    public partial class RolePrivilege
    {
        public int RolePrivilegeId { get; set; }

        public int RoleId { get; set; }

        public int MenuId { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdateNo { get; set; }

        public virtual Menu Menu { get; set; }

        public virtual Role Role { get; set; }
    }
}
