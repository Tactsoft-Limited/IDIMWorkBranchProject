using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class ContractorPaymentSearchVm : DataTablaVm
    {
        public ContractorPaymentSearchVm()
        {
            SubProjectDropdown = new List<SelectListItem>();
            ConstructionFirmDropdown = new List<SelectListItem>();
        }

        [Display(Name = "SubProject")]
        public int SubProjectId { get; set; }

        [Display(Name = "Construction Firm")]
        public int ConstructionFirmId { get; set; }

        [Display(Name = "SubProject Title")]
        public string SubProjectTitle { get; set; }


        [Display(Name = "Firm Namer")]
        public string ConstructionFirmName { get; set; }


        public IEnumerable<SelectListItem> SubProjectDropdown { get; set; }
        public IEnumerable<SelectListItem> ConstructionFirmDropdown { get; set; }

        public List<ContractorPaymentVm> ContractorPayments { get; set; }
    }
}