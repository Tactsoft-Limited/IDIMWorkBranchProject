using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.BGBFund")]
    public class BGBFund
    {
        [Key]
        public int BGBFundId { get; set; }
        public string LetterNo { get; set; }
        public DateTime DepositeDate { get; set; }
        public int ProjectId { get; set; }
        public int ConstructionFirmId { get; set; }
        public string PayOrderNo { get; set; }
        public DateTime PayOrderDate { get; set; }
        public string BankName { get; set; }
        public string BrunchName { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }


        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }
        [ForeignKey(nameof(ConstructionFirmId))]
        public ConstructionFirm ConstructionFirm { get; set; }

    }
}
