using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using BGB.Data.Entities.Base;

namespace BGB.Data.Entities.Wbpm
{
    [Table("WorkOrder", Schema = "wbpm")]
    public class WorkOrder : BaseEntity
    {
        [Key]
        public int WorkOrderId { get; set; }
        public int ProjectWorkId { get; set; }
        public string LetterNo { get; set; }
        public DateTime? WorkOrderDate { get; set; }
        public string WorkOrderDateB { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? HandOverDate { get; set; }
        public string ScanDocument { get; set; }

        public int? HeadAssistantId { get; set; }
        public int? ConcernedEngineerId { get; set; }
        public int? SectionICId { get; set; }
        public int? BranchClerkId { get; set; }
        public int? OfficerId { get; set; }       


        [ForeignKey(nameof(ProjectWorkId))]
        public virtual ProjectWork ProjectWork { get; set; }

        [ForeignKey(nameof(BranchClerkId))]
        public virtual SignatoryAuthority BranchClerk { get; set; }

        [ForeignKey(nameof(ConcernedEngineerId))]
        public virtual SignatoryAuthority ConcernedEngineer { get; set; }

        [ForeignKey(nameof(HeadAssistantId))]
        public virtual SignatoryAuthority HeadAssistant { get; set; }

        [ForeignKey(nameof(SectionICId))]
        public virtual SignatoryAuthority SectionIC { get; set; }

        [ForeignKey(nameof(OfficerId))]
        public virtual SignatoryAuthority Officers { get; set; }

    }
}
