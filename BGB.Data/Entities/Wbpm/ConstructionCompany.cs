using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BGB.Data.Entities.Base;

namespace BGB.Data.Entities.Wbpm
{
	[Table("ConstructionCompany", Schema = "wbpm")]
    public class ConstructionCompany : BaseEntity
    {


        [Key]
        public int ConstructionCompanyId { get; set; }

        public string FirmName { get; set; }
        public string FirmNameB { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonB { get; set; }
        public string ContactPhone { get; set; }
        public string Email { get; set; }
        public string FirmAddress { get; set; }
        public string FirmAddressB { get; set; }


        #region  Navigation Property
        public virtual ICollection<ProjectWork> ProjectWorks { get; set; }
        #endregion
    }
}
