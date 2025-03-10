using BGB.Data.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Wbpm
{
    [Table("ContractAgreement", Schema = "wbpm")]
    public class ContractAgreement : BaseEntity
    {
        [Key]
        public int ContractAgreementId { get; set; }
        public int ProjectWorkId { get; set; }
        public int ConstructionCompanyId { get; set; }
        public DateTime? AgreementDate { get; set; }
        public int? AddDGId { get; set; }
        public int? DDGId { get; set; }
        public int? ProjectDirectorId { get; set; }
        public int? DirectorId { get; set; }
        public string ScanDocument { get; set; }




        // Navigation Properties (Foreign Key Relationships)
        [ForeignKey(nameof(ProjectWorkId))]
        public ProjectWork ProjectWork { get; set; }

        [ForeignKey(nameof(ConstructionCompanyId))]
        public virtual ConstructionCompany ConstructionCompany { get; set; }

        [ForeignKey(nameof(AddDGId))]
        public virtual RecruitmentCommittee AddDG { get; set; }

        [ForeignKey(nameof(DDGId))]
        public virtual RecruitmentCommittee DDG { get; set; }

        [ForeignKey(nameof(ProjectDirectorId))]
        public virtual RecruitmentCommittee ProjectDirector { get; set; }

        [ForeignKey(nameof(DirectorId))]
        public virtual RecruitmentCommittee Director { get; set; }
    }
}
