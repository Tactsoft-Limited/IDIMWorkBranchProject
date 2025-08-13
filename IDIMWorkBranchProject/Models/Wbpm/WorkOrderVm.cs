using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.Wbpm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    [Validator(typeof(WorkOrderVmValidator))]
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

        [DisplayName("কার্যাদেশের তারিখ বাংলা")]
        public string WorkOrderDateB { get; set; }

        [DisplayName("কাজ শুরুর তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DisplayName("কাজ শেষের তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [DisplayName("কাজ হস্তান্তরের তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? HandOverDate { get; set; }

        [DisplayName("কার্যাদেশ ডকুমেন্ট")]
        public string ScanDocument { get; set; }

        [DisplayName("কার্যাদেশ ডকুমেন্ট")]
        public HttpPostedFileBase DocumentFile { get; set; }

        [DisplayName("প্রধান সহকারী")]
        public int? HeadAssistantId { get; set; }

        [DisplayName("প্রধান সহকারী")]
        public string HeadAssistantName { get; set; }

        [DisplayName("ইঞ্জিনিয়ার আইসি")]
        public int? ConcernedEngineerId { get; set; }

        [DisplayName("ইঞ্জিনিয়ার আইসি")]
        public string ConcernedEngineerName { get; set; }

        [DisplayName("সেকশন আইসি")]
        public int? SectionICId { get; set; }

        [DisplayName("সেকশন আইসি")]
        public string SectionICTName { get; set; }

        [DisplayName("শাখা করনিক")]
        public int? BranchClerkId { get; set; }

        [DisplayName("শাখা করনিক")]
        public string BranchClerkName { get; set; }

        [DisplayName("অফিসার")]
        public int? OfficerId { get; set; }

        [DisplayName("অফিসার")]
        public string officerName { get; set; }


        public IEnumerable<SelectListItem> HeadAssistantDropdown { get; set; }
        public IEnumerable<SelectListItem> ConcernedEngineerDropdown { get; set; }
        public IEnumerable<SelectListItem> SectionICTDropdown { get; set; }
        public IEnumerable<SelectListItem> BranchClerkDropdown { get; set; }
        public IEnumerable<SelectListItem> OfficersDropdown { get; set; }

    }
}