using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.ConstructionFirm")]
    public partial class ConstructionFirm
    {
        public ConstructionFirm() { SubProjects = new HashSet<SubProject>(); }

        public int ConstructionFirmId { get; set; }

        [Required]
        [StringLength(255)]
        public string ConstructionFirmName { get; set; }

        [StringLength(255)]
        public string ContactPerson { get; set; }

        [StringLength(255)]
        public string ContactPhone { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual ICollection<SubProject> SubProjects { get; set; }
    }
}
