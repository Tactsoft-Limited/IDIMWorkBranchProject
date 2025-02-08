using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.ProjectExtend")]
    public partial class ProjectExtend
    {
        public int ProjectExtendId { get; set; }

        public int SubProjectId { get; set; }

        public int NoOfDays { get; set; }

        public DateTime? ExtendDate { get; set; }

        public DateTime? NewHandOverDate { get; set; }

        public int? AttachmentId { get; set; }

        public string Reason { get; set; }

        public string Remark { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual SubProject SubProject { get; set; }
    }
}
