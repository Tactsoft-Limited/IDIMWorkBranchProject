using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class ProjectSignatoryAuthorityVm
    {
        public int ProjectSignatoryAuthorityId { get; set; }

        [DisplayName("প্রকল্পের নাম")]
        public int? ADPProjectId { get; set; }

        [DisplayName("প্রকল্পের নাম")]
        public string ADPProjectTitle { get; set; }

        [DisplayName("প্রধান সহকারী")]
        public int? HeadAssistant { get; set; }

        [DisplayName("প্রধান সহকারী")]
        public string HeadAssistantName { get; set; }

        [DisplayName("ইঞ্জিনিয়ার আইসি")]
        public int? ConcernedEngineer { get; set; }

        [DisplayName("ইঞ্জিনিয়ার আইসি")]
        public string ConcernedEngineerName { get; set; }

        [DisplayName("সেকশন আইসি")]
        public int? SectionICT { get; set; }

        [DisplayName("সেকশন আইসি")]
        public string SectionICTName { get; set; }

        [DisplayName("শাখা করনিক")]
        public int? BranchClerk { get; set; }

        [DisplayName("শাখা করনিক")]
        public string BranchClerkName { get; set; }


        public IEnumerable<SelectListItem> HeadAssistantDropdown { get; set; }
        public IEnumerable<SelectListItem> ConcernedEngineerDropdown { get; set; }
        public IEnumerable<SelectListItem> SectionICTDropdown { get; set; }
        public IEnumerable<SelectListItem> BranchClerkDropdown { get; set; }

    }
}