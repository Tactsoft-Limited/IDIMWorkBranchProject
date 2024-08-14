using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.ComplainType")]
    public partial class ComplainType
    {
        public ComplainType() { Complains = new HashSet<Complain>(); }

        public int ComplainTypeId { get; set; }

        public int? ComplainCategoryId { get; set; }

        [StringLength(250)]
        public string ComplainTypeName { get; set; }

        public string Description { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual ICollection<Complain> Complains { get; set; }

        public virtual ComplainCategory ComplainCategory { get; set; }
    }
}
