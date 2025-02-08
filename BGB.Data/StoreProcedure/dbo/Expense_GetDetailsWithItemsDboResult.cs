using System;

namespace BGB.Data.StoreProcedure.dbo
{
    public partial class Expense_GetDetailsWithItemsDboResult
    {
        public string CategoryName { get; set; }

        public DateTime ExpenseDate { get; set; }

        public int ExpenseId { get; set; }

        public string ItemBarCode { get; set; }

        public string ItemName { get; set; }

        public string MainRemarks { get; set; }

        public double Quantity { get; set; }

        public string ReferenceNo { get; set; }

        public string Remarks { get; set; }

        public string SerialNo { get; set; }

        public string SetupUnitName { get; set; }

        public string Status { get; set; }

        public string StoreName { get; set; }

        public string UnitName { get; set; }
    }
}
