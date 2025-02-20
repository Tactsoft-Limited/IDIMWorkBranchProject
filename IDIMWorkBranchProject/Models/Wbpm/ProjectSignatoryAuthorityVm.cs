using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class ProjectSignatoryAuthorityVm
    {
        public ProjectSignatoryAuthorityVm()
        {
            HeadAssistantDropdown = new List<SelectListItem>();
            ConcernedEngineerDropdown = new List<SelectListItem>();
            SectionICTDropdown = new List<SelectListItem>();
            BranchClerkDropdown = new List<SelectListItem>();
        }
        public int ProjectSignatoryAuthorityId { get; set; }
        [Display(Name = "প্রকল্পের নাম")]
        public int? ADPProjectId { get; set; }
        [Display(Name = "প্রকল্পের নাম")]
        public string ADPProjectTitle { get; set; }

        [Display(Name = "প্রধান সহকারী")]
        public int? HeadAssistant { get; set; }
        [Display(Name = "প্রধান সহকারী নাম")]
        public string HeadAssistantName { get; set; }

        [Display(Name = "সংশ্লিষ্ট প্রকৌশলী")]
        public int? ConcernedEngineer { get; set; }
        [Display(Name = "সংশ্লিষ্ট প্রকৌশলী নাম")]
        public string ConcernedEngineerName { get; set; }

        [Display(Name = "সেকশনআইসি")]
        public int? SectionICT { get; set; }
        [Display(Name = "সেকশনআইসি নাম")]
        public string SectionICName { get; set; }

        [Display(Name = "কেরানি শাখা")]
        public int? BranchClerk { get; set; }
        [Display(Name = "কেরানি শাখা নাম")]
        public string BranchClerkName { get; set; }




        public IEnumerable<SelectListItem> HeadAssistantDropdown { get; set; }
        public IEnumerable<SelectListItem> ConcernedEngineerDropdown { get; set; }
        public IEnumerable<SelectListItem> SectionICTDropdown { get; set; }
        public IEnumerable<SelectListItem> BranchClerkDropdown { get; set; }
    }
}