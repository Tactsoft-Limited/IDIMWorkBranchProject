using System.Collections.Generic;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class ProjectWorkDetailsVm
    {
        public ProjectWorkVm ProjectWorks { get; set; }
        public NohaVm Noha { get; set; }
        public PerformanceSecurityVm PerformanceSecurity { get; set; }
        public ContractAgreementVm ContractAgreement { get; set; }
        public WorkOrderVm WorkOrder { get; set; }
        public ProjectWorkStatusVm ProjectWorkStatus { get; set; }
        public List<ADPReceivePaymentVm> ADPReceivePayments { get; set; }
        public List<ContractorCompanyPaymentVm> ContractorCompanyPayments { get; set; }

    }
}