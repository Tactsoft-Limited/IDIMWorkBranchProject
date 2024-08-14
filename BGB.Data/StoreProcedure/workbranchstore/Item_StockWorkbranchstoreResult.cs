namespace BGB.Data.StoreProcedure.workbranchstore
{
    public partial class Item_StockWorkbranchstoreResult
    {
        public string CategoryName { get; set; }

        public double? DirectReceivedQuantity { get; set; }

        public double? ExpenseQuantity { get; set; }

        public double? IssueQuantity { get; set; }

        public string ItemBarCode { get; set; }

        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public double? PurchaseOrderReceivedQuantity { get; set; }

        public double? StockQuantity { get; set; }

        public string UnitName { get; set; }
    }
}
