using BGB.Data.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Wbpm
{
    [Table("SignatoryAuthority", Schema ="wbpm")]
    public class SignatoryAuthority : BaseEntity
    {
        public int SignatoryAuthorityId { get; set; }

        public string AuthorityName { get; set; }

        public string AuthorityNameB { get; set; }

        public string Designation {  get; set; }    

        public string DesignationB { get;set; } 
    }
}
