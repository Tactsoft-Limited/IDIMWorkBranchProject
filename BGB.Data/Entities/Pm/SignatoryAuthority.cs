using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.SignatoryAuthority")]
    public class SignatoryAuthority
    {
        [Key]
        public int SignatoryAuthorityId { get; set; }
        public int ReceivePaymentId { get; set; }
        public int ProjectId { get; set; }
        public string AuthorityName { get; set; }
        public string Designation { get; set; }
        public string Note { get; set; }
        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }


        [ForeignKey(nameof(ReceivePaymentId))]
        public virtual ReceivePayment ReceivePayment { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public virtual Project Project { get; set; }
    }
}
