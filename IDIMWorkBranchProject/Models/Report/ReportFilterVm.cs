using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.Report
{
    public class ReportFilterVm
    {
        public ReportFilterVm()
        {
            FiscalYearDropdown = new List<SelectListItem>();
            SubProjectDropdown = new List<SelectListItem>();
            ConstructionFirmDropdown = new List<SelectListItem>();
            ConsultantDropdown = new List<SelectListItem>();
        }

        [DisplayName("Fiscal Year")]
        public int? FiscalYearId { get; set; }
        [DisplayName("Project")]
        public int? SubProjectId { get; set; }

        [DisplayName("Sub-Project Title")]
        public string SubProjectTitle { get; set; }

        [DisplayName("Construction Firm")]
        public int? ConstructionFirmId { get; set; }

        [DisplayName("Letter No")]
        public string Letterno { get; set; }

        [DisplayName("Consultant")]
        public int? ConsultantId { get; set; }

        public IEnumerable<SelectListItem> FiscalYearDropdown { get; set; }
        public IEnumerable<SelectListItem> SubProjectDropdown { get; set; }
        public IEnumerable<SelectListItem> ConstructionFirmDropdown { get; set; }
        public IEnumerable<SelectListItem> ConsultantDropdown { get; set; }
    }
}