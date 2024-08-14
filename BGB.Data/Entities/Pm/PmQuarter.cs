using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.Quarter")]
    public partial class PmQuarter
    {
        public PmQuarter() { ReceiptPayments = new HashSet<ReceiptPayment>(); }

        [Key]
        public int QuarterId { get; set; }

        [Required]
        [StringLength(255)]
        public string QuarterName { get; set; }

        public int YearFrom { get; set; }

        public int? YearTo { get; set; }

        public int MonthFrom { get; set; }

        public int MonthTo { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual ICollection<ReceiptPayment> ReceiptPayments { get; set; }
    }
}
