using BGB.Data.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Wbpm
{
    [Table("Revenue", Schema = "wbpm")]
    public class Revenue:BaseEntity
    {
        public int RevenueId { get; set; }
        public int FisCalYearId { get; set; }
        public string WorkTitle { get; set; }
        public string WorkTitleB { get; set; }
        public decimal EstimateCost { get; set; }
        public string EstimateCostInWord { get; set; }
        public string EstimateCostInWordB { get; set; }
        public string Remarks { get; set; }
        public bool IsFurnitureIncluded { get; set; }
        public bool IsNoahCompleted { get; set; }
        public bool IsPerformanceSecuritySubmited { get; set; }
        public bool IsAgreementCompleted { get; set; }
        public bool IsWorkOrderComplited{ get; set; }



    }
}
