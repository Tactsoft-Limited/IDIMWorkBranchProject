using System.Collections.Generic;

namespace IDIMWorkBranchProject.Models.Wbpm
{
	public class ProjectWorkDetailsVm
    {
        public ProjectWorkVm ProjectWorks { get; set; }
        public List<ADPReceivePaymentVm> ADPReceivePayments { get; set; }
        public List<ContractorCompanyPaymentVm> ContractorCompanyPayments { get; set; }

    }
}