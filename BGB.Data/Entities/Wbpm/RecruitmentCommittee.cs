using BGB.Data.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Wbpm
{
    [Table("RecruitmentCommittee",Schema ="wbpm")]
    public class RecruitmentCommittee : BaseEntity
    {
        public RecruitmentCommittee()
        {
            TenderEvaluationCommitteeAddDG = new HashSet<TenderEvaluationCommittee>();
            TenderEvaluationCommitteeDDG = new HashSet<TenderEvaluationCommittee>();
            TenderEvaluationCommitteeProjectDirector = new HashSet<TenderEvaluationCommittee>();
            TenderEvaluationCommitteeDirector = new HashSet<TenderEvaluationCommittee>();
        }

        [Key]
        public int RecruitmentCommitteeId { get; set; }
        public string Name { get; set; }
        public string NameB { get; set; }
        public string Designation { get; set; }
        public string DesignationB { get; set; }
        public string Title { get; set; }
        public string TitleB { get; set; }
        public string Address { get; set; }
        public string AddressB { get; set; }



        public virtual ICollection<TenderEvaluationCommittee> TenderEvaluationCommitteeAddDG { get; set; }
        public virtual ICollection<TenderEvaluationCommittee> TenderEvaluationCommitteeDDG { get; set; }
        public virtual ICollection<TenderEvaluationCommittee> TenderEvaluationCommitteeProjectDirector { get; set; }
        public virtual ICollection<TenderEvaluationCommittee> TenderEvaluationCommitteeDirector { get; set; }
    }
}
