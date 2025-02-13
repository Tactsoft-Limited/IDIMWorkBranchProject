using BGB.Data.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Wbpm
{
    [Table("ContractAgreement", Schema ="wbpm")]
    public class ContractAgreement : BaseEntity
    {
        [Key]
        public int ContractAgreementId { get; set; }
        public int ProjectWorkId { get; set; }
        public string ContractDay { get; set; }
        public DateTime ContractDate { get; set; }
        public decimal AgreementCost { get; set; }
        public string AgreementCostInWord { get; set; }


        [ForeignKey(nameof(ProjectWorkId))]
        public ProjectWork ProjectWork { get; set; }
    }
}
