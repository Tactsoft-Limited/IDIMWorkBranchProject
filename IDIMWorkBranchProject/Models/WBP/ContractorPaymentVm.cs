using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class ContractorPaymentVm
    {
        public ContractorPaymentVm()
        {
            ConstructionFirmDropdown = new List<SelectListItem>();
        }

        [Key]
        public int ContractorPaymentId { get; set; }

        [DisplayName("উপ প্রকল্প")]
        public int SubProjectId { get; set; }

        [DisplayName("উপ প্রকল্প শিরোনাম")]
        public string SubProjectTitle { get; set; }

        [DisplayName("নির্মাণ প্রতিষ্ঠান")]
        [Required(ErrorMessage = "Construction Firm Id is required.")]
        public int ConstructionFirmId { get; set; }

        [DisplayName("নির্মাণ প্রতিষ্ঠানের নাম")]
        public string ConstructionFirmName { get; set; }

        [DisplayName("প্রাক্কলিত/অনুমোদিত অর্থ ")]
        [Required(ErrorMessage = "Approval Amount is required.")]
        public decimal ApprovalAmount { get; set; }

        [DisplayName("আয়কর (%)")]
        public double TaxPer { get; set; }

        [DisplayName("ভ্যাট (%)")]
        public double VatPer { get; set; }

        [DisplayName("নিরাপত্তা জামানত (%)")]
        public double CollateralPer { get; set; }

        [DisplayName("ভ্যাট, ট্যাক্স ও নিরাপত্তা জামানত")]
        [Required(ErrorMessage = "VatTax Security Amount is required.")]
        public decimal VatTaxSecurityAmount { get; set; }

        [DisplayName("নীট টাকার পরিমান")]
        [Required(ErrorMessage = "Net Amount is required.")]
        public decimal NetAmount { get; set; }

        [DisplayName("অগ্রগতি পরিমাণ (%)")]
        [Required(ErrorMessage = "Progress Percentage is required.")]
        public double ProgressPer { get; set; }

        [DisplayName("উত্তোলিত টাকার পরিমাণ")]
        [Required(ErrorMessage = "Progress Percentage Amount is required.")]
        public decimal ProgressAmount { get; set; }

        [DisplayName("অগ্রগতি উপর আয়কর (%)")]
        public double ProgressTaxPer { get; set; }

        [DisplayName("অগ্রগতি উপর ভ্যাট (%)")]
        public double ProgressVatPer { get; set; }

        [DisplayName("অগ্রগতি উপর নিরাপত্তা জামানত (%)")]
        public double ProgressCollateralPer { get; set; }

        [DisplayName("অগ্রগতি উপর কর্তনকৃত টাকার পরিমান")]
        [Required(ErrorMessage = "Deduction Amount is required.")]
        public decimal DeductionAmount { get; set; }

        [DisplayName("অগ্রগতি উপর কর্তনকৃত টাকার নীট পরিমান")]
        [Required(ErrorMessage = "Net Deduction Amount is required.")]
        public decimal NetDeductionAmount { get; set; }

        [DisplayName("পারফর্মেন্স সিকিউরিটি (%)")]
        [Required(ErrorMessage = "Performance Security Percentage is required.")]
        public double PerformanceSecurityPer { get; set; }

        [DisplayName("পারফর্মেন্স সিকিউরিটি পরিমাণ")]
        [Required(ErrorMessage = "Performance Security Amount is required.")]
        public decimal PerformanceSecurityAmount { get; set; }

        [DisplayName("অদ্যাবধি অগ্রগতি (%)")]
        [Required(ErrorMessage = "Contractor Progress Percentage is required.")]
        public double ContactorProgressPer { get; set; }

        [DisplayName("অগ্রগতি অনুযায়ী ঠিকাদারের প্রাপ্য পরিমাণ")]
        [Required(ErrorMessage = "Contractor Progress Amount is required.")]
        public decimal ContactorProgressAmount { get; set; }

        [DisplayName("বিল পেমেন্ট নম্বর")]
        [Required(ErrorMessage = "Bill Payment Number is required.")]
        public int BillPaymentNumber { get; set; }

        [DisplayName("চলতি বিলে পরিশোধযোগ্য অর্থ")]
        [Required(ErrorMessage = "Running Bill Payment is required.")]
        public decimal RunningBillPayment { get; set; }

        [DisplayName("ইতিপূর্বে ঠিকাদারকে পরিশোধকৃত অর্থ")]
        [Required(ErrorMessage = "Contractor Deposit Amount is required.")]
        public decimal ContactorDepositAmount { get; set; }

        [DisplayName("অগ্রগতি অনুযায়ী ঠিকাদারকে পরিশোধযোগ্য অর্থ ")]
        [Required(ErrorMessage = "Contractor Final Payment Amount is required.")]
        public decimal ContactorFinalPaymentAmount { get; set; }

        [DisplayName("বিজিবি বিবিধ ফান্ডে জমা আছে")]
        [Required(ErrorMessage = "BGB Fund Deposit Amount is required.")]
        public decimal BGBFundDepositAmount { get; set; }

        [DisplayName("মন্তব্য")]
        [StringLength(150, ErrorMessage = "Remarks cannot exceed 150 characters.")]
        public string Remarks { get; set; }



        public int TotalBillPayment { get; set; }
        public decimal TotalBillPaymentAmount { get; set; }

        public IEnumerable<SelectListItem> ConstructionFirmDropdown { get; set; }

    }
}