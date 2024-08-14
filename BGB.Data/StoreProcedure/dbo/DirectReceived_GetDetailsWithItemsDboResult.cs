using System;

namespace BGB.Data.StoreProcedure.dbo
{
    public partial class DirectReceived_GetDetailsWithItemsDboResult
    {
        public string AutherizedBy { get; set; }

        public string CategoryName { get; set; }

        public DateTime DateOfReceived { get; set; }

        public int DirectReceivedId { get; set; }

        public string DirectRecievedChallan { get; set; }

        public string ItemBarCode { get; set; }

        public string ItemName { get; set; }

        public double ItemPrice { get; set; }

        public string MainRemarks { get; set; }

        public double Quantity { get; set; }

        public string RackName { get; set; }

        public string SetupUnitName { get; set; }

        public string StoreName { get; set; }

        public string UnitName { get; set; }
    }
}
