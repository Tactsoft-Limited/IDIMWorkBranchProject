using System.ComponentModel.DataAnnotations;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class ConsultantVm
    {
        [Display(Name = "Id")]
        public int ConsultantId { get; set; }
        [Display(Name = "Consultant Name")]
        public string Name { get; set; }
        public string Address { get; set; }
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone no.")]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        public string Fax { get; set; }


        //public int CreatedUser { get; set; }
        //public DateTime CreatedDateTime { get; set; }
        //public int? UpdatedUser { get; set; }
        //public DateTime? UpdatedDateTime { get; set; }
        //public int UpdateNo { get; set; }
    }
}