using System;
using System.ComponentModel;
using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.Wbpm;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    [Validator(typeof(ContractorCompanyPaymentVmValidator))]
    public class ContractorCompanyPaymentVm
    {

        public int ContractorCompanyPaymentId { get; set; }

        [DisplayName("প্রকল্প কাজ আইডি")]
        public int ProjectWorkId { get; set; }

        [DisplayName("নির্মাণ কাজের নাম")]
        public string ProjectWorkTitle { get; set; }

        [DisplayName("লেটার নং")]
        public string LetterNo { get; set; }

        [DisplayName("প্রাক্কলিত ব্যয়")]
        public decimal? EstimatedCost { get; set; }

        [DisplayName("ট্যাক্স (%)")]
        public double EstimatedCostTaxPer { get; set; }

        [DisplayName("ট্যাক্স পরিমান")]
        public decimal EstimatedCostTaxAmount { get; set; }

        [DisplayName("ভ্যাট (%)")]
        public double EstimatedCostVatPer { get; set; }

        [DisplayName("ভ্যাট পরিমান")]
        public decimal EstimatedCostVatAmount { get; set; }

        [DisplayName("জামানত (%)")]
        public double EstimatedCostCollateralPer { get; set; }

        [DisplayName("জামানত পরিমান")]
        public decimal EstimatedCostCollateralAmount { get; set; }

        [DisplayName("মোট কর্তনের পরিমান")]
        public decimal EstimatedCostDeductionAmount { get; set; }

        [DisplayName("নীট টাকার পরিমান")]
        public decimal NetEstimatedCostAmount { get; set; }

        [DisplayName("অগ্রগতি (%)")]
        public double ProgressPer { get; set; }

        [DisplayName("অগ্রগতি পরিমান")]
        public decimal ProgressAmount { get; set; }

        [DisplayName("ট্যাক্স (%)")]
        public double ProgressTaxPer { get; set; }

        [DisplayName("ট্যাক্স পরিমান")]
        public double ProgressTaxAmount { get; set; }

        [DisplayName("ভ্যাট (%)")]
        public double ProgressVatPer { get; set; }

        [DisplayName("ভ্যাট পরিমান")]
        public double ProgressVatAmount { get; set; }

        [DisplayName("জামানত (%)")]
        public double ProgressCollateralPer { get; set; }

        [DisplayName("জামানত পরিমান")]
        public double ProgressCollateralAmount { get; set; }

        [DisplayName("মোট কর্তনের পরিমান")]
        public decimal ProgressDeductionAmount { get; set; }

        [DisplayName("নীট অগ্রগতি পরিমান")]
        public decimal NetProgressAmount { get; set; }

        [DisplayName("পারফরম্যান্স সিকিউরিটি (%)")]
        public double PerformanceSecurityPer { get; set; }

        [DisplayName("পারফরম্যান্স সিকিউরিটি পরিমান")]
        public decimal PerformanceSecurityAmount { get; set; }

        [DisplayName("অগ্রগতি (%) অনুসারে ঠিকাদারের প্রাপ্য")]
        public double ContactorProgressPer { get; set; }

        [DisplayName("অগ্রগতি অনুসারে ঠিকাদারের প্রাপ্য পরিমান")]
        public decimal ContactorProgressAmount { get; set; }

        [DisplayName("বিল পেমেন্ট নম্বর")]
        public int BillPaymentNumber { get; set; }

        public int PreviouslyTotalPaidNo { get; set; }

        [DisplayName("ইতিপূর্বে ঠিকাদারকে নীট পরিশোধ")]
        public decimal PreviouslyPaidAmount { get; set; }

        [DisplayName("চলতি বিলে পরিশোধযোগ্য অর্থ")]
        public decimal PayableAmountOnCurrentBill { get; set; }

        [DisplayName("নীট পরিশোধযোগ্য অর্থ")]
        public decimal FinalPaymentAmount { get; set; }

        [DisplayName("নীট পরিশোধযোগ্য অর্থ কথায়")]
        public string FinalPaymentAmountInWord { get; set; }

        [DisplayName("বিবিধ ফান্ডে অবশিষ্ট জমা থাকবে")]
        public decimal WillBeDepositedInFund { get; set; }

        [DisplayName("মন্তব্য")]
        public string Remarks { get; set; }

        [DisplayName("সংশ্লিষ্ট ফার্মের নামে বিজিবি বিবিধ ফান্ডে জমা")]
        public decimal TotalDepositsInFund { get; set; }

        public decimal TotalWithdrawFromMinistry { get; set; }

        public double TotalWithdrawPercent { get; set; }

        public DateTime WorkStarted { get; set; }

        public DateTime WorkEnded { get; set; }

        public string ConstructionCompany { get; set; }
    }
}