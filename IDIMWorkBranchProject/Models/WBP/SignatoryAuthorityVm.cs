using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class SignatoryAuthorityVm
    {
        public SignatoryAuthorityVm()
        {
            ProjectDropdown = new List<SelectListItem>();
            ReceivePaymentDropdown = new List<SelectListItem>();
        }
        public int SignatoryAuthorityId { get; set; }

        [Required(ErrorMessage = "Receive Payment ID is required.")]
        [Display(Name = "Receive Payment ID")]
        public int ReceivePaymentId { get; set; }

        [Display(Name = "Letter Number")]
        public string LetterNumber { get; set; }

        [Required(ErrorMessage = "Project is required.")]
        [Display(Name = "Project")]
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Authority Name is required.")]
        [StringLength(150, ErrorMessage = "Authority Name cannot exceed 150 characters.")]
        [Display(Name = "Authority Name")]
        public string AuthorityName { get; set; }

        [StringLength(150, ErrorMessage = "Designation cannot exceed 150 characters.")]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [StringLength(200, ErrorMessage = "Note cannot exceed 200 characters.")]
        [Display(Name = "Note")]
        public string Note { get; set; }


        public IEnumerable<SelectListItem> ProjectDropdown { get; set; }
        public IEnumerable<SelectListItem> ReceivePaymentDropdown { get; set; }
    }
}