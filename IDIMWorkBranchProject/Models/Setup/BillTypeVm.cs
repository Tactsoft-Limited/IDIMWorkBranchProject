using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.Setup;

namespace IDIMWorkBranchProject.Models.Setup
{
    [Validator(typeof(BillTypeVmValidator))]
    public class BillTypeVm
    {
        [Display(Name ="Id")]
        public int BillTypeId { get; set; }

        [Display(Name = "Name")]
        public string BillTypeName { get; set; }

        [Display(Name = "Application")]
        public int ApplicationId { get; set; }

        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }
    }
}