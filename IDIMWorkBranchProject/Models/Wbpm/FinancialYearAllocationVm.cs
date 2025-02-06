using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.Wbpm;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    [Validator(typeof(FinancialYearAllocationVmValidator))]
    public class FinancialYearAllocationVm
    {
        public FinancialYearAllocationVm()
        {
            FiscalYearDropdown = new List<SelectListItem>();
        }

        public int FinancialYearAllocationId { get; set; }

        [DisplayName("প্রকল্প আইডি")]
        public int ADPProjectId { get; set; }

        [DisplayName("প্রকল্পের শিরোনাম")]
        public string ProjectTitle { get; set; }

        [DisplayName("আর্থবছর")]
        public int FiscalYearId { get; set; }

        [DisplayName("আর্থবছর")]
        public string FiscalYearDescription { get; set; }

        [DisplayName("মোট বরাদ্দ")]
        public double TotalAllocation { get; set; }

        [DisplayName("এডিপি বরাদ্দ")]
        public double ADPAllocation { get; set; }

        [DisplayName("আরএডিপি বরাদ্দ")]
        public double RADPAllocation { get; set; }


        public IEnumerable<SelectListItem> FiscalYearDropdown { get; set; }

    }
}