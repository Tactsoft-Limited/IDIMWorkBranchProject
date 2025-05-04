using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class RevenuePerformanceSecurityVm
    {
        public int RevenuePerformanceSecurityId { get; set; }

        [DisplayName("নির্মাণ কাজ")]
        public int ProjectWorkId { get; set; }

        [DisplayName("নির্মাণ কাজের নাম (বাংলা)")]
        public string ProjectWorkTitleB { get; set; }

        [DisplayName("জমার তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RevenuePerformanceSecuritySubmissionDate { get; set; }

        [DisplayName("ব্যাংক গ্যারান্টির শেষ তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RevenuePerformanceSecurityExpiryDate { get; set; }

        [DisplayName("ব্যাংক গ্যারান্টি ডকুমেন্ট")]
        public string RevenuePerformanceSecurityScanDocument { get; set; }

        [DisplayName("ব্যাংক গ্যারান্টি ডকুমেন্ট")]
        public HttpPostedFileBase DocumentFile { get; set; }
    }
}