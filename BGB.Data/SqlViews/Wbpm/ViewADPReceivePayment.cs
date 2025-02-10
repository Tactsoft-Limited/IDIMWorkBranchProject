using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.SqlViews.Wbpm
{
	[Table("ViewADPReceivePayments", Schema ="wbpm")]
	public class ViewADPReceivePayment
	{
		[Key]
		[Column(Order = 0)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int ADPReceivePaymentId { get; set; }
		public string LetterNo { get; set; }
		public string BillNumber { get; set; }
		public DateTime BillDate { get; set; }
		public DateTime ExtraTime { get; set; } 
		public string BillPaymentSector { get; set; }
		public decimal ActualWorkProgressPer { get; set; }
		public decimal FinancialProgressPer { get; set; }
		public decimal BillPaidPer { get; set; }
		public decimal BillPaidAmount { get; set; }
		public string BillPaidAmountInWord { get; set; }
		public decimal TotalDeductionAmount { get; set; }
		public decimal DepositInBGBFund { get; set; }
		public string FirmNameB { get; set; }
		public string FirmName { get; set; }
		public string ProjectWorkTitle { get; set; }
		public decimal EstimatedCost { get; set; }
		public DateTime WorkStartDate { get; set; }
		public DateTime WorkEndDate { get; set; }
	}
}
