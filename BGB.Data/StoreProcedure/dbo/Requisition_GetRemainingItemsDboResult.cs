using System;

namespace BGB.Data.StoreProcedure.dbo
{
    public partial class Requisition_GetRemainingItemsDboResult
    {
        public DateTime CreatedDateTime { get; set; }

        public int CreatedUser { get; set; }

        public string ItemBarCode { get; set; }

        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public string ItemStatus { get; set; }

        public string ItemType { get; set; }

        public double? OrderQuantity { get; set; }

        public int ProductBrandId { get; set; }

        public int ProductCategoryId { get; set; }

        public int? ProductGroupId { get; set; }

        public int ProductTypeId { get; set; }

        public int ProductUnitId { get; set; }

        public int? ProductWarrantyId { get; set; }

        public double? ReceivedQuantity { get; set; }

        public string Remarks { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public int UpdateNo { get; set; }
    }
}
