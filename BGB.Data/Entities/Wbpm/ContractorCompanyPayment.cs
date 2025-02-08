using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using BGB.Data.Entities.Base;

namespace BGB.Data.Entities.Wbpm
{
    [Table("ContractorCompanyPayment", Schema = "wbpm")]
    public class ContractorCompanyPayment : BaseEntity
    {


        [Key]
        public int ContractorCompanyPaymentId { get; set; }

        public int ProjectWorkId { get; set; }
        public decimal EstimatedCost { get; set; }
        public float EstimatedCostTaxPer { get; set; }
        public decimal EstimatedCostTaxAmount { get; set; }
        public float EstimatedCostVatPer { get; set; }
        public decimal EstimatedCostVatAmount { get; set; }
        public float EstimatedCostCollateralPer { get; set; }
        public decimal EstimatedCostCollateralAmount { get; set; }
        public decimal EstimatedCostDeductionAmount { get; set; }
        public decimal NetEstimatedCostAmount { get; set; }
        public float ProgressPer { get; set; }
        public decimal ProgressAmount { get; set; }
        public float ProgressTaxPer { get; set; }
        public float ProgressVatPer { get; set; }
        public float ProgressCollateralPer { get; set; }
        public decimal ProgressDeductionAmount { get; set; }
        public decimal NetProgressAmount { get; set; }
        public float PerformanceSecurityPer { get; set; }
        public decimal PerformanceSecurityAmount { get; set; }
        public float ContactorProgressPer { get; set; }
        public decimal ContactorProgressAmount { get; set; }
        public int BillPaymentNumber { get; set; }
        public decimal PreviouslyPaidAmount { get; set; }
        public decimal PayableAmountOnCurrentBill { get; set; }
        public decimal FinalPaymentAmount { get; set; }
        public string Remarks { get; set; }


        #region Navigation Property
        [ForeignKey(nameof(ProjectWorkId))]
        public virtual ProjectWork ProjectWork { get; set; }
        #endregion
    }
}
