using BGB.Data.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Wbpm
{
    [Table("RecruitmentCommittee",Schema ="wbpm")]
    public class RecruitmentCommittee : BaseEntity
    {
        public int RecruitmentCommitteeId { get; set; }
        public string Name { get; set; }
        public string NameB { get; set; }
        public string Designation { get; set; }
        public string DesignationB { get; set; }
        public string Title { get; set; }
        public string TitleB { get; set; }
        public string Address { get; set; }
        public string AddressB { get; set; }
    }
}
