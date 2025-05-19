using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using BGB.Data.Entities.Base;

namespace BGB.Data.Entities.Wbpm
{
    [Table("PerformanceSecurity", Schema = "wbpm")]
    public class PerformanceSecurity : BaseEntity
    {
        [Key]
        public int PerformanceSecurityId { get; set; }
        public int ProjectWorkId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string ScanDocument { get; set; }


        [ForeignKey(nameof(ProjectWorkId))]
        public virtual ProjectWork ProjectWork { get; set; }
    }
}
