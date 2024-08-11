using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.Setup;

namespace IDIMWorkBranchProject.Models.Setup
{
    [Validator(typeof(QuarterVmValidator))]
    public class QuarterVm
    {
        public QuarterVm()
        {
            MonthFromDropdown = new List<SelectListItem>();
            MonthToDropdown = new List<SelectListItem>();
            YearFromDropdown = new List<SelectListItem>();
        }

        [Display(Name = "Id")]
        public int QuarterId { get; set; }

        [Display(Name = "Quarter")]
        public string QuarterName { get; set; }

        [Display(Name = "Year From")]
        public int YearFrom { get; set; }

        [Display(Name = "Year To")]
        public int? YearTo { get; set; }

        [Display(Name = "Month From")]
        public int MonthFrom { get; set; }

        [Display(Name = "Month To")]
        public int MonthTo { get; set; }

        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }

        public IEnumerable<SelectListItem> MonthFromDropdown { get; set; }
        public IEnumerable<SelectListItem> MonthToDropdown { get; set; }

        public IEnumerable<SelectListItem> YearFromDropdown { get; set; }

    }
}