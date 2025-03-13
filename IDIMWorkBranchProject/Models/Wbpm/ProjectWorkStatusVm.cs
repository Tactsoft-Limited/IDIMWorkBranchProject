using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class ProjectWorkStatusVm
    {
        public int ProjectWorkStatusId { get; set; }

        [DisplayName("নির্মাণ কাজ")]
        public int ProjectWorkId { get; set; }

        [DisplayName("নির্মাণ কাজের নাম (বাংলা)")]
        public string ProjectWorkTitleB { get; set; }

        [DisplayName("প্রকল্পের অবস্থা")]
        public StatusType? StatusTypeId { get; set; }
    }

    public enum StatusType
    {
        Ongoing = 1,
        Completed = 2,
        Suspended = 3
    }
}

