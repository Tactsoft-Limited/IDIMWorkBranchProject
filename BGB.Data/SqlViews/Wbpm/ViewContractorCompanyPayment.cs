using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.SqlViews.Wbpm
{
    [Table("ViewContractorCompanyPayment", Schema = "wbpm")]
    public class ViewContractorCompanyPayment
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ContractorCompanyPaymentId { get; set; }
        public int ProjectWorkId { get; set; }
        public string LetterNo { get; set; }
        public decimal EstimatedCost { get; set; }
        public decimal EstimatedCostTaxAmount { get; set; }
        public double EstimatedCostTaxPer { get; set; }
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
        public decimal ProgressDeductionAmount { get; set; }
        public decimal ProgressCollateralAmount { get; set; }
        public decimal NetProgressAmount { get; set; }
        public double PerformanceSecurityPer { get; set; }
        public decimal PerformanceSecurityAmount { get; set; }
        public double ContactorProgressPer { get; set; }
        public decimal ContactorProgressAmount { get; set; }
        public int BillPaymentNumber { get; set; }
        public int PreviouslyTotalPaidNo { get; set; }
        public decimal PreviouslyPaidAmount { get; set; }
        public decimal PayableAmountOnCurrentBill { get; set; }
        public decimal FinalPaymentAmount { get; set; }
        public string FinalPaymentAmountInWord { get; set; }
        public decimal TotalDepositsInFund { get; set; }
        public decimal WillBeDepositedInFund { get; set; }
        public string Remarks { get; set; }
        public string ProjectWorkTitleB { get; set; }
        public DateTime WorkStartDate { get; set; }
        public DateTime WorkEndDate { get; set; }
        public string FirmNameB { get; set; }

        public string HeadAssistant { get; set; }
        public string ConcernedEngineer { get; set; }
        public string SectionICT { get; set; }
        public string BranchClerk { get; set; }
    }
}
