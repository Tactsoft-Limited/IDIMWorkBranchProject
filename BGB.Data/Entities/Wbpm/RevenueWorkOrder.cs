

using BGB.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Wbpm
{
    [Table("RevenueWorkOrder", Schema = "wbpm")]
    public class RevenueWorkOrder:BaseEntity
    {
        [Key]
        public int RevenueWorkOrderId { get; set; }
        public int RevenueId { get; set; }
        public string RevenueWorkOrderLetterNo { get; set; }
        public DateTime? RevenueWorkOrderDate { get; set; }
        public DateTime? RevenueWorkOrderStartDate { get; set; }
        public DateTime? RevenueWorkOrderEndDate { get; set; }
        public DateTime? RevenueWorkOrderHandOverDate { get; set; }
        public string RevenueWorkOrderScanDocument { get; set; }        
    }
}
