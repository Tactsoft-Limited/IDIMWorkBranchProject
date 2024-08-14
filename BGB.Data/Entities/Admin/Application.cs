using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Admin
{
    [Table("admin.Applications")]
    public partial class Application
    {
        public Application()
        {
            ActivityLogs = new HashSet<ActivityLog>();
            Menus = new HashSet<Menu>();
            Roles = new HashSet<Role>();
        }

        [Key]
        public int ApplicationId { get; set; }

        [Required]
        [StringLength(255)]
        public string ApplicationCode { get; set; }

        [Required]
        [StringLength(255)]
        public string ApplicationName { get; set; }

        [Required]
        [StringLength(150)]
        public string ApplicationShortName { get; set; }

        public int Priority { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        [Required]
        [StringLength(255)]
        public string Url { get; set; }

        [StringLength(50)]
        public string Icon { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int? UpdateNo { get; set; }

        public bool? IsPublished { get; set; }

        [StringLength(500)]
        public string IconImagePath { get; set; }

        public virtual ICollection<ActivityLog> ActivityLogs { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
