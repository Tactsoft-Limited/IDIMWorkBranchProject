using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.Consultant")]
    public partial class Consultant
    {
        public Consultant()
        {
            ConsultancyActivities = new HashSet<ConsultancyActivity>();
            ConsultancyFees = new HashSet<ConsultancyFee>();
        }

        public int ConsultantId { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(250)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }

        public virtual ICollection<ConsultancyActivity> ConsultancyActivities { get; set; }

        public virtual ICollection<ConsultancyFee> ConsultancyFees { get; set; }
    }
}
