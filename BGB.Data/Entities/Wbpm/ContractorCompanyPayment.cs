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
        public string LetterNo { get; set; }
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
        public decimal ProgressTaxAmount { get; set; }
        public double ProgressVatPer { get; set; }
        public decimal ProgressVatAmount { get; set; }
        public double ProgressCollateralPer { get; set; }
        public decimal ProgressCollateralAmount { get; set; }
        public decimal ProgressDeductionAmount { get; set; }
        public decimal NetProgressAmount { get; set; }
        public double PerformanceSecurityPer { get; set; }
        public decimal PerformanceSecurityAmount { get; set; }
        public double ContactorProgressPer { get; set; }
        public decimal ContactorProgressAmount { get; set; }
        public string ContractorCompanyPaymentDocument { get; set; }
        public int BillPaymentNumber { get; set; }
        public int PreviouslyTotalPaidNo { get; set; }
        public decimal PreviouslyPaidAmount { get; set; }
        public decimal PayableAmountOnCurrentBill { get; set; }
        public decimal FinalPaymentAmount { get; set; }
        public string FinalPaymentAmountInWord { get; set; }
        public decimal? TotalDepositsInFund { get; set; }
        public decimal WillBeDepositedInFund { get; set; }
        public string Remarks { get; set; }

        public int? HeadAssistantId { get; set; }
        public int? ConcernedEngineerId { get; set; }
        public int? SectionICId { get; set; }
        public int? BranchClerkId { get; set; }
        public int? OfficerId { get; set; }


        #region Navigation Property
        [ForeignKey(nameof(ProjectWorkId))]
        public virtual ProjectWork ProjectWork { get; set; }
        [ForeignKey(nameof(BranchClerkId))]
        public virtual SignatoryAuthority BranchClerk { get; set; }

        [ForeignKey(nameof(ConcernedEngineerId))]
        public virtual SignatoryAuthority ConcernedEngineer { get; set; }

        [ForeignKey(nameof(HeadAssistantId))]
        public virtual SignatoryAuthority HeadAssistant { get; set; }

        [ForeignKey(nameof(SectionICId))]
        public virtual SignatoryAuthority SectionIC { get; set; }

        [ForeignKey(nameof(OfficerId))]
        public virtual SignatoryAuthority Officer { get; set; }
        #endregion
    }
}
