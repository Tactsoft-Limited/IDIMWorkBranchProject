using System;

namespace BGB.Data.StoreProcedure.workbranchstore
{
    public partial class PurchaseOrder_GetSingleDetailsWorkbranchstoreResult
    {
        public string CategoryName { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int CreatedUser { get; set; }

        public int DelivaryUnitId { get; set; }

        public string DelivaryUnitName { get; set; }

        public bool IsComplete { get; set; }

        public int? IssueUnitId { get; set; }

        public string IssueUnitName { get; set; }

        public string ItemBarCode { get; set; }

        public string ItemName { get; set; }

        public double ItemPrice { get; set; }

        public string ItemRemarks { get; set; }

        public DateTime? PODalivaryDate { get; set; }

        public DateTime? PODate { get; set; }

        public string PONoteSheetAttachedDoc { get; set; }

        public string PONoteSheetNo { get; set; }

        public double? POTotalValue { get; set; }

        public int PurchaseOrderId { get; set; }

        public string PurchaseOrderNo { get; set; }

        public double Quantity { get; set; }

        public string Remarks { get; set; }

        public int SupplierId { get; set; }

        public string SupplierName { get; set; }

        public string TenderScheduleNo { get; set; }

        public string UnitName { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public int UpdateNo { get; set; }
    }
}
