using BGB.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGB.Data.Entities.Wbpm
{
    [Table("FinalBillPayment", Schema = "wbpm")]
    public class FinalBillPayment:BaseEntity
    {
        [Key]
        public int FinalBillPaymentId { get; set; }
        public int ProjectWorkId { get; set; }
        public decimal NetAmountAsPerFinalMeasurement { get; set; }
        public double VatTaxPer { get; set; }
        public decimal VatTaxAmount { get; set; }      
        public decimal ContractorDueAfterVatTaxDeduction { get; set; }
        public int PreviouslyPaidBillNo { get; set; }
        public decimal PreviouslyPaidAmount { get; set; }
        public decimal PayableFinalBill { get; set; }      
        public decimal DepositBGBFund { get; set; }
        public decimal NetFinalBill { get; set; }
        public decimal FinalBill { get; set; }
        public int? PaidFromBGBFundId { get; set; }
        public decimal? RemainingDepositsInBgbFund { get; set; }
        public decimal? DuePaidAmount { get; set; }

        [ForeignKey(nameof(PaidFromBGBFundId))]
        public virtual ICollection<BGBFund> BGBFunds { get; set; }
    }
}
