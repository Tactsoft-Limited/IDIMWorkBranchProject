using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class RevenueVm
    {
        public RevenueVm()
        {
            FiscalYearDropdown = new List<SelectListItem>();
        }

        public int RevenueId { get; set; }

        [DisplayName("অর্থবছর")]
        public int FisCalYearId { get; set; }

        [DisplayName("কার্যবিবরনী (English)")]
        public string WorkTitle { get; set; }

        [DisplayName("কার্যবিবরনী (বাংলা)")]
        public string WorkTitleB { get; set; }

        [DisplayName("প্রাক্কলিত ব্যয়")]
        public decimal EstimateCost { get; set; }

        [DisplayName("প্রাক্কলিত ব্যয় কথায় (English)")]
        public string EstimateCostInWord { get; set; }

        [DisplayName("প্রাক্কলিত ব্যয় কথায় (বাংলা)")]
        public string EstimateCostInWordB { get; set; }

        [DisplayName("মন্তব্য")]
        public string Remarks { get; set; }

        [DisplayName("আসবাব পত্র সংযুক্ত কিনা?")]
        public bool IsFurnitureIncluded { get; set; }

        [DisplayName("এন ও এ এইচ করা হয়েছে কিনা?")]
        public bool IsNoahCompleted { get; set; }

        [DisplayName("ব্যাংক জামানত/প্যারফরমেন্স সিকিউরিটি সংযুক্ত কিনা?")]
        public bool IsPerformanceSecuritySubmited { get; set; }

        [DisplayName("চুক্তি করা হয়েছে কিনা?")]
        public bool IsAgreementCompleted { get; set; }

        [DisplayName("কার্যাদেশ করা হয়েছে কিনা?")]
        public bool IsWorkOrderComplited { get; set; }

        #region navigation property
        public IEnumerable<SelectListItem> FiscalYearDropdown { get; set; }

        #endregion
    }
}