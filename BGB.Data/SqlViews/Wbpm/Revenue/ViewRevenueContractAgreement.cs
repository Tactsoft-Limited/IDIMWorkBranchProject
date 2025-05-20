using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BGB.Data.SqlViews.Wbpm.Revenue
{
    [Table("ViewRevenueContractAgreement", Schema = "wbpm")]
    public class ViewRevenueContractAgreement 
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        // Contract Agreement Info
        public int ContractAgreementId { get; set; }
        public int RevenueId { get; set; }
        public int ConstructionCompanyId { get; set; }
        public DateTime AgreementDate { get; set; }
        public int? AddDGId { get; set; }
        public int? DDGId { get; set; }
        public int? ProjectDirectorId { get; set; }
        public int? DirectorId { get; set; }

        // Construction Company Info
        public string FirmName { get; set; }
        public string ContactPerson { get; set; }
        public string FirmAddress { get; set; }

        // Revenue Info
        public string WorkTitle { get; set; }
        public decimal EstimateCost { get; set; }
        public string EstimateCostInWord { get; set; }

        // Additional Director General
        public string AddDG_Name { get; set; }
        public string AddDG_Designation { get; set; }
        public string AddDG_Title { get; set; }
        public string AddDG_Address { get; set; }

        // Deputy Director General
        public string DDG_Name { get; set; }
        public string DDG_Designation { get; set; }
        public string DDG_Title { get; set; }
        public string DDG_Address { get; set; }

        // Project Director
        public string ProjectDirector_Name { get; set; }
        public string ProjectDirector_Designation { get; set; }
        public string ProjectDirector_Title { get; set; }
        public string ProjectDirector_Address { get; set; }

        // Director
        public string Director_Name { get; set; }
        public string Director_Designation { get; set; }
        public string Director_Title { get; set; }
        public string Director_Address { get; set; }
    }
}
