using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.Wbpm
{

    public class ContractAgreementVm
    {
        public ContractAgreementVm()
        {
    
        }
        [Key]
        public int ContractAgreementId { get; set; }

        [Display(Name ="নির্মান কাজ আইডি")]
        public int ProjectWorkId { get; set; }

        [DisplayName("নির্মান কাজের শিরোনাম")]
        public string ProjectWorkTitle { get; set; }

        [Display(Name = "ঠিকাদার প্রতিষ্ঠান")]
        public string ConstructionFirm { get; set; }

        [Display(Name = "চুক্তির দিন")]
        public string ContractDay { get; set; }
        [Display(Name = "চুক্তির তারিখ")]
        public DateTime ContractDate { get; set; }

        [Display(Name = "চুক্তির পরিমান")]
        public decimal? AgreementCost { get; set; }

        [Display(Name = "চুক্তির পরিমান কথায়")]
        public string AgreementCostInWord { get; set; }

    }
}