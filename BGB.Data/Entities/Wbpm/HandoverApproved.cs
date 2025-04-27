using BGB.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGB.Data.Entities.Wbpm
{
    [Table("HandoverApproved", Schema = "wbpm")]
    public class HandoverApproved:BaseEntity
    {
        [Key]
        public int HandoverApprovedId { get; set; }
        public int ProjectWorkId { get; set; }
        public string LetterNo { get; set; }
        public string QuoateOne { get; set; }
        public string QuoateTwo { get; set; }
        public DateTime? QuoateOneDate { get; set; }
        public DateTime? QuoateTwoDate { get; set; }
        public string Description { get; set; }

        public int? HeadAssistantId { get; set; }
        public int? ConcernedEngineerId { get; set; }
        public int? SectionICId { get; set; }
        public int? BranchClerkId { get; set; }

    }
}
