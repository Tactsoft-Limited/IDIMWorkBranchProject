using BGB.Data.StoreProcedure;
using System;
using System.Data.Entity;
using System.Linq;

namespace BGB.Data.Database
{
    public partial class IDIMDBEntities
    {
        private IQueryable<TEntity> FromExpression<TEntity>(Func<IQueryable<TEntity>> query) { return query.Invoke(); }

        protected void OnModelCreatingGeneratedFunctions(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FiscalYearResult>();
            modelBuilder.Entity<fn_splitResult>();
            modelBuilder.Entity<fnGetExactDateDifferenceResult>();
            modelBuilder.Entity<GetLastMonthResult>();
            modelBuilder.Entity<GetLastMonthsResult>();
            modelBuilder.Entity<getUnitResult>();
            modelBuilder.Entity<LastMonthsResult>();
            modelBuilder.Entity<splitstringResult>();
        }


        [DbFunction("AccountCodeWisseBill", "budget")]
        public static double? AccountCodeWisseBill(int? acid)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("AgeAdd", "irms")]
        public static int? AgeAdd(int? y1, int? m1, int? d1, int? y2, int? m2, int? d2, string part)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("AgeCalculate", "irms")]
        public static int? AgeCalculate(decimal? totalDays, string returnType)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("AgeSubtract", "irms")]
        public static int? AgeSubtract(int? y1, int? m1, int? d1, int? y2, int? m2, int? d2, string part)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("AgeToTotalDay", "dbo")]
        public static decimal? AgeToTotalDayDbo(int? Year, int? Month, int? Day)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("AgeToTotalDay", "irms")]
        public static decimal? AgeToTotalDayIrms(int? Year, int? Month, int? Day)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("AvailableQuantity", "cmsd")]
        public static double? AvailableQuantity(int? medicineId)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("BatchWiseDemandIssueQuantity", "cmsd")]
        public static double? BatchWiseDemandIssueQuantity(string batchno, int? Mid)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("BatchWiseIssueQty", "dbo")]
        public static double? BatchWiseIssueQty(string batchno, int? Mid)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("BatchWiseRecievedQty", "dbo")]
        public static double? BatchWiseRecievedQty(string batchno, int? Mid)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("DemandIssueQuantity", "cmsd")]
        public static double? DemandIssueQuantity(int? demandId, int? medicineId)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("DisposalQty", "cmsd")]
        public static double? DisposalQty(int? medicineid, DateTime? datefrom, DateTime? dateTo)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("DurationCalculate", "irms")]
        public static int? DurationCalculate(int? totalDays, string returnType)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("DurationCalculateFinal", "irms")]
        public static int? DurationCalculateFinal(int? totalDays, string returnType)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("FiscalYear", "idim")]
        public IQueryable<FiscalYearResult> FiscalYear(int? month, int? fiscalYearId)
        { return FromExpression(() => FiscalYear(month, fiscalYearId)); }

        [DbFunction("fn_split", "bsb")]
        public IQueryable<fn_splitResult> fn_split(string str, string delimiter)
        { return FromExpression(() => fn_split(str, delimiter)); }

        [DbFunction("fnBidroho", "irms")]
        public static string fnBidroho(int? armyid)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("fnBudgetAdjustment", "budget")]
        public static double? fnBudgetAdjustment(int? fiscalYearId, int? accountCodeId)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("fnGetExactDateDifference", "irms")]
        public IQueryable<fnGetExactDateDifferenceResult> fnGetExactDateDifference(
            DateTime? startDate,
            DateTime? endDate)
        { return FromExpression(() => fnGetExactDateDifference(startDate, endDate)); }

        [DbFunction("fnHillService", "irms")]
        public static string fnHillService(int? armyid)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("fnHillServiceYear", "irms")]
        public static double? fnHillServiceYear(int? armyid)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("fnLastMonth", "budget")]
        public static int? fnLastMonth(int? month)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("ftMonthInBangla", "cmsd")]
        public static string ftMonthInBangla(DateTime? Date)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("get_Exact_Date_diff", "dbo")]
        public static string get_Exact_Date_diff(DateTime? date, DateTime? date2)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("GetAcrBlackNotification", "irms")]
        public static bool? GetAcrBlackNotification(int? armyId)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("GetAcrRedNotification", "irms")]
        public static bool? GetAcrRedNotification(int? armyId)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("GetAge", "irms")]
        public static int? GetAge(DateTime? date, DateTime? date2, string part)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("GetAgeYearMonthD", "irms")]
        public static string GetAgeYearMonthD(DateTime? date)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("GetAgeYearMonthDays", "irms")]
        public static string GetAgeYearMonthDays(DateTime? date)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("GetCurrentBill", "budget")]
        public static double? GetCurrentBill(
            int? accountCodeId,
            int? unitId,
            int? fiscalYearId,
            int? billMonth,
            int? billYear)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("GetDateDiffInt", "irms")]
        public static int? GetDateDiffInt(string pYrMth, DateTime? pFromDate, DateTime? pToDate)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("GetDateDiffString", "irms")]
        public static string GetDateDiffString(DateTime? pFromDate, DateTime? pToDate)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("GetDateDiffStringbk", "irms")]
        public static string GetDateDiffStringbk(DateTime? pFromDate, DateTime? pToDate)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("getDD", "irms")]
        public static string getDD(string dstart, string dend)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("getFiscalYear", "irms")]
        public static string getFiscalYear(DateTime? date)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("GetGPFPrefixBn", "irms")]
        public static string GetGPFPrefixBn(string gpf)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("GetLastBill", "budget")]
        public static double? GetLastBill(
            int? accountCodeId,
            int? unitId,
            int? fiscalYearId,
            DateTime? fiscalYearStartDate,
            int? billMonth,
            int? billYear)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("GetLastMonth", "idim")]
        public IQueryable<GetLastMonthResult> GetLastMonth(int? month, int? fiscalYearId)
        { return FromExpression(() => GetLastMonth(month, fiscalYearId)); }

        [DbFunction("GetLastMonths", "idim")]
        public IQueryable<GetLastMonthsResult> GetLastMonths(int? months)
        { return FromExpression(() => GetLastMonths(months)); }

        [DbFunction("GetLatestBudget", "budget")]
        public static double? GetLatestBudget(int? unitBudgetId)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("getmm", "irms")]
        public static string getmm(string dstart, string dend)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("getUnit", "dbo")]
        public IQueryable<getUnitResult> getUnit(int? sectorId) { return FromExpression(() => getUnit(sectorId)); }

        [DbFunction("GetUnitBudgetWithAdjustment", "budget")]
        public static double? GetUnitBudgetWithAdjustment(int? unitId, int? fiscalYearId, int? accountCodeId)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("getYY", "irms")]
        public static string getYY(string dstart, string dend)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("issueqty", "cmsd")]
        public static double? issueqty(int? medicineid, DateTime? datefrom, DateTime? dateTo)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("LastMonths", "idim")]
        public IQueryable<LastMonthsResult> LastMonths(int? count) { return FromExpression(() => LastMonths(count)); }

        [DbFunction("poqty", "dbo")]
        public static int? poqty(int? pono, int? medicineid)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("porcvqty", "dbo")]
        public static int? porcvqty(int? pono, int? medicineid)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("porqty", "cmsd")]
        public static double? porqty(int? medicineid, DateTime? datefrom, DateTime? dateTo)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("PurchaseOrderPrice", "central")]
        public static double? PurchaseOrderPrice(int? purchaseOrderId, int? itemId)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("PurchaseOrderQuantity", "central")]
        public static double? PurchaseOrderQuantityCentral(int? purchaseOrderId, int? itemId)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("PurchaseOrderQuantity", "cmsd")]
        public static double? PurchaseOrderQuantityCmsd(int? purchaseOrderId, int? medicineId)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("PurchaseOrderQuantity", "eme")]
        public static int? PurchaseOrderQuantityEme(int? purchaseOrderId, int? partId)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("PurchaseOrderReceiveQuantity", "central")]
        public static double? PurchaseOrderReceiveQuantityCentral(int? purchaseOrderId, int? itemId)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("PurchaseOrderReceiveQuantity", "cmsd")]
        public static double? PurchaseOrderReceiveQuantityCmsd(int? purchaseOrderId, int? medicineId)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("PurchaseOrderReceiveQuantity", "eme")]
        public static int? PurchaseOrderReceiveQuantityEme(int? purchaseOrderId, int? partId)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("purqty", "eme")]
        public static int? purqty(int? partid, int? fiscalyearid)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("RegimentDemandIssueQuantity", "cmsd")]
        public static double? RegimentDemandIssueQuantity(int? regimentDemandId, int? medicineId)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("Regqty", "cmsd")]
        public static double? Regqty(int? medicineid, DateTime? datefrom, DateTime? dateTo)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("Returnqty", "cmsd")]
        public static double? Returnqty(int? medicineid, DateTime? datefrom, DateTime? dateTo)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("sanctionqty", "eme")]
        public static int? sanctionqty(int? partid, int? fiscalyearid)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }

        [DbFunction("splitstring", "dbo")]
        public IQueryable<splitstringResult> splitstring(string stringToSplit)
        { return FromExpression(() => splitstring(stringToSplit)); }

        [DbFunction("Transferqty", "cmsd")]
        public static double? Transferqty(int? medicineid, DateTime? datefrom, DateTime? dateTo)
        { throw new NotSupportedException("This method can only be called from Entity Framework Core queries"); }
    }
}
