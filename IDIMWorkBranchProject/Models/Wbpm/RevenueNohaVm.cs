using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class RevenueNohaVm
    {
        public int RevenueNohaId { get; set; }

        [DisplayName("নির্মাণ কাজ")]
        public int ProjectWorkId { get; set; }

        [DisplayName("নির্মাণ কাজের নাম (বাংলা)")]
        public string ProjectWorkTitleB { get; set; }

        [DisplayName("লেটার নং")]
        public string LetterNo { get; set; }

        [DisplayName(" রাজস্ব এনওএইচ তারিখ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? RevenueNohaDate { get; set; }

        [DisplayName("রাজস্ব এনওএইচএ ডকুমেন্ট")]
        public string ScanDocument { get; set; }

        [DisplayName("রাজস্ব এনওএইচএ ডকুমেন্ট")]
        public HttpPostedFileBase DocumentFile { get; set; }
    }
}