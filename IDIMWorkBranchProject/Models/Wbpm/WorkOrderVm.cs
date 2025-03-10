using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel;
using System.Web;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class WorkOrderVm
    {
        public int WorkOrderId { get; set; }

        [DisplayName("নির্মাণ কাজ")]
        public int ProjectWorkId { get; set; }

        [DisplayName("নির্মাণ কাজের নাম (বাংলা)")]
        public string ProjectWorkTitleB { get; set; }

        [DisplayName("লেটার নং")]
        public string LetterNo { get; set; }

        [DisplayName("কার্যাদেশের তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? WorkOrderDate { get; set; }

        [DisplayName("কাজ শুরুর তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DisplayName("কাজ শেষের তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [DisplayName("কার্যাদেশ ডকুমেন্ট")]
        public string ScanDocument { get; set; }

        [DisplayName("কার্যাদেশ ডকুমেন্ট")]
        public HttpPostedFileBase DocumentFile { get; set; }

    }
}