using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.ComplainCategory")]
    public partial class ComplainCategory
    {
        public ComplainCategory() { ComplainTypes = new HashSet<ComplainType>(); }

        public int ComplainCategoryId { get; set; }

        [StringLength(250)]
        public string ComplainCategoryName { get; set; }

        public string Description { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual ICollection<ComplainType> ComplainTypes { get; set; }
    }
}
