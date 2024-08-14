using System;

namespace BGB.Data.StoreProcedure
{
    public partial class GetLastMonthResult
    {
        public DateTime? FiscalYearEndDate { get; set; }

        public string FiscalYearName { get; set; }

        public DateTime? FiscalYearStartDate { get; set; }

        public int? LastMonth { get; set; }

        public DateTime? LastMonthEndDate { get; set; }

        public string LastMonthName { get; set; }

        public int? LastMonthYear { get; set; }

        public int? Month { get; set; }

        public string MonthName { get; set; }

        public int? Year { get; set; }
    }
}
