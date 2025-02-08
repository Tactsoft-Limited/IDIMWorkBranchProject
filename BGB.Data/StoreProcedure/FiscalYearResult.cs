using System;

namespace BGB.Data.StoreProcedure
{
    public partial class FiscalYearResult
    {
        public string FiscalYear { get; set; }

        public DateTime? FiscalYearStartDate { get; set; }

        public int? Month { get; set; }

        public string MonthName { get; set; }

        public int? Year { get; set; }
    }
}
