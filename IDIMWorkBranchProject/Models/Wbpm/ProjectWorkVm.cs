using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.Wbpm;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    [Validator(typeof(ProjectWorkVmValidator))]
    public class ProjectWorkVm
    {


        public int ProjectWorkId { get; set; }

        [DisplayName("প্রকল্প আইডি")]
        public int ADPProjectId { get; set; }

        [DisplayName("প্রকল্পের শিরোনাম")]
        public string ProjectTitle { get; set; }

        [DisplayName("নির্মাণ কাজের নাম (ইংরেজি)")]
        public string ProjectWorkTitle { get; set; }

        [DisplayName("নির্মাণ কাজের নাম (বাংলা)")]
        public string ProjectWorkTitleB { get; set; }

        [DisplayName("ঠিকাদারি প্রতিষ্ঠান")]
        public string FirmNameB { get; set; }

        [DisplayName("প্রাক্কলিত ব্যয়")]
        public decimal? EstimatedCost { get; set; }

        [DisplayName("প্রাক্কলিত ব্যয় কথায়")]
        public string EstimatedCostInWord { get; set; }

        [DisplayName("প্রাক্কলিত ব্যয় কথায়")]
        public string EstimatedCostInWordBangla { get; set; }

        [DisplayName("আসবাবপত্র কি অন্তর্ভুক্ত?")]
        public bool IsFurnitureIncluded { get; set; }

        [DisplayName("মন্তব্য")]
        public string Remarks { get; set; }        

        public DateTime? AgreementDate { get; set; }
        public DateTime? WorkStartDate { get; set; }
        public DateTime? WorkEndDate { get; set; }
        public DateTime? BankGuaranteeEndDate { get; set; }
        public DateTime? HandOverDate { get; set; }
        public string WorkStatus { get; set; }

    }
}