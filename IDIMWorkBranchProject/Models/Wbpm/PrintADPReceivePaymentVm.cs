using System;

namespace IDIMWorkBranchProject.Models.Wbpm
{
	public class PrintADPReceivePaymentVm
	{
		public int ADPReceivePaymentId { get; set; }
		public string ProjectTitle { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string ConstructionFirm { get; set; }
		public string LetterNo { get; set; }
		public int BillNumber { get; set; }
		public DateTime BillDate { get; set; }
		public DateTime? ExtraTime { get; set; }
		public string BillPaymentSector { get; set; }
		public decimal? EstimatedCost { get; set; }
		public double ActualWorkProgressPer { get; set; }
		public double FinancialProgressPer { get; set; }
		public decimal? BillPaidTillDate { get; set; }
		public double BillPaidPer { get; set; }
		public decimal BillPaidAmount { get; set; }
		public string BillPaidAmountInWord { get; set; }
	}
}