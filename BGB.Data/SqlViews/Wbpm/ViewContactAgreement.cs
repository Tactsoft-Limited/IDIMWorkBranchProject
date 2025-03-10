using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.SqlViews.Wbpm
{
    [Table("ViewContactAgreement", Schema = "wbpm")]
    public class ViewContactAgreement
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectWorkId { get; set; }
        public string DayOfWeek { get; set; } // Assuming this maps to an enum or integer value
        public DateTime? AgreementDate { get; set; }
        public string FirmName { get; set; }
        public string ContactPerson { get; set; }
        public string FirmAddress { get; set; }
        public string ProjectWorkTitle { get; set; }
        public decimal EstimatedCost { get; set; }
        public string EstimatedCostInWord { get; set; }
        public string AddDG_Name { get; set; }
        public string AddDG_Designation { get; set; }
        public string AddDG_Title { get; set; }
        public string AddDG_Address { get; set; }
        public string DDG_Name { get; set; }
        public string DDG_Designation { get; set; }
        public string DDG_Title { get; set; }
        public string DDG_Address { get; set; }
        public string ProjectDirector_Name { get; set; }
        public string ProjectDirector_Designation { get; set; }
        public string ProjectDirector_Title { get; set; }
        public string ProjectDirector_Address { get; set; }
        public string Director_Name { get; set; }
        public string Director_Designation { get; set; }
        public string Director_Title { get; set; }
        public string Director_Address { get; set; }
    }
}
