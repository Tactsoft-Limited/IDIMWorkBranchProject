using BGB.Data.Entities.Budget;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.ReceiptPayment")]
    public partial class ReceiptPayment
    {
        public int ReceiptPaymentId { get; set; }

        public int ProjectId { get; set; }

        public int FiscalYearId { get; set; }

        public int QuarterId { get; set; }

        public int DocumentType { get; set; }

        public int SourceType { get; set; }

        public double ReceiptAmount { get; set; }

        public DateTime? ReceiptDate { get; set; }

        public string Remark { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual FiscalYear FiscalYear { get; set; }

        public virtual Project Project { get; set; }

        public virtual PmQuarter Quarter { get; set; }
    }
}
