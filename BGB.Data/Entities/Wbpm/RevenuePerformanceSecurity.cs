using BGB.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGB.Data.Entities.Wbpm
{
    [Table("RevenuePerformanceSecurity", Schema = "wbpm")]
    public class RevenuePerformanceSecurity:BaseEntity
    {
        [Key]
        public int RevenuePerformanceSecurityId { get; set; }
        public int ProjectWorkId { get; set; }
        public DateTime RevenuePerformanceSecuritySubmissionDate { get; set; }
        public DateTime RevenuePerformanceSecurityExpiryDate { get; set; }
        public string RevenuePerformanceSecurityScanDocument { get; set; }


        [ForeignKey(nameof(ProjectWorkId))]
        public virtual ProjectWork ProjectWork { get; set; }
    }
}
