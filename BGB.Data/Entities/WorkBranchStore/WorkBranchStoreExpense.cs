using BGB.Data.Entities.Inv;
using BGB.Data.Entities.Irms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.Expense")]
    public partial class WorkBranchStoreExpense
    {
        public WorkBranchStoreExpense() { ExpenseDetail2 = new HashSet<WorkBranchStoreExpenseDetail>(); }

        [Key]
        public int ExpenseId { get; set; }

        public int StoreId { get; set; }

        [StringLength(50)]
        public string SerialNo { get; set; }

        public DateTime ExpenseDate { get; set; }

        [StringLength(50)]
        public string ReferenceNo { get; set; }

        public int UnitId { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        [StringLength(250)]
        public string Remarks { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual Store Store { get; set; }

        public virtual IrmsSetupUnit IrmsSetupUnit { get; set; }

        public virtual ICollection<WorkBranchStoreExpenseDetail> ExpenseDetail2 { get; set; }
    }
}
