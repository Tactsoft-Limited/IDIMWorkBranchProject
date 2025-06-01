namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class BGBFundVm
    {
        public int BGBFundId { get; set; }
        public int ProjectWorkId { get; set; }
        public decimal? AmountDeposited { get; set; }
        public int? PaidFromBGBFundId { get; set; }
        public decimal? PaidAmount { get; set; }
    }
}