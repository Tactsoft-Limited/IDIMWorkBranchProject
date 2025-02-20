using BGB.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGB.Data.Entities.Wbpm
{

    [Table("ProjectSignatoryAuthority", Schema = "wbpm")]
    public class ProjectSignatoryAuthority : BaseEntity
    {
        [Key]
        public int ProjectSignatoryAuthorityId { get; set; }    

        public int? ADPProjectId { get; set; }

        public int? HeadAssistant {  get; set; }     

        public int? ConcernedEngineer {  get; set; }

        public int? SectionICT { get; set; }

        public int? BranchClerk { get; set; }

        #region navigation property
        [ForeignKey(nameof(ADPProjectId))]
        public virtual ADPProject ADPProject { get; set; }

        [ForeignKey(nameof(HeadAssistant))]
        public virtual SignatoryAuthority signatureAuthorityHeadAssistant { get; set; }

        [ForeignKey(nameof(ConcernedEngineer))]
        public virtual SignatoryAuthority signatureAuthorityConcernedEngineer { get; set; }

        [ForeignKey(nameof(SectionICT))]
        public virtual SignatoryAuthority signatureAuthoritySectionICT { get; set; }

        [ForeignKey(nameof(BranchClerk))]
        public virtual SignatoryAuthority signatureAuthorityBranchClerk { get; set; }

        #endregion

    }
}
