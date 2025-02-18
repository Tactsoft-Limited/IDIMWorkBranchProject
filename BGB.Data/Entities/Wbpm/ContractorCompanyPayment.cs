using System.ComponentModel.DataAnnotations;
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
        public double EstimatedCostTaxPer { get; set; }
        public decimal EstimatedCostTaxAmount { get; set; }
        public double EstimatedCostVatPer { get; set; }
        public decimal EstimatedCostVatAmount { get; set; }
        public double EstimatedCostCollateralPer { get; set; }
        public decimal EstimatedCostCollateralAmount { get; set; }
        public decimal EstimatedCostDeductionAmount { get; set; }
        public decimal NetEstimatedCostAmount { get; set; }
        public double ProgressPer { get; set; }
        public decimal ProgressAmount { get; set; }
        public double ProgressTaxPer { get; set; }
        public double ProgressVatPer { get; set; }
        public double ProgressCollateralPer { get; set; }
        public decimal ProgressDeductionAmount { get; set; }
        public decimal NetProgressAmount { get; set; }
        public double PerformanceSecurityPer { get; set; }
        public decimal PerformanceSecurityAmount { get; set; }
        public double ContactorProgressPer { get; set; }
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
