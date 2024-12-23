using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.VatTax")]
    public class VatTax
    {
        [Key]
        public int VatTaxId { get; set; }
        public int ProjectId { get; set; }
        public int ReceivePaymentId { get; set; }
        public double TaxPer { get; set; }
        public decimal TaxAmount { get; set; }
        public double VatPer { get; set; }
        public decimal VatAmount { get; set; }
        public double CollateralPer { get; set; }
        public decimal CollateralAmount { get; set; }
        public string Remarks { get; set; }
        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public virtual Project Project { get; set; }

        [ForeignKey(nameof(ReceivePaymentId))]
        public virtual ReceivePayment ReceivePayment { get; set; }
    }
}
