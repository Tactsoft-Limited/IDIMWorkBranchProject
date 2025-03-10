using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace IDIMWorkBranchProject.Models.Wbpm
{

    public class ContractAgreementVm
    {

        [Key]
        public int ContractAgreementId { get; set; }

        [Display(Name = "নির্মান কাজ")]
        public int ProjectWorkId { get; set; }

        [DisplayName("নির্মান কাজের শিরোনাম")]
        public string ProjectWorkTitle { get; set; }

        [Display(Name = "ঠিকাদার প্রতিষ্ঠান")]
        public int ConstructionCompanyId { get; set; }

        [Display(Name = "ঠিকাদার প্রতিষ্ঠান")]
        public string ConstructionFirm { get; set; }

        [Display(Name = "চুক্তির তারিখ")]
        public DateTime? AgreementDate { get; set; }

        public int? AddDGId { get; set; }
        public string AddDGName { get; set; }
        public int? DDGId { get; set; }

        public string DDGName { get; set; }

        public int? ProjectDirectorId { get; set; }

        public string ProjectDirectorName { get; set; }

        public int? DirectorId { get; set; }

        public string DirectorName { get; set; }


        [DisplayName("চুক্তি ডকুমেন্ট")]
        public string ScanDocument { get; set; }

        [DisplayName("চুক্তি ডকুমেন্ট")]
        public HttpPostedFileBase DocumentFile { get; set; }
    }
}