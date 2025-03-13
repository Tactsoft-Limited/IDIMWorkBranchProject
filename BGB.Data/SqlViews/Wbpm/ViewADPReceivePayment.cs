using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.SqlViews.Wbpm
{
    [Table("ViewADPReceivePayments", Schema = "wbpm")]
    public class ViewADPReceivePayment
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ADPReceivePaymentId { get; set; }
        public int ProjectWorkId { get; set; }
        public string ProjectWorkTitleB { get; set; }
        public decimal EstimatedCost { get; set; }
        public string FirmNameB { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string LetterNo { get; set; }
        public int BillNumber { get; set; }
        public DateTime? BillDate { get; set; }
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
    }
}
