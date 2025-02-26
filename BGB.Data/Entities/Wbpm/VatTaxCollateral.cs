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
    [Table("VatTaxCollateral", Schema = "wbpm")]
    public class VatTaxCollateral:BaseEntity
    {
        [Key]
        public int VatTaxCollateralId { get; set; }
        public int ADPReceivePaymentId { get; set; }
        public double TaxPer { get; set; }
        public decimal TaxAmount { get; set; }
        public double VatPer { get; set; }
        public decimal VatAmount { get; set; }
        public double CollateralPer { get; set; }
        public decimal CollateralAmount { get; set; }
        public decimal TotalDeductionAmount { get; set; }
        public decimal NeetAmount { get; set; }
        public string NeetAmountInWord { get; set; }
        public decimal DepositInBGBFund { get; set; }
        public int BillSubmissionNo { get; set; }
        public DateTime BillSubmissionDate { get; set; }
        public decimal LastBillAmount { get; set; }
        public decimal AllocatedAmountTillNow { get; set; }
        public string AllocatedAmountLetterNo { get; set; }
        public decimal ReducedAllocatedAmountTillNow { get; set; }
        public string ReducedAllocatedAmountLetterNo { get; set; }
        public decimal NetTotalAmount { get; set; }
        public decimal LastBillTotalBalance { get; set; }
        public decimal CurrentBillTotalBalance { get; set; }
        public decimal RelatedWorkBillAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string CodeNo { get; set; }
        public string EconomicCode { get; set; }
        public string VoucherNo { get; set; }

        // Navigation Property
        [ForeignKey(nameof(ADPReceivePaymentId))]
        public ADPReceivePayment ADPReceivePayment { get; set; }
    }
}
