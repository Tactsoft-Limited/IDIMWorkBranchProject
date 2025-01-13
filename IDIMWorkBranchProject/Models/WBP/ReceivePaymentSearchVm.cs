using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class ReceivePaymentSearchVm : DataTablaVm
    {
        public ReceivePaymentSearchVm()
        {
            ReceivePayments = new List<ReceivePaymentVm>();
            ProjectDropdown = new List<SelectListItem>();
            ConstructionFirmDropdown = new List<SelectListItem>();
        }


        [Display(Name = "Project")]
        public int? ProjectId { get; set; }

        [Display(Name = "Firm Name")]
        public int? ConstructionFirmId { get; set; }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Display(Name = "Firm Name")]
        public string ConstructionFirmName { get; set; }

        [Display(Name = "Letter Number")]
        public string LetterNo { get; set; }

        [Display(Name = "Bill Date")]
        public DateTime? BillDate { get; set; }


        public List<ReceivePaymentVm> ReceivePayments { get; set; }

        public IEnumerable<SelectListItem> ProjectDropdown { get; set; }
        public IEnumerable<SelectListItem> ConstructionFirmDropdown { get; set; }

    }

}