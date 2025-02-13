using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using BGB.Data.Entities.Base;

namespace BGB.Data.Entities.Wbpm
{
    [Table("BGBMiscellaneousFund", Schema = "wbpm")]
    public class BGBMiscellaneousFund : BaseEntity
    {
        [Key]
        public int FundId { get; set; }

        public int ProjectWorkId { get; set; }
        public int? ADPReceivePaymentId { get; set; }
        public string LetterNo { get; set; }
        public DateTime DepositeDate { get; set; }
        public string PayOrderNo { get; set; }
        public DateTime PayOrderDate { get; set; }
        public string BankName { get; set; }
        public string BrunchName { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }


        #region Navigation Properties
        [ForeignKey(nameof(ProjectWorkId))]
        public virtual ProjectWork ProjectWork { get; set; }

        [ForeignKey(nameof(ADPReceivePaymentId))]
        public virtual ADPReceivePayment ADPReceivePayment { get; set; }
        #endregion
    }
}
