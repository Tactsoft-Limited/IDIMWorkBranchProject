using Microsoft.Reporting.Map.WebForms.BingMaps;

namespace IDIMWorkBranchProject.Models.Dashboard
{
    public class DashboardVm
    {
        //public int Project { get; set; }
        //public int Subproject { get; set; }
        //public double? TotalBillPayment { get; set; }
        //public double TotalBillReceived { get; set; }
        //public int ProjectExtended { get; set; }
        //public int ProjectProblem { get; set; }

        public int OngoingProjects { get; set; }
        public int TotalConstructionWork { get; set; }
        public int CompletedConstructionWork { get; set; }
        public int ConstructionWorkInProgress { get; set; }

    }
}