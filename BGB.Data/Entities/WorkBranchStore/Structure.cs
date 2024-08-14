using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.Structure")]
    public partial class Structure
    {
        public Structure() { StructureUnits = new HashSet<StructureUnit>(); }

        public int StructureId { get; set; }

        public int AreaId { get; set; }

        [Required]
        [StringLength(255)]
        public string StructureType { get; set; }

        [Required]
        [StringLength(255)]
        public string StructureName { get; set; }

        public int? NoStored { get; set; }

        public string Description { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual Area Area { get; set; }

        public virtual ICollection<StructureUnit> StructureUnits { get; set; }
    }
}
