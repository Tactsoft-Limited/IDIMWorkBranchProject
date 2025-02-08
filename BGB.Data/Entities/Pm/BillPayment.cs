using BGB.Data.Entities.Budget;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.BillPayment")]
    public partial class BillPayment
    {
        [Key]
        public int BillPaymentId { get; set; }

        public int FiscalYearId { get; set; }

        public int SubProjectId { get; set; }

        public int BillTypeId { get; set; }

        public int SourceType { get; set; }

        public int PaymentType { get; set; }

        public double PaymentAmount { get; set; }

        public DateTime PaymentDate { get; set; }

        [StringLength(255)]
        public string BankName { get; set; }

        [StringLength(255)]
        public string BranchName { get; set; }

        [StringLength(255)]
        public string AccountName { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual FiscalYear FiscalYear { get; set; }

        public virtual BillType BillType { get; set; }

        public virtual SubProject SubProject { get; set; }
    }
}
