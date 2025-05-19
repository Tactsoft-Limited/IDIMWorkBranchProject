using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class RevenueWorkOrderVm
    {
        public int RevenueWorkOrderId { get; set; }

        [DisplayName("রাজস্ব কার্যবিবরনী")]
        public int RevenueId { get; set; }

        [DisplayName("রাজস্ব কার্যবিবরনী (বাংলা)")]
        public string RevenueWorkTitleB { get; set; }

        [DisplayName("লেটার নং")]
        public string RevenueWorkOrderLetterNo { get; set; }

        [DisplayName(" রাজস্ব কার্যাদেশের তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? RevenueWorkOrderDate { get; set; }

        [DisplayName("রাজস্ব কার্যাদেশের কাজ শুরুর তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? RevenueWorkOrderStartDate { get; set; }

        [DisplayName("রাজস্ব কার্যাদেশের কাজ শেষের তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? RevenueWorkOrderEndDate { get; set; }

        [DisplayName("রাজস্ব কার্যাদেশের কাজ হস্তান্তরের তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? RevenueWorkOrderHandOverDate { get; set; }

        [DisplayName("রাজস্ব কার্যাদেশ ডকুমেন্ট")]
        public string RevenueWorkOrderScanDocument { get; set; }

        [DisplayName("রাজস্ব কার্যাদেশ ডকুমেন্ট")]
        public HttpPostedFileBase RevenueWorkOrderDocumentFile { get; set; }
    }
}