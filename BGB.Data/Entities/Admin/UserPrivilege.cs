using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Admin
{
    [Table("admin.UserPrivileges")]
    public partial class UserPrivilege
    {
        public int UserPrivilegeId { get; set; }

        public int RoleId { get; set; }

        public int UserId { get; set; }

        public int? CreatedUser { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdateNo { get; set; }

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }
    }
}
