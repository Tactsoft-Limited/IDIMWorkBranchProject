using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class VatTaxSearchVm : DataTablaVm
    {
        public VatTaxSearchVm()
        {
            ProjectDropdown = new List<SelectListItem>();
            ReceivePaymentDropdown = new List<SelectListItem>();
        }


        [Display(Name = "Project")]
        public int ProjectId { get; set; }

        [Display(Name = "Receive Payment")]
        public int ReceivePaymentId { get; set; }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }


        [Display(Name = "Letter Number")]
        public string LetterNumber { get; set; }


        public IEnumerable<SelectListItem> ProjectDropdown { get; set; }
        public IEnumerable<SelectListItem> ReceivePaymentDropdown { get; set; }

        public List<VatTaxVm> VatTaxes { get; set; }
    }
}