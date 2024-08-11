using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class SecurityDepositSearchVm
    {
        public SecurityDepositSearchVm()
        {
            SubProjectDropdown = new List<SelectListItem>();
            SecurityDeposits = new List<SecurityDepositVm>();
        }

        [Display(Name = "Sub Project")]
        public int? SubProjectId { get; set; }

        [Display(Name = "Letter No")]
        public string Letterno { get; set; }

        public IEnumerable<SelectListItem> SubProjectDropdown { get; set; }
        public List<SecurityDepositVm> SecurityDeposits { get; set; }
    }
}