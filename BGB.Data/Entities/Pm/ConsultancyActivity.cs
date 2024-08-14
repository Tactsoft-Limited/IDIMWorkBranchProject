using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.ConsultancyActivities")]
    public partial class ConsultancyActivity
    {
        [Key]
        public int CAId { get; set; }

        public int SubProjectId { get; set; }

        public int ConsultantId { get; set; }

        public int? Progress { get; set; }

        public int? ConsultantProgressForSupervission { get; set; }

        public int? ProgressAccordingToContract { get; set; }

        public double? AgreedMoney { get; set; }

        public double? PaidMoney { get; set; }

        public double? PayableForProgress { get; set; }

        public double? RestOfPayableForProgress { get; set; }

        public string Remark { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual Consultant Consultant { get; set; }

        public virtual SubProject SubProject { get; set; }
    }
}
