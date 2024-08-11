using System.ComponentModel;
using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.User;

namespace IDIMWorkBranchProject.Models.User
{
    [Validator(typeof(EditInformationVmValidator))]
    public class EditInformationVm
    {
        [DisplayName("Regiment Number")]
        public string RegimentNumber { get; set; }

        [DisplayName("RDO Number")]
        public string RdoNumber { get; set; }

        [DisplayName("JCO Number")]
        public string JcoNumber { get; set; }
    }
}