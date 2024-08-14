using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.StructureUnit")]
    public partial class StructureUnit
    {
        public StructureUnit() { Complains = new HashSet<Complain>(); }

        public int StructureUnitId { get; set; }

        public int StructureId { get; set; }

        [Required]
        [StringLength(250)]
        public string StructureUnitName { get; set; }

        public string Description { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual ICollection<Complain> Complains { get; set; }

        public virtual Structure Structure { get; set; }
    }
}
