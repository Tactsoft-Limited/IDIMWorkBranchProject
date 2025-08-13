using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using BGB.Data.Entities.Base;

namespace BGB.Data.Entities.Wbpm
{
    [Table("ADPReceivePayment", Schema = "wbpm")]
    public class ADPReceivePayment : BaseEntity
    {
        public ADPReceivePayment()
        {
            VatTaxCollaterals = new HashSet<VatTaxCollateral>();
        }


        [Key]
        public int ADPReceivePaymentId { get; set; }

        public int ProjectWorkId { get; set; }
        public string LetterNo { get; set; }
        public int BillNumber { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime? ExtraTime { get; set; }
        public string BillPaymentSector { get; set; }
        public double ActualWorkProgressPer { get; set; }
        public double? FinancialProgressPer { get; set; }
        public double? BillPaidPerTillDate { get; set; }
        public decimal? BillPaidAmountTillDate { get; set; }
        public double BillPaidPer { get; set; }
        public decimal BillPaidAmount { get; set; }
        public string BillPaidAmountInWord { get; set; }
        public double? TotalReceivePer { get; set; }
        public decimal? TotalReceiveAmount { get; set; }
        public string TotalReceiveAmountInWord { get; set; }
        public bool IsDepositeBGBFund { get; set; }
        public string Remarks { get; set; }
        public int? HeadAssistantId { get; set; }
        public int? ConcernedEngineerId { get; set; }
        public int? SectionICId { get; set; }
        public int? BranchClerkId { get; set; }
        public int? OfficerId { get; set; }


        #region Navigation Property
        [ForeignKey(nameof(ProjectWorkId))]
        public virtual ProjectWork ProjectWork { get; set; }

        public virtual ICollection<BGBMiscellaneousFund> BGBMiscellaneousFunds { get; set; }
        public ICollection<VatTaxCollateral> VatTaxCollaterals { get; set; }

        [ForeignKey(nameof(BranchClerkId))]
        public virtual SignatoryAuthority BranchClerk { get; set; }

        [ForeignKey(nameof(ConcernedEngineerId))]
        public virtual SignatoryAuthority ConcernedEngineer { get; set; }

        [ForeignKey(nameof(HeadAssistantId))]
        public virtual SignatoryAuthority HeadAssistant { get; set; }

        [ForeignKey(nameof(SectionICId))]
        public virtual SignatoryAuthority SectionIC { get; set; }

        [ForeignKey(nameof(OfficerId))]
        public virtual SignatoryAuthority Officers { get; set; }
        #endregion
    }
}
