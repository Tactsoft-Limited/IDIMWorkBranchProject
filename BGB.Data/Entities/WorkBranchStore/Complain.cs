using BGB.Data.Entities.Irms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.Complain")]
    public partial class Complain
    {
        public Complain()
        {
            ComplainDetails = new HashSet<ComplainDetail>();
            ComplainIssues = new HashSet<ComplainIssue>();
            ComplainReceives = new HashSet<ComplainReceive>();
        }

        public int ComplainId { get; set; }

        [Required]
        [StringLength(255)]
        public string ComplainNo { get; set; }

        public DateTime ComplainDate { get; set; }

        public int ComplainTypeId { get; set; }

        public int StructureUnitId { get; set; }

        public int UnitId { get; set; }

        [Column(TypeName = "text")]
        public string ComplainDescription { get; set; }

        public int? ComplainArmyId { get; set; }

        [StringLength(255)]
        public string ComplainPerson { get; set; }

        [StringLength(250)]
        public string ComplainPhoneNo { get; set; }

        public int? ComplainReceiveArmyId { get; set; }

        public int? WorkByArmyId { get; set; }

        [StringLength(255)]
        public string WorkbyOther { get; set; }

        public DateTime? WorkCompletionDate { get; set; }

        public string Remark { get; set; }

        public bool Status { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual GeneralInformation GeneralInformation { get; set; }

        public virtual GeneralInformation GeneralInformation1 { get; set; }

        public virtual GeneralInformation GeneralInformation2 { get; set; }

        public virtual IrmsSetupUnit IrmsSetupUnit { get; set; }

        public virtual ComplainType ComplainType { get; set; }

        public virtual StructureUnit StructureUnit { get; set; }

        public virtual ICollection<ComplainDetail> ComplainDetails { get; set; }

        public virtual ICollection<ComplainIssue> ComplainIssues { get; set; }

        public virtual ICollection<ComplainReceive> ComplainReceives { get; set; }
    }
}
