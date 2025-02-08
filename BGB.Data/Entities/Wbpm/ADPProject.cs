using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using BGB.Data.Entities.Base;

namespace BGB.Data.Entities.Wbpm
{
    [Table("ADPProject", Schema = "wbpm")]
    public class ADPProject : BaseEntity
    {
        public ADPProject()
        {
            Masterplans = new HashSet<Masterplan>();
            FinancialYearAllocations = new HashSet<FinancialYearAllocation>();
            FiscalYearExpenses = new HashSet<FiscalYearExpense>();
            FormalMeetings = new HashSet<FormalMeeting>();
            ProjectWorks = new HashSet<ProjectWork>();
            ProjectDirectors = new HashSet<ProjectDirector>();
        }


        [Key]
        public int ADPProjectId { get; set; }

        public string ProjectTitle { get; set; }
        public string MinistryDepartment { get; set; }
        public decimal EstimatedExpenses { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public int NoOfWork { get; set; }
        public double FinancialProgress { get; set; }
        public double PhysicalProgress { get; set; }
        public string Remarks { get; set; }




        #region Navigation Property
        public virtual ICollection<Masterplan> Masterplans { get; set; }
        public virtual ICollection<FinancialYearAllocation> FinancialYearAllocations { get; set; }
        public virtual ICollection<FiscalYearExpense> FiscalYearExpenses { get; set; }
        public virtual ICollection<FormalMeeting> FormalMeetings { get; set; }
        public virtual ICollection<ProjectWork> ProjectWorks { get; set; }
        public virtual ICollection<ProjectDirector> ProjectDirectors { get; set; }
        #endregion
    }
}
