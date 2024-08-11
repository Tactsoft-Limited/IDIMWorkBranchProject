using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IDIMWorkBranchProject.Models.Setup
{
    public class GeneralInformationVm
    {
        public int ArmyId { get; set; }
        [DisplayName("Regiment No.")]
        public string RegimentNo { get; set; }
        [DisplayName("Regiment No. (Bengali)")]
        public string RegimentNoB { get; set; }

        public string Name { get; set; }
        [DisplayName("Name (Bengali)")]
        public string NameB { get; set; }

        [DisplayName("BJO No.")]
        public string BjoNo { get; set; }

        [DisplayName("Fathers Unit")]
        public int? ParentUnitId { get; set; }
        [DisplayName("Unit")]
        public int? UnitId { get; set; }

        [DisplayName("Enrollment Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EnrollmentDate { get; set; }

        public string Age { get; set; }
        public string ServiceDate { get; set; }

        [DisplayName("Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }

        [UIHint("Picture")]
        public string Picture { get; set; }
        [DisplayName("Father's Name")]
        public string FatherName { get; set; }
        [DisplayName("Father's Name (Bengali)")]
        public string FatherNameB { get; set; }
        [DisplayName("Is Father BGB Member")]
        public bool IsFatherBgb { get; set; }
        [DisplayName("Father's Regiment No.")]
        public string FatherRegimentNo { get; set; }
        [DisplayName("Mother Name")]
        public string MotherName { get; set; }
        [DisplayName("Mother Name (Bengali)")]
        public string MotherNameB { get; set; }

        [DisplayName("Mobile No (Personal)")]
        public string Phone1 { get; set; }
        [DisplayName("Mobile No. (NOK)")]
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        [DisplayName("NID / Voter Id")]
        public string NationalId { get; set; }
        [DisplayName("Freedom Fighter")]
        public bool IsFreedomFighter { get; set; }
        [DisplayName("Rank")]
        public int? RankId { get; set; }
        [DisplayName("Trade")]
        public int? TradeId { get; set; }
        [DisplayName("Blood Group")]
        public int? BloodGroupId { get; set; }
        [DisplayName("Medical Category")]
        public int? MedicalCategoryId { get; set; }
        [DisplayName("Batch")]
        public int? BatchId { get; set; }
        [DisplayName("Education")]
        public int? EducationId { get; set; }

        [DisplayName("Presen Rank")]
        [DataType(DataType.Date)]
        public DateTime? PresentRankDate { get; set; }
        public int? EmploymentTypeIdR { get; set; }
        public int? ServiceTermsIdR { get; set; }
        [DisplayName("Core")]
        public int? CoreId { get; set; }
        public int? PoliceVarificationStatusR { get; set; }

        [DisplayName("Present District")]
        public int? PresentDistrictId { get; set; }
        [DisplayName("Marital Status")]
        public int? MaritalStatusId { get; set; }

        [DisplayName("Marriage Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MarriageDate { get; set; }
        public int? PreviousBasicR { get; set; }
        [DisplayName("Current Basic")]
        public int? CurrentBasic { get; set; }

        [DisplayName("Next Increment Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NextIncrementDate { get; set; }

        [DisplayName("Expected Date Retirement")]
        [DataType(DataType.Date)]
        public DateTime? ExpectedDateRetirement { get; set; }
        [DisplayName("Pay Grade date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PayGradeDate { get; set; }
        [DisplayName("Religion")]
        public int? ReligionId { get; set; }
        [DisplayName("BJO No.")]
        public string BjoNoB { get; set; }
        [DisplayName("RDO No.")]
        public string RdoNo { get; set; }
        [DisplayName("RDO No. (Bengali)")]
        public string RdoNoB { get; set; }
        [DisplayName("Status")]
        public bool IsActiveStatus { get; set; }

        [DisplayName("Mission Status")]
        public string MissionStatus { get; set; }
        public string ClassR { get; set; }
        public string CastR { get; set; }
        [DisplayName("GPF No.")]
        public string GpfNo { get; set; }
        public DateTime? PostingDate { get; set; }

        [DisplayName("Injured in Liberation war")]
        public bool IsInjuredLiberation { get; set; }
        [DisplayName("Civil")]
        public bool IsCivil { get; set; }
        [DisplayName("CS")]
        public bool IsCs { get; set; }
        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }

        [DisplayName("Rank")]
        public string RankName { get; set; }
        [DisplayName("Trade")]
        public string TradeName { get; set; }
        [DisplayName("Batch")]
        public string BatchName { get; set; }
        [DisplayName("Fathers Unit Name")]
        public string ParentUnitName { get; set; }
        [DisplayName("Unit Name")]
        public string UnitName { get; set; }
        [DisplayName("Medical Category")]
        public string MedicalCategoryName { get; set; }
        [DisplayName("Blood Group Category")]
        public string BloodGroupName { get; set; }
        [DisplayName("Education")]
        public string EducationName { get; set; }
        [DisplayName("Core")]
        public string CoreName { get; set; }
        [DisplayName("Present District")]
        public string PresentDistrictName { get; set; }
        [DisplayName("Marital Status")]
        public string MaritalStatusName { get; set; }
        [DisplayName("Religion")]
        public string ReligionName { get; set; }
    }
}