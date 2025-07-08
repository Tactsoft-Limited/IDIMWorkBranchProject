using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGB.Data.SqlViews.Wbpm.Revenue
{
    [Table("ViewRevenueWorkOrder", Schema = "wbpm")]
    public class ViewRevenueWorkOrder
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RevenueId { get; set; }
        public DateTime? RevenueWorkOrderDate { get; set; }
        public DateTime? RevenueWorkOrderStartDate { get; set; }
        public DateTime? RevenueWorkOrderEndDate { get; set; }

        public string RevenueWorkOrderLetterNo { get; set; }

        public string WorkTitleB { get; set; }
        public decimal EstimateCost { get; set; }
        public string EstimateCostInWordB { get; set; }

        public string FirmNameB { get; set; }
        public string OwnerDesignationB { get; set; }
        public string OwnerNameB { get; set; }
        public string FirmAddressB { get; set; }

        public DateTime? RevenueNohaDate { get; set; }
        public DateTime? AgreementDate { get; set; }
    }
}
