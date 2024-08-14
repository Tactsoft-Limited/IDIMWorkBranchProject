using BGB.Data.Entities.Budget;
using BGB.Data.Entities.Irms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.Bop")]
    public partial class Bop
    {
        public Bop()
        {
            UnitBudgets = new HashSet<UnitBudget>();
            UnitBudget2 = new HashSet<WorkBranchStoreUnitBudget>();
        }

        [Key]
        public int BopId { get; set; }

        public int UnitId { get; set; }

        [Required]
        [StringLength(255)]
        public string BopName { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual ICollection<UnitBudget> UnitBudgets { get; set; }

        public virtual IrmsSetupUnit IrmsSetupUnit { get; set; }

        public virtual ICollection<WorkBranchStoreUnitBudget> UnitBudget2 { get; set; }
    }
}
