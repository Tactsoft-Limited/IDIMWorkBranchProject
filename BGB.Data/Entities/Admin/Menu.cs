using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Admin
{
    [Table("admin.Menus")]
    public partial class Menu
    {
        public Menu()
        {
            RolePrivileges = new HashSet<RolePrivilege>();
        }

        public int MenuId { get; set; }

        public int ApplicationId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string ControllerName { get; set; }

        public int MenuType { get; set; }

        public int Priority { get; set; }

        [StringLength(50)]
        public string Icon { get; set; }

        public bool IsPublished { get; set; }

        [StringLength(550)]
        public string Remark { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        [ForeignKey(nameof(ApplicationId))]
        public virtual Application Application { get; set; }



        public virtual ICollection<RolePrivilege> RolePrivileges { get; set; }
    }
}
