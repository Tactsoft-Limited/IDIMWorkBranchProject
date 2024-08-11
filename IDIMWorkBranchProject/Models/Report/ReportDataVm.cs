using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IDIMWorkBranchProject.Models.Report
{
    public class ReportDataVm
    {
        public string SubProjectTitle { get; set; }
        public string ConstructionFirmName { get; set; }
        public double Amount { get; set; }
        public string Letterno { get; set; }
        public DateTime Letterdate { get; set; }

        public double AgreementCost { get; set; }
        public int Status { get; set; }
        public bool IsDelivered { get; set; }
        public bool IsFinalBillPaid { get; set; }
        public bool TenPercentGuarantee { get; set; }
        public bool FivePercentSecurityMoney { get; set; }
        public double TenPercentPaidFarm { get; set; }
        public double TenPercentPaidSimantoFund { get; set; }
        public string FiscalYearDescription { get; set; }

        public string ConsultantName { get; set; }
        public double ConsultancyPercent { get; set; }
        public double TaxAndVat { get; set; }
        public double TotalCost { get; set; }
        public double ConsultancyPercentOfTotalCost { get; set; }
        public double ConsultancyFeePercent { get; set; }
        public double Remaining { get; set; }

        public int Progress { get; set; }
        public int NetProgress { get; set; }
        public int ConsultantProgressForSupervission { get; set; }
        public int ProgressAccordingToContract { get; set; }
        public double AgreedMoney { get; set; }
        public double PaidMoney { get; set; }
        public double PayableMoney { get; set; }
        public double PayableForProgress { get; set; }
        public double RestOfPayableForProgress { get; set; }
        public int YearlyData { get; set; }

        public string ContactPhone { get; set; }
        public bool hasCompleted { get; set; }
        public string BGOrPayOrder { get; set; }
        public bool BGConfirmed { get; set; }
        public bool ContractComplete { get; set; }
        public DateTime WorkOrderDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool DesignSent { get; set; }
        public double Installment1 { get; set; }
        public double Installment2 { get; set; }
        public double Installment4 { get; set; }
        public double Installment5 { get; set; }
        public double TotalAmmount { get; set; }
        public double BGBMiscellaneousDeposit { get; set; }

        public double IncomeTax { get; set; }
        public double Vat { get; set; }
        public double Bail { get; set; }
        public double TotalDeduction { get; set; }
        public double NetTotalAfterDeduction { get; set; }
        public double NetIncomeTaxAndVat { get; set; }
        public double NetReceivable { get; set; }
        public int TotalBill { get; set; }
        public int BillAmount { get; set; }
        public double PrevBillPayment { get; set; }
        public double NetReceivableAfterComplete { get; set; }
        public double BGBFund { get; set; }

        public long RowNumber { get; set; }
    }
}