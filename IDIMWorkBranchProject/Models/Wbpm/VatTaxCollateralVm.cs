using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class VatTaxCollateralVm
    {
        public int VatTaxCollateralId { get; set; }
        public int ProjectWorkId { get; set; }

        [Display(Name = "এডিপি রিসিভ পেমেন্ট")]
        public int ADPReceivePaymentId { get; set; }

        [DisplayName("নির্মাণ কাজের নাম")]
        public string ProjectWorkTitle { get; set; }

        [DisplayName("বিল পরিশোধের পরিমাণ")]
        public decimal BillPaidAmount { get; set; }

        [DisplayName("ট্যাক্স (%)")]
        public double TaxPer { get; set; }

        [DisplayName("ট্যাক্স পরিমাণ")]
        public decimal TaxAmount { get; set; }

        [DisplayName("ভ্যাট (%)")]
        public double VatPer { get; set; }

        [DisplayName("ভ্যাট পরিমাণ")]
        public decimal VatAmount { get; set; }

        [DisplayName("জামানত (%)")]
        public double CollateralPer { get; set; }

        [DisplayName("জামানত পরিমাণ")]
        public decimal CollateralAmount { get; set; }

        [DisplayName("মোট কর্তনের পরিমাণ")]
        public decimal TotalDeductionAmount { get; set; }

        [DisplayName("নীট পরিমান")]
        public decimal NeetAmount { get; set; }

        [DisplayName("নীট পরিমান কথায়")]
        public string NeetAmountInWord { get; set; }

        [DisplayName("বিবিধ ফান্ডে জমা")]
        public decimal DepositInBGBFund { get; set; }

        [Display(Name = "বিল নং")]
        public int BillSubmissionNo { get; set; }

        [Display(Name = "বিল তারিখ")]
        [DataType(DataType.Date)]
        public DateTime BillSubmissionDate { get; set; }

        [Display(Name = "শেষ বিলের টাকার অংক")]        
        public decimal LastBillAmount { get; set; }

        [Display(Name = "এ যাবত যে অংকের বরাদ্দ ")]
        public decimal AllocatedAmountTillNow { get; set; }

        [Display(Name = "পত্র নং")]
        public string AllocatedAmountLetterNo { get; set; }

        [Display(Name = "এ যাবত যে অংকের বরাদ্দ কমানো হইয়াছে")]
        public decimal ReducedAllocatedAmountTillNow { get; set; }

        [Display(Name = "পত্র নং")]
        public string ReducedAllocatedAmountLetterNo { get; set; }

        [Display(Name = "নীট মোট")]
        public decimal NetTotalAmount { get; set; }

        [Display(Name = "গত বিলের মোট জের")]
        public decimal LastBillTotalBalance { get; set; }

        [Display(Name = "এই বিলের মোট")]
        public decimal CurrentBillTotalBalance { get; set; }

        [Display(Name = "সংযক্ত-পূর্তকর্মের বিলের টাকা")]
        public decimal RelatedWorkBillAmount { get; set; }

        [Display(Name = "মোট (পরবর্তী বিলে জের টানিয়া নেওয়া হইবে)")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "কোড নম্বর")]
        public string CodeNo { get; set; }

        [Display(Name = "অর্থনৈতিক কোড")]
        public string EconomicCode { get; set; }

        [Display(Name = "ভাউচার নম্বর")]
        public string VoucherNo { get; set; }

    }
}