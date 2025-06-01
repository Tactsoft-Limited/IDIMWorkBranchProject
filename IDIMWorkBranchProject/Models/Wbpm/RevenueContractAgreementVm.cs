using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class RevenueContractAgreementVm
    {
        public RevenueContractAgreementVm()
        {
            AddDGDropdown = new List<SelectListItem>();
            DDGDropdown = new List<SelectListItem>();
            ProjectdirectorDropdown = new List<SelectListItem>();
            DirectorDropdown = new List<SelectListItem>();
            ConstructionFirmDropdown = new List<SelectListItem>();
        }

        [Key]
        public int ContractAgreementId { get; set; }

        [Display(Name = "রাজস্ব")]
        public int RevenueId { get; set; }

        [DisplayName("রাজস্ব কাজের শিরোনাম")]
        public string RevenueWorkTitle { get; set; }

        [DisplayName("রাজস্ব কাজের শিরোনাম")]
        public string RevenueWorkTitleB { get; set; }

        [Display(Name = "ঠিকাদার প্রতিষ্ঠান")]
        public int ConstructionCompanyId { get; set; }

        [Display(Name = "ঠিকাদার প্রতিষ্ঠান")]
        public string ConstructionFirm { get; set; }

        [Display(Name = "চুক্তির তারিখ")]
        public DateTime? AgreementDate { get; set; }

        [Display(Name = "অতিরিক্ত মহাপরিচালক")]
        public int? AddDGId { get; set; }

        [Display(Name = "অতিরিক্ত মহাপরিচালক")]
        public string AddDGName { get; set; }

        [Display(Name = "উপ-মহাপরিচালক")]
        public int? DDGId { get; set; }

        [Display(Name = "উপ-মহাপরিচালক")]
        public string DDGName { get; set; }

        [Display(Name = "প্রকল্প পরিচালক")]
        public int? ProjectDirectorId { get; set; }

        [Display(Name = "প্রকল্প পরিচালক")]
        public string ProjectDirectorName { get; set; }

        [Display(Name = "পরিচালক")]
        public int? DirectorId { get; set; }

        [Display(Name = "পরিচালক")]
        public string DirectorName { get; set; }


        [DisplayName("চুক্তি ডকুমেন্ট")]
        public string ScanDocument { get; set; }

        [DisplayName("চুক্তি ডকুমেন্ট")]
        public HttpPostedFileBase DocumentFile { get; set; }


        public IEnumerable<SelectListItem> AddDGDropdown { get; set; }
        public IEnumerable<SelectListItem> DDGDropdown { get; set; }
        public IEnumerable<SelectListItem> ProjectdirectorDropdown { get; set; }
        public IEnumerable<SelectListItem> DirectorDropdown { get; set; }
        public IEnumerable<SelectListItem> ConstructionFirmDropdown { get; set; }
    }
}