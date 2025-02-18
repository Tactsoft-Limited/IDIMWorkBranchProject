using BGB.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Wbpm
{
    [Table("TenderEvaluationCommittee", Schema = "wbpm")]
    public class TenderEvaluationCommittee : BaseEntity
    {
        [Key]
        public int TenderEvaluationCommitteeId { get; set; }
        public int? ADPProjectId {  get; set; }
        public int? AddDG {  get; set; }
        public int? DDG  { get; set; }
        public int? ProjectDirector {  get; set; }
        public int? Director {  get; set; }


        #region navigation property
        [ForeignKey(nameof(ADPProjectId))]
        public virtual ADPProject ADPProject { get; set; }

        [ForeignKey(nameof(AddDG))]
        public virtual RecruitmentCommittee RecruitmentCommitteeAddDG { get; set; }

        [ForeignKey(nameof(DDG))]
        public virtual RecruitmentCommittee RecruitmentCommitteeDDG { get; set; }

        [ForeignKey(nameof(ProjectDirector))]
        public virtual RecruitmentCommittee RecruitmentCommitteeProjectDirector { get; set; }

        [ForeignKey(nameof(Director))]
        public virtual RecruitmentCommittee RecruitmentCommitteeDirector { get; set; }

        #endregion
    }
}
