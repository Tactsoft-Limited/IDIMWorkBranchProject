using BGB.Data.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Wbpm
{
    [Table("ProjectSignatoryAuthority", Schema = "wbpm")]
    public class ProjectSignatoryAuthority : BaseEntity
    {
        public int ProjectSignatoryAuthorityId { get; set; }
        public int? ADPProjectId { get; set; }
        public int? HeadAssistant { get; set; }
        public int? ConcernedEngineer { get; set; }
        public int? SectionICT { get; set; }
        public int? BranchClerk { get; set; }

        // You may want to include navigation properties for the foreign keys if using Entity Framework

        [ForeignKey(nameof(ADPProjectId))]
        public virtual ADPProject ADPProject { get; set; }
        [ForeignKey(nameof(BranchClerk))]
        public virtual SignatoryAuthority BranchClerkSignatoryAuthority { get; set; }
        [ForeignKey(nameof(ConcernedEngineer))]
        public virtual SignatoryAuthority ConcernedEngineerSignatoryAuthority { get; set; }
        [ForeignKey(nameof(HeadAssistant))]
        public virtual SignatoryAuthority HeadAssistantSignatoryAuthority { get; set; }
        [ForeignKey(nameof(SectionICT))]
        public virtual SignatoryAuthority SectionICTSignatoryAuthority { get; set; }
    }
}
