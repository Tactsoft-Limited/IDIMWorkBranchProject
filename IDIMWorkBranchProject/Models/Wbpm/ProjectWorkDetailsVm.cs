using System;
using System.Collections.Generic;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class ProjectWorkDetailsVm
    {
        public int ADPProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public int ProjectWorkId { get; set; }
        public string ProjectWorkTitleB { get; set; }
        public string ProjectWorkTitle { get; set; }
        public decimal? EstimatedCost { get; set; }
        public string EstimatedCostInWord { get; set; }
        public bool IsNoahCompleted { get; set; }
        public bool IsPerformanceSecuritySubmited { get; set; }
        public bool IsAgreementCompleted { get; set; }
        public bool IsWorkOrderCompleted { get; set; }
        public string FirmNameB { get; set; }
        public DateTime? AgreementDate { get; set; }
        public DateTime? WorkOrderDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? StatusTypeId { get; set; }
        public DateTime? NohaDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? ProjectWorkStatus { get; set; }
        public string Remarks { get; set; }


        public List<ADPReceivePaymentVm> ADPReceivePayments { get; set; }
        public List<ContractorCompanyPaymentVm> ContractorCompanyPayments { get; set; }

    }
}