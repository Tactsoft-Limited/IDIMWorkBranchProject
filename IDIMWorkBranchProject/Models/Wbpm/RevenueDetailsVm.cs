using System.Collections.Generic;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class RevenueDetailsVm
    {
        public RevenueVm RevenueDetail { get; set; }
        public RevenueNohaVm NohaDetail { get; set; }
        public RevenuePerformanceSecurityVm PerformanceSecurityDetail { get; set; }
        public RevenueWorkOrderVm WorkOrderDetail { get; set; }
        public RevenueContractAgreementVm RevenueContractAgreementDetail { get; set; }
        public string ConstructionFirm { get; set; }    
        public List<RevenueWorkOrderVm> WorkOrderList{ get; set; }
    }
}