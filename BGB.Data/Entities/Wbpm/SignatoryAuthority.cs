using BGB.Data.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Wbpm
{
    [Table("SignatoryAuthority", Schema ="wbpm")]
    public class SignatoryAuthority : BaseEntity
    {
        public SignatoryAuthority()
        {
            BranchClerks = new HashSet<ADPReceivePayment>();
            ConcernedEngineers = new HashSet<ADPReceivePayment>();
            HeadAssistants = new HashSet<ADPReceivePayment>();
            SectionICs = new HashSet<ADPReceivePayment>();
        }

        [Key]
        public int SignatoryAuthorityId { get; set; }

        public string AuthorityName { get; set; }

        public string AuthorityNameB { get; set; }

        public string Designation {  get; set; }    

        public string DesignationB { get;set; } 


        public ICollection<ADPReceivePayment> BranchClerks { get; set; }
        public ICollection<ADPReceivePayment> ConcernedEngineers { get; set; }
        public ICollection<ADPReceivePayment> HeadAssistants { get; set; }
        public ICollection<ADPReceivePayment> SectionICs { get; set; }
    }
}
