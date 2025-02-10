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

        [Key]
        public int ADPReceivePaymentId { get; set; }

        public int ProjectWorkId { get; set; }
        public string LetterNo { get; set; }
        public int BillNumber { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime? ExtraTime { get; set; }
        public string BillPaymentSector { get; set; }
        public double ActualWorkProgressPer { get; set; }
        public double FinancialProgressPer { get; set; }
		public decimal? BillPaidTillDate { get; set; }
		public double BillPaidPer { get; set; }
        public decimal BillPaidAmount { get; set; }
		public string BillPaidAmountInWord { get; set; }
		public double TaxPer { get; set; }
        public decimal TaxAmount { get; set; }
        public double VatPer { get; set; }
        public decimal VatAmount { get; set; }
        public double CollateralPer { get; set; }
        public decimal CollateralAmount { get; set; }
        public decimal TotalDeductionAmount { get; set; }
        public decimal DepositInBGBFund { get; set; }
        public bool IsDepositeBGBFund { get; set; }
        public string Remarks { get; set; }


        #region Navigation Property
        [ForeignKey(nameof(ProjectWorkId))]
        public virtual ProjectWork ProjectWork { get; set; }

        public virtual ICollection<BGBMiscellaneousFund> BGBMiscellaneousFunds { get; set; }
        #endregion
    }
}
