using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.SqlViews.Wbpm
{
    [Table("ViewWorkOrder", Schema = "wbpm")]
    public class ViewWorkOrder
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectWorkId { get; set; }
        public string ProjectWorkTitleB { get; set; }
        public decimal EstimatedCost { get; set; }
        public string EstimatedCostInWordBangla { get; set; }
        public string NohaLetterNo { get; set; }
        public DateTime? NohaDate { get; set; }
        public string WorkOrderLetterNo { get; set; }
        public DateTime? WorkOrderDate { get; set; }
        public string WorkOrderDateB { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string FirmNameB { get; set; }
        public string OwnerNameB { get; set; }
        public string OwnerDesignationB { get; set; }
        public string FirmAddressB { get; set; }
        public DateTime? AgreementDate { get; set; }
        public string AuthorityNameB { get; set; }
        public string Recruitment { get; set; }
        public string DesignationB { get; set; }
        public string onThePartOf { get; set; }
    }
}
