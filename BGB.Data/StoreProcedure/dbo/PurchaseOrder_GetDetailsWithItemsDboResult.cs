using System;

namespace BGB.Data.StoreProcedure.dbo
{
    public partial class PurchaseOrder_GetDetailsWithItemsDboResult
    {
        public string CategoryName { get; set; }

        public bool IsComplete { get; set; }

        public string ItemBarCode { get; set; }

        public string ItemName { get; set; }

        public double ItemPrice { get; set; }

        public string MainRemarks { get; set; }

        public DateTime? PODalivaryDate { get; set; }

        public DateTime? PODate { get; set; }

        public string PONoteSheetNo { get; set; }

        public int PurchaseOrderId { get; set; }

        public string PurchaseOrderNo { get; set; }

        public double Quantity { get; set; }

        public string Remarks { get; set; }

        public string SupplierName { get; set; }

        public string UnitName { get; set; }
    }
}
