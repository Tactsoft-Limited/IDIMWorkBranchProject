using System;

namespace BGB.Data.Entities.Pm
{
    public partial class FundRelease
    {
        public int FundReleaseId { get; set; }
        public int? ProjectId { get; set; }
        public int? FiscalYearID { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public double? ReleasedAmount { get; set; }
        public string Remark { get; set; }
    }
}
