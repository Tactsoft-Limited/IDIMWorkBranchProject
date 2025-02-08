using System;

namespace BGB.Data.StoreProcedure.dbo
{
    public partial class PurchaseOrderReceived_GetSingleDetailsDboResult
    {
        public string CategoryName { get; set; }

        public string ChallanAttached { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int CreatedUser { get; set; }

        public DateTime DateOfReceived { get; set; }

        public string ItemBarCode { get; set; }

        public string ItemName { get; set; }

        public string ItemRemarks { get; set; }

        public int? PurchaseOrderId { get; set; }

        public string PurchaseOrderNo { get; set; }

        public double Quantity { get; set; }

        public string RackName { get; set; }

        public string ReceivedBy { get; set; }

        public int ReceivedId { get; set; }

        public string RecievedChallan { get; set; }

        public string Remarks { get; set; }

        public string SetupUnitName { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public string SupplierName { get; set; }

        public int UnitId { get; set; }

        public string UnitName { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public int UpdateNo { get; set; }

        public DateTime? WarrantyDate { get; set; }
    }
}
