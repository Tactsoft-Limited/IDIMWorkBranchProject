using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class ConsultancyActivitiesVm
    {
        public ConsultancyActivitiesVm()
        {
            SubProjectDropdown = new List<SelectListItem>();
            ConsultantDropdown = new List<SelectListItem>();
        }
        [Display(Name = "Id")]
        public int CAId { get; set; }
        public int SubProjectDetailsId { get; set; }
        [Display(Name = "Sub Project")]
        public int SubProjectId { get; set; }
        [Display(Name = "Sub Project")]
        public string SubProjectTitle { get; set; }
        [Display(Name = "Consultant")]
        public int ConsultantId { get; set; }
        [Display(Name = "Consultant")]
        public string ConsultantName { get; set; }

        public int Progress { get; set; }

        [Display(Name = "Consultant Progress for Supervission")]
        public int ConsultantProgressForSupervission { get; set; }
        [Display(Name = "Progress According to Contract")]
        public int ProgressAccordingToContract { get; set; }
        [Display(Name = "Agreed Money")]
        public float AgreedMoney { get; set; }
        [Display(Name = "Paid Money")]
        public float PaidMoney { get; set; }
        [Display(Name = "Payable for Progress")]
        public float PayableForProgress { get; set; }
        [Display(Name = "Rest of Payable For Progress")]
        public float RestOfPayableForProgress { get; set; }
        [Display(Name = "Remarks")]
        public string Remark { get; set; }

        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }
        public IEnumerable<SelectListItem> SubProjectDropdown { get; set; }
        public IEnumerable<SelectListItem> ConsultantDropdown { get; set; }
    }
}