namespace BGB.Data.StoreProcedure.dbo
{
    public partial class Item_Stock_ByUnitAndItemResult
    {
        public double? DirectReceivedQuantity { get; set; }

        public double? ExpenseQuantity { get; set; }

        public double? IssueQuantity { get; set; }

        public int ItemId { get; set; }

        public double? PurchaseOrderReceivedQuantity { get; set; }

        public double? ReceivedFromOtherUnit { get; set; }

        public double? StockQty { get; set; }

        public double? TransferToOtherUnit { get; set; }
    }
}
