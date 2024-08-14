using BGB.Data.Entities.Irms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.MaintenanceDemand")]
    public partial class MaintenanceDemand
    {
        public MaintenanceDemand()
        {
            MaintenanceDemandDetails = new HashSet<MaintenanceDemandDetail>();
            MaintenanceDemandIssues = new HashSet<MaintenanceDemandIssue>();
        }

        public int MaintenanceDemandId { get; set; }

        [Required]
        [StringLength(255)]
        public string MaintenanceDemandNo { get; set; }

        public DateTime MaintenanceDemandDate { get; set; }

        public int UnitId { get; set; }

        [StringLength(255)]
        public string AuthorizedBy { get; set; }

        public string Remark { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual IrmsSetupUnit IrmsSetupUnit { get; set; }

        public virtual ICollection<MaintenanceDemandDetail> MaintenanceDemandDetails { get; set; }

        public virtual ICollection<MaintenanceDemandIssue> MaintenanceDemandIssues { get; set; }
    }
}
