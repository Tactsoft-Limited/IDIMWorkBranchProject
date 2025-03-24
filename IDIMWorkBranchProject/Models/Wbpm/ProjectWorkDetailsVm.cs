using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class ProjectWorkDetailsVm
    {
        public int ADPProjectId { get; set; }

        [DisplayName("প্রকল্পের শিরোনাম")]
        public string ProjectTitle { get; set; }

        [DisplayName("নির্মাণ কাজ")]
        public int ProjectWorkId { get; set; }

        [DisplayName("নির্মাণ কাজের নাম (বাংলা)")]
        public string ProjectWorkTitleB { get; set; }

        [DisplayName("নির্মাণ কাজের নাম (ইংরেজি)")]
        public string ProjectWorkTitle { get; set; }

        [DisplayName("প্রাক্কলিত ব্যয়")]
        public decimal? EstimatedCost { get; set; }
        public string EstimatedCostInWord { get; set; }
        public string EstimatedCostInWordBangla { get; set; }
        public bool IsNoahCompleted { get; set; }
        public bool IsPerformanceSecuritySubmited { get; set; }
        public bool IsAgreementCompleted { get; set; }
        public bool IsWorkOrderCompleted { get; set; }

        [DisplayName("লেটার নং")]
        public string NohaLetterNo { get; set; }

        [DisplayName("এনওএইচ তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NohaDate { get; set; }

        [DisplayName("এনওএইচ ডকুমেন্ট")]
        public string NohaDocument { get; set; }


        [DisplayName("জমার তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SubmissionDate { get; set; }

        [DisplayName("ব্যাংক গ্যারান্টির শেষ তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ExpiryDate { get; set; }

        [DisplayName("ব্যাংক গ্যারান্টি ডকুমেন্ট")]
        public string PerformanceSecurityDocument { get; set; }


        [DisplayName("ঠিকাদারি প্রতিষ্ঠান")]
        public string FirmNameB { get; set; }       

        [DisplayName("চুক্তির তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AgreementDate { get; set; }

        [DisplayName("চুক্তি ডকুমেন্ট")]
        public string AgreementDocument { get; set; }

        [DisplayName("লেটার নং")]
        public string WorkOrderLetterNo { get; set; }

        [DisplayName("কার্যাদেশের তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? WorkOrderDate { get; set; }

        [DisplayName("শুরুর তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DisplayName("শেষের তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [DisplayName("কার্যাদেশ ডকুমেন্ট")]
        public string WorkOrderDocument { get; set; }

        [DisplayName("নির্মাণ কাজের অবস্থা")]
        public int? ProjectWorkStatus { get; set; }

        [DisplayName("মন্তব্য")]
        public string Remarks { get; set; }



        public List<WorkOrderVm> WorkOrderList { get; set; }
        public List<ADPReceivePaymentVm> ADPReceivePayments { get; set; }
        public List<ContractorCompanyPaymentVm> ContractorCompanyPayments { get; set; }

    }
}