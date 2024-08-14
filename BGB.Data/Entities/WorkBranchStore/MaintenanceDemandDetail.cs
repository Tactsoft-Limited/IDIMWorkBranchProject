using BGB.Data.Entities.Budget;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.MaintenanceDemandDetail")]
    public partial class MaintenanceDemandDetail
    {
        public int MaintenanceDemandDetailId { get; set; }

        public int MaintenanceDemandId { get; set; }

        public int AccountCodeId { get; set; }

        public double Amount { get; set; }

        public string Remark { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual AccountCode AccountCode { get; set; }

        public virtual MaintenanceDemand MaintenanceDemand { get; set; }
    }
}
