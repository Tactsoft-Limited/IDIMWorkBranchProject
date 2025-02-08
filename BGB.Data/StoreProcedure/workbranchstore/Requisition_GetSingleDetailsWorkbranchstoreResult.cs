using System;

namespace BGB.Data.StoreProcedure.workbranchstore
{
    public partial class Requisition_GetSingleDetailsWorkbranchstoreResult
    {
        public string AuthorizedBy { get; set; }

        public string CategoryName { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int CreatedUser { get; set; }

        public double DemandQty { get; set; }

        public bool IsComplete { get; set; }

        public string ItemBarCode { get; set; }

        public string ItemName { get; set; }

        public string ItemRemarks { get; set; }

        public string NoteSheetAttached { get; set; }

        public string Remarks { get; set; }

        public DateTime RequisitionDate { get; set; }

        public int RequisitionId { get; set; }

        public string RequisitionNo { get; set; }

        public string SetupUnitName { get; set; }

        public int UnitId { get; set; }

        public string UnitName { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public int UpdateNo { get; set; }
    }
}
