using BGB.Data.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Wbpm
{
    [Table("RecruitmentCommittee", Schema = "wbpm")]
    public class RecruitmentCommittee : BaseEntity
    {
        public RecruitmentCommittee()
        {
            AddDGs = new HashSet<ContractAgreement>();
            DDGs = new HashSet<ContractAgreement>();
            ProjectDirectors = new HashSet<ContractAgreement>();
            Directors = new HashSet<ContractAgreement>();
        }

        public int RecruitmentCommitteeId { get; set; }
        public string Name { get; set; }
        public string NameB { get; set; }
        public string Designation { get; set; }
        public string DesignationB { get; set; }
        public string Title { get; set; }
        public string TitleB { get; set; }
        public string Address { get; set; }
        public string AddressB { get; set; }


        // Navigation Properties for Recruitment Committee in other tables
        public virtual ICollection<ContractAgreement> AddDGs { get; set; }
        public virtual ICollection<ContractAgreement> DDGs { get; set; }
        public virtual ICollection<ContractAgreement> ProjectDirectors { get; set; }
        public virtual ICollection<ContractAgreement> Directors { get; set; }
    }
}
