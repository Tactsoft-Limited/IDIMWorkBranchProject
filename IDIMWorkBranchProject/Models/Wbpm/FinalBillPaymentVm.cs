using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class FinalBillPaymentVm 
    { 
        public FinalBillPaymentVm()
        {
            BGBFundDropdown = new List<SelectListItem>();
        }
        public int FinalBillPaymentId { get; set; }

        [DisplayName("নির্মাণ কাজের নাম ")]
        public int ProjectWorkId { get; set; }

        [DisplayName("নির্মাণ কাজের নাম ")]
        public string ProjectWorkName { get; set; }

        [Display(Name = "চূড়ান্ত পরিমাপ অনুযায়ী মোট পরিমাণ")]
        public decimal NetAmountAsPerFinalMeasurement { get; set; }
        [Display(Name = "ভ্যাট ও আয়কর (%)")]
        public double VatTaxPer { get; set; }
        [Display(Name = " ভ্যাট ট্যাক্স পরিমান ")]
        public decimal VatTaxAmount { get; set; }
        [Display(Name = "আয়কর ও ভ্যাট কর্তনের পর ঠিকাদারের প্রাপ্য ")]
        public decimal ContractorDueAfterVatTaxDeduction { get; set; }
        [Display(Name = "পূর্বে পরিশোধিত চলতি বিলের সংখ্যা")]
        public int PreviouslyPaidBillNo { get; set; }
        [Display(Name = "পূর্বে পরিশোধিত চলতি বিলের পরিমাণ")]
        public decimal PreviouslyPaidAmount { get; set; }
        [Display(Name = "আসবাবপত্র বিল পরিশোধের পরিমান")]
        public decimal FurnitureBillPaymentAmount { get; set; }
        [Display(Name = "১০% জামানতে পরিশোধের পরিমান")]
        public decimal? CollateralPaidAmound { get; set; }
        [Display(Name = "ইতি পূর্বে মোট পরিশোধ")]
        public decimal AdvancePaymentAmount { get; set; }
        [Display(Name = "চুড়ান্ত বিলে পরিশোধযোগ্য অর্থ")]
        public decimal PayableFinalBill { get; set; }
        [Display(Name = "বিজিবি ফান্ডে নীট জমা")]
        public decimal DepositBGBFund { get; set; }
        [Display(Name = "চূড়ান্ত বিলে ঠিকাদারকে নীট পরিশোধ করা যেতে পারে")]
        public decimal NetFinalBill { get; set; }
        public decimal FinalBill { get; set; }
        [Display(Name = "বিজিবি ফান্ড")]
        public int? PaidFromBGBFundId { get; set; }
        [Display(Name = "চূড়ান্ত বিলে ঠিকাদারের বকেয়া প্রাপ্য")]
        public decimal? DuePaidAmount { get; set; }
        [Display(Name = "বিজিবি বিবিধ ফান্ডে অবশিষ্ঠ জমা থাকবে ")]
        public decimal? RemainingDepositsInBgbFund { get; set; }

        #region navigation property
        public IEnumerable<SelectListItem> BGBFundDropdown { get; set; }        
        #endregion
    }
}