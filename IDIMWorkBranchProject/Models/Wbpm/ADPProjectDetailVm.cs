using System;
using System.Collections.Generic;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class ADPProjectDetailVm
    {
        public ADPProjectVm ADPProjectDetail { get; set; }
        public List<ProjectDirectorVm> ProjectDirector { get; set; }
        public List<FinancialYearAllocationVm> FinancialYearAllocation { get; set; }
        public List<FiscalYearExpenseVm> FiscalYearExpense { get; set; }
        public List<FormalMeetingVm> FormalMeeting { get; set; }
        public List<ProjectWorkVm> ProjectWorks { get; set; }


    }
}