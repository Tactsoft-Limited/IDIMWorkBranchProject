using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class ProjectWorkDetailsVm
    {
        public ProjectWorkVm ProjectWorks { get; set; }
        public List<ADPReceivePaymentVm> ADPReceivePayments { get; set; }
        public List<ContractorCompanyPaymentVm> ContractorCompanyPayments { get; set; }

    }
}