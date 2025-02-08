using System;

namespace BGB.Data.StoreProcedure.dbo
{
    public partial class DirectReceived_GetSingleDetailsDboResult
    {
        public string AutherizedBy { get; set; }

        public string CategoryName { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int CreatedUser { get; set; }

        public DateTime DateOfReceived { get; set; }

        public int DirectReceivedId { get; set; }

        public string DirectRecievedChallan { get; set; }

        public string ItemBarCode { get; set; }

        public string ItemName { get; set; }

        public double ItemPrice { get; set; }

        public double Quantity { get; set; }

        public string RackName { get; set; }

        public string Remarks { get; set; }

        public string SetupUnitName { get; set; }

        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public int? SupplierId { get; set; }

        public int UnitId { get; set; }

        public string UnitName { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public int UpdateNo { get; set; }
    }
}
