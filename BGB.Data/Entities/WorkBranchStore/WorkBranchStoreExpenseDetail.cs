using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.ExpenseDetail")]
    public partial class WorkBranchStoreExpenseDetail
    {
        [Key]
        public int ExpenseDetailId { get; set; }

        public int ExpenseId { get; set; }

        public int ItemId { get; set; }

        public double Quantity { get; set; }

        [StringLength(150)]
        public string Remarks { get; set; }

        public virtual WorkBranchStoreExpense Expense2 { get; set; }

        public virtual WorkBranchStoreItem Item2 { get; set; }
    }
}
