using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.SqlViews.Wbpm
{
    [Table("ViewFinalBillPayment", Schema = "wbpm")]
    public class ViewFinalBillPayment
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FinalBillPaymentId { get; set; }
        public int ProjectWorkId { get; set; }
        public double? VatPer { get; set; }
        public decimal? VatAmount { get; set; }
        public double? TaxPer { get; set; }
        public decimal? TaxAmount { get; set; }
        public double? CollateralPer { get; set; }
        public decimal? CollateralAmount { get; set; }
        public decimal? TotalAmountOfVatTaxCollateral { get; set; }
        public string TotalAmountOfVatTaxCollateralB { get; set; }
        public decimal? NetBillAfterVatTAxCollateralDeduction { get; set; }
        public decimal NetAmountAsPerFinalMeasurement { get; set; }
        public string LetterNo { get; set; }
        public double VatTaxPer { get; set; }
        public decimal VatTaxAmount { get; set; }
        public decimal ContractorDueAfterVatTaxDeduction { get; set; }
        public int PreviouslyPaidBillNo { get; set; }
        public decimal PreviouslyPaidAmount { get; set; }
        public decimal FurnitureBillPaymentAmount { get; set; }
        public decimal CollateralPaidAmound { get; set; }
        public decimal AdvancePaymentAmount { get; set; }
        public decimal PayableFinalBill { get; set; }
        public decimal DepositBGBFund { get; set; }
        public decimal NetFinalBill { get; set; }
        public string NetFinalBillWordB { get; set; }
        public decimal FinalBill { get; set; }
        public int PaidFromBGBFundId { get; set; }
        public decimal RemainingDepositsInBgbFund { get; set; }
        public string RemainingDepositsInBgbFundWordB { get; set; }
        public decimal DuePaidAmount { get; set; }
        public string ProjectWorkTitleB { get; set; }
        public decimal EstimatedCost { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FirmNameB { get; set; }
        public string HeadAssistant { get; set; }
        public string ConcernedEngineer { get; set; }
        public string SectionIC { get; set; }
        public string BranchClerk { get; set; }
    }
}
