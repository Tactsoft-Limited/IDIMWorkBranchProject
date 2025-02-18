using System.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.Wbpm;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    [Validator(typeof(ProjectWorkVmValidator))]
    public class ProjectWorkVm
    {
        public ProjectWorkVm()
        {
            ConstructionFirmDropdown = new List<SelectListItem>();
        }

        public int ProjectWorkId { get; set; }

        [DisplayName("প্রকল্প আইডি")]
        public int ADPProjectId { get; set; }

        [DisplayName("প্রকল্পের শিরোনাম")]
        public string ProjectTitle { get; set; }

        [DisplayName("নির্মাণ কোম্পানি")]
        public int ConstructionCompanyId { get; set; }

        [Display(Name = "ফার্মের নাম")]
        public string FirmName { get; set; }

        [DisplayName("নির্মাণ কাজের নাম")]
        public string ProjectWorkTitle { get; set; }

        [DisplayName("প্রাক্কলিত ব্যয়")]
        public decimal? EstimatedCost { get; set; }

        [DisplayName("এনওএইচ তারিখ")]
		[DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
		public DateTime NOADate { get; set; }

        [DisplayName("চুক্তির তারিখ")]
		[DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
		public DateTime AgreementDate { get; set; }

        [DisplayName("কার্যাদেশের তারিখ")]
		[DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
		public DateTime WorkOrderDate { get; set; }

        [DisplayName("কাজ শুরুর তারিখ")]
		[DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
		public DateTime WorkStartDate { get; set; }

        [DisplayName("কাজ শেষের তারিখ")]
		[DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
		public DateTime WorkEndDate { get; set; }

        [DisplayName("সরেজমিনে শুরুর তারিখ")]
		[DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? OnFieldStartDate { get; set; }

        [DisplayName("সরেজমিনে শেষের তারিখ")]
		[DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? OnFieldEndDate { get; set; }

        [DisplayName("হস্তান্তর তারিখ")]
		[DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
		public DateTime HandOverDate { get; set; }

        [DisplayName("ব্যাংক গ্যারান্টির শেষ তারিখ")]
		[DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
		public DateTime BankGuaranteeEndDate { get; set; }

        [DisplayName("নির্মাণ অগ্রগতি")]
        public float ConstructionProgress { get; set; }

        [DisplayName("কাজের অবস্থা")]
        public string WorkStatus { get; set; }

        [DisplayName("মন্তব্য")]
        public string Remarks { get; set; }

        [DisplayName("ব্যাংক গ্যারান্টি ডকুমেন্ট")]
        public string BankGuaranteeDocument { get; set; }

        [DisplayName("এনওএইচ ডকুমেন্ট")]
        public string NOADocument { get; set; }

        [DisplayName("চুক্তি ডকুমেন্ট")]
        public string AgreementDocument { get; set; }

        [DisplayName("কার্যাদেশ ডকুমেন্ট")]
        public string WorkOrderDocument { get; set; }


        [DisplayName("ব্যাংক গ্যারান্টি ডকুমেন্ট")]
        public HttpPostedFileBase BankGuaranteeDocumentFile { get; set; }
        [DisplayName("এনওএইচ ডকুমেন্ট")]
        public HttpPostedFileBase NOADocumentFile { get; set; }
        [DisplayName("চুক্তি ডকুমেন্ট")]
        public HttpPostedFileBase AgreementDocumentFile { get; set; }
        [DisplayName("কার্যাদেশ ডকুমেন্ট")]
        public HttpPostedFileBase WorkOrderDocumentFile { get; set; }


        public IEnumerable<SelectListItem> ConstructionFirmDropdown { get; set; }
    }
}