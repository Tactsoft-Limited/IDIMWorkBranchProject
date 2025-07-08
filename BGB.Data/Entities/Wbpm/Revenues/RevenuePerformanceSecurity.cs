using BGB.Data.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Wbpm
{
    [Table("RevenuePerformanceSecurity", Schema = "wbpm")]
    public class RevenuePerformanceSecurity:BaseEntity
    {
        [Key]
        public int RevenuePerformanceSecurityId { get; set; }
        public int RevenueId { get; set; }
        public DateTime RevenuePerformanceSecuritySubmissionDate { get; set; }
        public DateTime RevenuePerformanceSecurityExpiryDate { get; set; }
        public string RevenuePerformanceSecurityScanDocument { get; set; }
    }
}
