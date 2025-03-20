using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.Wbpm;
using System.ComponentModel.DataAnnotations;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    [Validator(typeof(SignatoryAuthorityVmValidator))]
    public class SignatoryAuthorityVm
    {
        public int SignatoryAuthorityId { get; set; }

        [Display(Name = "নাম")]
        public string AuthorityName { get; set; }

        [Display(Name = "নাম বাংলা")]
        public string AuthorityNameB { get; set; }

        [Display(Name = "পদবী")]
        public string Designation { get; set; }

        [Display(Name = "পদবী বাংলা")]
        public string DesignationB { get; set; }



       
       
       
       
    }
}