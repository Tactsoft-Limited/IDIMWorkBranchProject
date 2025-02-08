using System;

namespace BGB.Data.StoreProcedure.dbo
{
    public partial class PurchaseOrderReceived_GetDetailsWithItemsDboResult
    {
        public string CategoryName { get; set; }

        public DateTime DateOfReceived { get; set; }

        public string ItemBarCode { get; set; }

        public string ItemName { get; set; }

        public string MainRemarks { get; set; }

        public double Quantity { get; set; }

        public string RackName { get; set; }

        public string ReceivedBy { get; set; }

        public int ReceivedId { get; set; }

        public string RecievedChallan { get; set; }

        public string Remarks { get; set; }

        public string SetupUnitName { get; set; }

        public string StoreName { get; set; }

        public string UnitName { get; set; }

        public DateTime? WarrantyDate { get; set; }
    }
}
