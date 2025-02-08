using System;

namespace BGB.Data.StoreProcedure.dbo
{
    public partial class Issue_GetDetailsWithItemsDboResult
    {
        public string AuthorizedBy { get; set; }

        public string CategoryName { get; set; }

        public double? DamageQty { get; set; }

        public DateTime DateOfIssue { get; set; }

        public bool Flag { get; set; }

        public string IssuedBy { get; set; }

        public int IssueId { get; set; }

        public string IssueReferenceNo { get; set; }

        public string IssueSerial { get; set; }

        public string IssueStatus { get; set; }

        public string ItemBarCode { get; set; }

        public string ItemName { get; set; }

        public string MainRemarks { get; set; }

        public double? PartialUsableQty { get; set; }

        public DateTime RequisitionDate { get; set; }

        public string RequisitionNo { get; set; }

        public string SetupUnitName { get; set; }

        public string StoreName { get; set; }

        public double? TotalQty { get; set; }

        public string UnitName { get; set; }

        public double? UsableQty { get; set; }

        public DateTime? VoucherDate { get; set; }

        public string VoucherNo { get; set; }
    }
}
