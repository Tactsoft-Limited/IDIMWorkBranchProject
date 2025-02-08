using System.ComponentModel;
using System.Web;
using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.Wbpm;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    [Validator(typeof(MasterplanVmValidator))]
    public class MasterplanVm
    {


        public int MasterplanId { get; set; }

        [DisplayName("প্রকল্প আইডি")]
        public int ADPProjectId { get; set; }

        [DisplayName("প্রকল্পের শিরোনাম")]
        public string ProjectTitle { get; set; }

        [DisplayName("শিরোনাম")]
        public string Title { get; set; }

        [DisplayName("ডকুমেন্ট")]
        public string Document { get; set; }

        [DisplayName("আপলোড ডকুমেন্ট")]
        public HttpPostedFileBase DocumentFile { get; set; }

    }
}