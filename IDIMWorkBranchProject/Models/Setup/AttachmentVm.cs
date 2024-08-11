using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.Setup;

namespace IDIMWorkBranchProject.Models.Setup
{
    [Validator(typeof(AttachmentVmValidator))]
    public class AttachmentVm
    {
        [Display(Name ="Id")]
        public int AttachmentId { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        //public bool IsAttached { get; set; }

        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }
    }
}