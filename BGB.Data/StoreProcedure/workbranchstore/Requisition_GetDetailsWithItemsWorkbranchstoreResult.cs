using System;

namespace BGB.Data.StoreProcedure.workbranchstore
{
    public partial class Requisition_GetDetailsWithItemsWorkbranchstoreResult
    {
        public string AuthorizedBy { get; set; }

        public string CategoryName { get; set; }

        public double DemandQty { get; set; }

        public bool IsComplete { get; set; }

        public string ItemBarCode { get; set; }

        public string ItemName { get; set; }

        public string MainRemarks { get; set; }

        public string Remarks { get; set; }

        public DateTime RequisitionDate { get; set; }

        public int RequisitionId { get; set; }

        public string RequisitionNo { get; set; }

        public string SetupUnitName { get; set; }

        public string UnitName { get; set; }
    }
}
