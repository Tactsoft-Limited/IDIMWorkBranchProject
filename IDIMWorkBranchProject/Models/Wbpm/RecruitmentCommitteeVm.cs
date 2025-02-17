using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class RecruitmentCommitteeVm
    {
        public int RecruitmentCommitteeId { get; set; }

        [Display(Name = "নাম")]
        public string Name { get; set; }

        [Display(Name = " বাংলা নাম")]
        public string NameB { get; set; }

        [Display(Name = "পদবি")]
        public string Designation { get; set; }

        [Display(Name = "পদবি বাংলায়")]
        public string DesignationB { get; set; }

        [Display(Name = "শিরোনাম")]
        public string Title { get; set; }

        [Display(Name = "শিরোনাম বাংলায়")]
        public string TitleB { get; set; }

        [Display(Name = "ঠিকানা")]
        public string Address { get; set; }

        [Display(Name = "ঠিকানা বাংলায়")]
        public string AddressB { get; set; }
       
    }
}