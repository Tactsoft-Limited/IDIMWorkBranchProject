using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BGB.Data.Entities.Base;

namespace BGB.Data.Entities.Wbpm
{
    [Table("RevenueContractAgreement", Schema = "wbpm")]
    public class RevenueContractAgreement:BaseEntity
    {
        [Key]
        public int ContractAgreementId { get; set; }
        public int RevenueId { get; set; }
        public int ConstructionCompanyId { get; set; }
        public DateTime? AgreementDate { get; set; }
        public int? AddDGId { get; set; }
        public int? DDGId { get; set; }
        public int? ProjectDirectorId { get; set; }
        public int? DirectorId { get; set; }
        public string ScanDocument { get; set; }

        // Navigation Properties (Foreign Key Relationships)
        [ForeignKey(nameof(RevenueId))]
        public virtual Revenue Revenue { get; set; }

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
