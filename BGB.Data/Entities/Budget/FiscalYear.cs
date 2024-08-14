using BGB.Data.Entities.Pm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Budget
{
    [Table("budget.FiscalYear")]
    public partial class FiscalYear
    {
        public FiscalYear()
        {
            BillPayments = new HashSet<BillPayment>();
            ReceiptPayments = new HashSet<ReceiptPayment>();
        }

        public int FiscalYearId { get; set; }

        [Required]
        [StringLength(50)]
        public string FiscalYearDescription { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }



        public virtual ICollection<BillPayment> BillPayments { get; set; }
        public virtual ICollection<ReceiptPayment> ReceiptPayments { get; set; }
    }
}
