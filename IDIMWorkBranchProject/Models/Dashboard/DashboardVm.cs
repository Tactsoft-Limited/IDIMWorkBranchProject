namespace IDIMWorkBranchProject.Models.Dashboard
{
    public class DashboardVm
    {
        public int Project { get; set; }
        public int Subproject { get; set; }
        public double? TotalBillPayment { get; set; }
        public double TotalBillReceived { get; set; }
        public int ProjectExtended { get; set; }
        public int ProjectProblem { get; set; }
    }
}