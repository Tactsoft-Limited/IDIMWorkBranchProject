using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class VatTaxCollateralVm
    {
        public int VatTaxCollateralId { get; set; }

        [Display(Name = "এডিপি রিসিভ পেমেন্ট")]
        public int ADPReceivePaymentId { get; set; }

        [Display(Name = "বিল জমা নম্বর")]
        public int BillSubmissionNo { get; set; }

        [Display(Name = "বিল জমার তারিখ")]
        [DataType(DataType.Date)]
        public DateTime BillSubmissionDate { get; set; }

        [Display(Name = "সর্বশেষ বিলের পরিমাণ")]
        [Range(0, double.MaxValue)]
        public decimal LastBillAmount { get; set; }

        [Display(Name = "এ পর্যন্ত বরাদ্দকৃত পরিমাণ")]
        public decimal AllocatedAmountTillNow { get; set; }

        [Display(Name = "বরাদ্দকৃত পরিমাণের চিঠি নম্বর")]
        public string AllocatedAmountLetterNo { get; set; }

        [Display(Name = "এ পর্যন্ত হ্রাসকৃত বরাদ্দের পরিমাণ")]
        public decimal ReducedAllocatedAmountTillNow { get; set; }

        [Display(Name = "হ্রাসকৃত বরাদ্দের চিঠি নম্বর")]
        public string ReducedAllocatedAmountLetterNo { get; set; }

        [Display(Name = "নেট মোট পরিমাণ")]
        public decimal NetTotalAmount { get; set; }

        [Display(Name = "সর্বশেষ বিলের মোট ব্যালেন্স")]
        public decimal LastBillTotalBalance { get; set; }

        [Display(Name = "বর্তমান বিলের মোট ব্যালেন্স")]
        public decimal CurrentBillTotalBalance { get; set; }

        [Display(Name = "সম্পর্কিত কাজের বিলের পরিমাণ")]
        public decimal RelatedWorkBillAmount { get; set; }

        [Display(Name = "মোট পরিমাণ")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "কোড নম্বর")]
        public string CodeNo { get; set; }

        [Display(Name = "অর্থনৈতিক কোড")]
        public string EconomicCode { get; set; }

        [Display(Name = "ভাউচার নম্বর")]
        public string VoucherNo { get; set; }

        // Dropdown List
        [Display(Name = "এডিপি রিসিভ পেমেন্ট তালিকা")]
        public List<SelectListItem> ADPReceivePaymentList { get; set; }
    }
}