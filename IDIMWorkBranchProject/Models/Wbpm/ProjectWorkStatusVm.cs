using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.Wbpm;
using System.ComponentModel;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    [Validator(typeof(ProjectWorkStatusVmValidator))]
    public class ProjectWorkStatusVm
    {
        public int ProjectWorkStatusId { get; set; }

        [DisplayName("নির্মাণ কাজ")]
        public int ProjectWorkId { get; set; }

        [DisplayName("নির্মাণ কাজের নাম (বাংলা)")]
        public string ProjectWorkTitleB { get; set; }

        [DisplayName("প্রকল্পের অবস্থা")]
        public StatusType? StatusTypeId { get; set; }

        public string WorkStatus { get; set; }
    }

    public enum StatusType
    {
        InProcess = 1,
        Ongoing = 2,
        Completed = 3,
        Suspended = 4
    }
}

