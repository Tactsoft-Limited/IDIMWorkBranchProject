

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class TenderEvaluationCommitteeVm 
    {
        public TenderEvaluationCommitteeVm()
        {
            AddDGDropdown=new List<SelectListItem>();
            DDGDropdown = new List<SelectListItem>();
            ProjectdirectorDropdown = new List<SelectListItem>();
            DirectorDropdown = new List<SelectListItem>();
        }
        public int TenderEvaluationCommitteeId { get; set; }
        [Display(Name = "প্রকল্পের নাম")]
        public int? ADPProjectId { get; set; }
        [Display(Name ="প্রকল্পের নাম")]
        public string ADPProjectTitle { get; set; }

        [Display(Name = "অতিরিক্ত মহাপরিচালক")]
        public int? AddDG { get; set; }
        [Display(Name = "অতিরিক্ত মহাপরিচালক")]
        public string AddDGName { get; set; }
        [Display(Name = "উপ-মহাপরিচালক")]
        public int? DDG { get; set; }
        [Display(Name = "উপ-মহাপরিচালক")]
        public string DDGName { get; set; }
        [Display(Name = "প্রকল্প পরিচালক")]
        public int? ProjectDirector { get; set; }
        [Display(Name = "প্রকল্প পরিচালক")]
        public string ProjectDirectorName { get; set; }
        [Display(Name = "পরিচালক")]
        public int? Director { get; set; }
        [Display(Name = "পরিচালক")]
        public string DirectorName { get; set; }


        public IEnumerable<SelectListItem> AddDGDropdown { get; set; }
        public IEnumerable<SelectListItem> DDGDropdown { get; set; }
        public IEnumerable<SelectListItem> ProjectdirectorDropdown { get; set; }
        public IEnumerable<SelectListItem> DirectorDropdown { get; set; }
    }
}