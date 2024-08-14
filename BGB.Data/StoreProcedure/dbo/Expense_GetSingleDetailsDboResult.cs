using System;

namespace BGB.Data.StoreProcedure.dbo
{
    public partial class Expense_GetSingleDetailsDboResult
    {
        public string CategoryName { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int CreatedUser { get; set; }

        public DateTime ExpenseDate { get; set; }

        public int ExpenseId { get; set; }

        public string ItemBarCode { get; set; }

        public string ItemName { get; set; }

        public string ItemRemarks { get; set; }

        public double Quantity { get; set; }

        public string ReferenceNo { get; set; }

        public string Remarks { get; set; }

        public string SerialNo { get; set; }

        public string SetupUnitName { get; set; }

        public string Status { get; set; }

        public int StoreId { get; set; }

        public int UnitId { get; set; }

        public string UnitName { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public int UpdateNo { get; set; }
    }
}
