using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.Setup;

namespace IDIMWorkBranchProject.Models.Setup
{
    [Validator(typeof(ConstructionFrimVmValidator))]
    public class ConstructionFirmVm
    {
        [Display(Name = "Id")]
        public int ConstructionFirmId { get; set; }

        [Display(Name = "Application")]
        public int ApplicationId { get; set; }

        [Display(Name = " Company Name")]
        public string ConstructionFirmName { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [Display(Name = "Contact Number")]
        public string ContactPhone { get; set; }

        [Display(Name = "Contact Email")]
        public string Email { get; set; }

        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }
    }
}