using System.ComponentModel;
using System;
using System.Web;
using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.Wbpm;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    [Validator(typeof(FormalMeetingVmValidator))]
    public class FormalMeetingVm
    {

        public int FormalMeetingId { get; set; }

        [DisplayName("প্রকল্প আইডি")]
        public int ADPProjectId { get; set; }

        [DisplayName("প্রকল্পের শিরোনাম")]
        public string ProjectTitle { get; set; }

        [DisplayName("মিটিং শিরোনাম")]
        public string MeetingTitle { get; set; }

        [DisplayName("মিটিং সংখ্যা (English)")]
        public int NumberOfMeeting { get; set; }

        [DisplayName("মিটিং তারিখ")]
        public DateTime MeetingDate { get; set; }

        [DisplayName("মিটিং ডকুমেন্ট")]
        public string MeetingDocument { get; set; }

        [DisplayName("আপলোড ডকুমেন্ট")]
        public HttpPostedFileBase MeetingDocumentFile { get; set; }
    }
}