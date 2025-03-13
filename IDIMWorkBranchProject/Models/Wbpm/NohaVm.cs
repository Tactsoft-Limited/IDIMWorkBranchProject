using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class NohaVm
    {
        public int NohaId { get; set; }

        [DisplayName("নির্মাণ কাজ")]
        public int ProjectWorkId { get; set; }

        [DisplayName("নির্মাণ কাজের নাম (বাংলা)")]
        public string ProjectWorkTitleB { get; set; }

        [DisplayName("লেটার নং")]
        public string LetterNo { get; set; }

        [DisplayName("এনওএইচ তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NohaDate { get; set; }

        [DisplayName("এনওএইচএ ডকুমেন্ট")]
        public string ScanDocument { get; set; }

        [DisplayName("এনওএইচএ ডকুমেন্ট")]
        public HttpPostedFileBase DocumentFile { get; set; }

    }
}