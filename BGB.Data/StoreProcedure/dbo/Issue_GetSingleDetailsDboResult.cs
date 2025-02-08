using System;

namespace BGB.Data.StoreProcedure.dbo
{
    public partial class Issue_GetSingleDetailsDboResult
    {
        public string AuthorizedBy { get; set; }

        public string CategoryName { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int CreatedUser { get; set; }

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

        public double? PartialUsableQty { get; set; }

        public string Remarks { get; set; }

        public DateTime RequisitionDate { get; set; }

        public int? RequisitionId { get; set; }

        public string RequisitionNo { get; set; }

        public string RequisitionNoteSheet { get; set; }

        public string SetupUnitName { get; set; }

        public bool status { get; set; }

        public int StoreId { get; set; }

        public double? TotalQty { get; set; }

        public int UnitId { get; set; }

        public string UnitName { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public int UpdateNo { get; set; }

        public double? UsableQty { get; set; }

        public DateTime? VoucherDate { get; set; }

        public string VoucherNo { get; set; }
    }
}
