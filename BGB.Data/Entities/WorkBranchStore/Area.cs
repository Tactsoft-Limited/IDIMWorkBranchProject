using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.Area")]
    public partial class Area
    {
        public Area() { Structures = new HashSet<Structure>(); }

        [Key]
        public int AreaId { get; set; }

        [StringLength(250)]
        public string AreaName { get; set; }

        public string Description { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual ICollection<Structure> Structures { get; set; }
    }
}
