using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace IDIMWorkBranchProject.Models.WBP
{
    public class ConsultancyFeesVm
    {
        public ConsultancyFeesVm()
        {
            SubProjectDropdown = new List<SelectListItem>();
            ConsultantDropdown = new List<SelectListItem>();
        }
        [Display(Name = "Id")]
        public int CFId { get; set; }
        public int SubProjectDetailsId { get; set; }
        [Display(Name = "Sub Project")]
        public int SubProjectId { get; set; }
        [Display(Name = "Sub Project")]
        public string SubProjectTitle { get; set; }
        [Display(Name = "Consultant")]
        public int ConsultantId { get; set; }
        [Display(Name = "Consultant")]
        public string ConsultantName { get; set; }

        [Display(Name = "Consultancy Fee")]
        public float ConsultancyFee { get; set; }

        [Display(Name = "Vat/Tax")]
        public float Vat_Tax { get; set; }
        [Display(Name = "Nit Ammount of Consultancy Fee")]
        public float NitAmmountConsultancyFee { get; set; }
        [Display(Name = "Other Consultancy Fee")]
        public float OtherConsultancyFee { get; set; }
        [Display(Name = "Rest Amount")]
        public float RestAmount { get; set; }
        [Display(Name = "Remarks")]
        public string Remark { get; set; }

        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }
        public IEnumerable<SelectListItem> SubProjectDropdown { get; set; }
        public IEnumerable<SelectListItem> ConsultantDropdown { get; set; }
    }
}