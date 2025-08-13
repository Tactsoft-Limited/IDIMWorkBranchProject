using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BGB.Data.SqlViews.Wbpm
{
    [Table("ViewVatTaxReport", Schema = "wbpm")]
    public class ViewVatTaxReport
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VatTAxCollateralId { get; set; }
        public int ADPReceivePaymentId { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectWorkTitleB { get; set; }
        public string FirmNameB { get; set; }
        public decimal BillPaidAmount { get; set; }
        public string BillPaidAmountInWord { get; set; }
        public string BillPaymentSector { get; set; }

        public int BillNumber { get; set; }
        public double TaxPer { get; set; }
        public decimal TaxAmount { get; set; }
        public double VatPer { get; set; }
        public decimal VatAmount { get; set; }
        public double CollateralPer { get; set; }
        public decimal CollateralAmount { get; set; }
        public decimal TotalDeductionAmount { get; set; }
        public string TotalDeductionAmountInWord { get; set; }
        public decimal NeetAmount { get; set; }
        public string NeetAmountInWord { get; set; }
        public string CodeNo { get; set; }
        public string EconomicCode { get; set; }
        public string VoucherNo { get; set; }
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
        public string FirmAddressB { get; set; }
        public string OwnerNameB { get; set; }
        public string AuthorityNameB { get; set; }
        public string DesignationB { get; set; }
        public string Recruitment { get; set; }
        public string onThePartOf { get; set; }
        public string LetterNo { get; set; }
        public DateTime WorkOrderDate { get; set; }
        public string WorkOrderDateB { get; set; }

    }
}
