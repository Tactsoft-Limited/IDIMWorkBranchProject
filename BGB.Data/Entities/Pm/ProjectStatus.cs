using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.ProjectStatus")]
    public partial class ProjectStatus
    {
        [Key]
        public int ProjectStatusId { get; set; }

        public int SubProjectId { get; set; }

        [StringLength(255)]
        public string Progress { get; set; }

        public int? PictureId { get; set; }

        public DateTime? StatusDate { get; set; }

        public string Description { get; set; }

        public string Remark { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual SubProject SubProject { get; set; }
    }
}
