using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.ContractorPayment")]
    public class ContractorPayment
    {
        [Key]
        public int ContractorPaymentId { get; set; }
        public int SubProjectId { get; set; }
        public int ConstructionFirmId { get; set; }
        public decimal ApprovalAmount { get; set; }
        public double TaxPer { get; set; }
        public double VatPer { get; set; }
        public double CollateralPer { get; set; }
        public decimal VatTaxSecurityAmount { get; set; }
        public decimal NetAmount { get; set; }
        public double ProgressPer { get; set; }
        public decimal ProgressAmount { get; set; }
        public double ProgressTaxPer { get; set; }
        public double ProgressVatPer { get; set; }
        public double ProgressCollateralPer { get; set; }
        public decimal DeductionAmount { get; set; }
        public decimal NetDeductionAmount { get; set; }
        public double PerformanceSecurityPer { get; set; }
        public decimal PerformanceSecurityAmount { get; set; }
        public double ContactorProgressPer { get; set; }
        public decimal ContactorProgressAmount { get; set; }
        public int BillPaymentNumber { get; set; }
        public decimal RunningBillPayment { get; set; }
        public decimal ContactorDepositAmount { get; set; }
        public decimal ContactorFinalPaymentAmount { get; set; }
        public decimal BGBFundDepositAmount { get; set; }
        public string Remarks { get; set; }
        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }

        // Navigation properties
        [ForeignKey(nameof(SubProjectId))]
        public virtual SubProject SubProject { get; set; }

        [ForeignKey(nameof(ConstructionFirmId))]
        public virtual ConstructionFirm ConstructionFirm { get; set; }
    }

}
