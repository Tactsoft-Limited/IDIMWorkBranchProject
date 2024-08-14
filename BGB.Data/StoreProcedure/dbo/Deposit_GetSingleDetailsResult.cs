using System;

namespace BGB.Data.StoreProcedure.dbo
{
    public partial class Deposit_GetSingleDetailsResult
    {
        public string AuthorizedBy { get; set; }

        public string CategoryName { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int CreatedUser { get; set; }

        public DateTime? DepositDate { get; set; }

        public int DepositId { get; set; }

        public int? FromUnitId { get; set; }

        public string FromUnitName { get; set; }

        public string ItemBarCode { get; set; }

        public string ItemName { get; set; }

        public string ItemRemarks { get; set; }

        public DateTime? LetterDate { get; set; }

        public string LetterNo { get; set; }

        public double Quantity { get; set; }

        public string Remarks { get; set; }

        public int? StoreId { get; set; }

        public int? ToUnitId { get; set; }

        public string ToUnitName { get; set; }

        public string UnitName { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public int UpdateNo { get; set; }
    }
}
