using BGB.Data.Entities.Irms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.SubProject")]
    public partial class SubProject
    {
        public SubProject()
        {
            BillPayments = new HashSet<BillPayment>();
            ConsultancyActivities = new HashSet<ConsultancyActivity>();
            ConsultancyFees = new HashSet<ConsultancyFee>();
            ProjectExtends = new HashSet<ProjectExtend>();
            ProjectProblems = new HashSet<ProjectProblem>();
            ProjectStatus = new HashSet<ProjectStatus>();
            SecurityDeposit1 = new HashSet<PmSecurityDeposit>();
            SubProjectDetails = new HashSet<SubProjectDetail>();
        }
        [Key]
        public int SubProjectId { get; set; }

        public int ProjectId { get; set; }

        [StringLength(255)]
        public string SubProjectTitle { get; set; }

        public int UnitId { get; set; }

        public string Description { get; set; }

        public int ConstructionFirmId { get; set; }

        public double AgreementCost { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Status { get; set; }

        public DateTime? HandOverDate { get; set; }

        public string Remark { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual IrmsSetupUnit IrmsSetupUnit { get; set; }

        public virtual ICollection<BillPayment> BillPayments { get; set; }

        public virtual ConstructionFirm ConstructionFirm { get; set; }

        public virtual ICollection<ConsultancyActivity> ConsultancyActivities { get; set; }

        public virtual ICollection<ConsultancyFee> ConsultancyFees { get; set; }

        public virtual Project Project { get; set; }

        public virtual ICollection<ProjectExtend> ProjectExtends { get; set; }

        public virtual ICollection<ProjectProblem> ProjectProblems { get; set; }

        public virtual ICollection<ProjectStatus> ProjectStatus { get; set; }

        public virtual ICollection<PmSecurityDeposit> SecurityDeposit1 { get; set; }

        public virtual ICollection<SubProjectDetail> SubProjectDetails { get; set; }
    }
}
