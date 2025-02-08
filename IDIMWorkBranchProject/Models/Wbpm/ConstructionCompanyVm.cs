using System.ComponentModel.DataAnnotations;
using IDIMWorkBranchProject.Models.Validation.Wbpm;
using FluentValidation.Attributes;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    [Validator(typeof(ConstructionCompanyVmValidator))]
    public class ConstructionCompanyVm
    {

        public int ConstructionCompanyId { get; set; }

        [Display(Name = "ফার্মের নাম")]
        public string FirmName { get; set; }

        [Display(Name = "ফার্মের নাম (বাংলা)")]
        public string FirmNameB { get; set; }

        [Display(Name = "যোগাযোগ ব্যক্তির নাম")]
        public string ContactPerson { get; set; }

        [Display(Name = "যোগাযোগ ব্যক্তির নাম (বাংলা)")]
        public string ContactPersonB { get; set; }

        [Display(Name = "ফোন নম্বর")]
        public string ContactPhone { get; set; }

        [Display(Name = "ইমেইল")]
        public string Email { get; set; }

        [Display(Name = "ফার্মের ঠিকানা")]
        public string FirmAddress { get; set; }

        [Display(Name = "ফার্মের ঠিকানা (বাংলা)")]
        public string FirmAddressB { get; set; }
    }
}