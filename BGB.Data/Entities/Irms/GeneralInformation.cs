using BGB.Data.Entities.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Irms
{
    [Table("irms.GeneralInformation")]
    public partial class GeneralInformation
    {
        public GeneralInformation()
        {
            Users = new HashSet<User>();
        }

        [Key]
        public int ArmyId { get; set; }

        [Required]
        [StringLength(20)]
        public string RegimentNo { get; set; }

        [StringLength(50)]
        public string RegimentNoB { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string NameB { get; set; }

        [StringLength(20)]
        public string BjoNo { get; set; }

        public int? ParentUnitId { get; set; }

        public int? UnitId { get; set; }

        public DateTime? EnrollmentDate { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Picture { get; set; }

        [StringLength(50)]
        public string FatherName { get; set; }

        [StringLength(50)]
        public string FatherNameB { get; set; }

        [StringLength(20)]
        public string FatherRegimentNo { get; set; }

        [StringLength(50)]
        public string MotherNameB { get; set; }

        [StringLength(50)]
        public string MotherName { get; set; }

        [StringLength(50)]
        public string Phone1 { get; set; }

        [StringLength(50)]
        public string Phone2 { get; set; }

        [StringLength(50)]
        public string Phone3 { get; set; }

        [StringLength(30)]
        public string NationalId { get; set; }

        public bool IsFreedomFighter { get; set; }

        public int? RankId { get; set; }

        public int? TradeId { get; set; }

        public int? BloodGroupId { get; set; }

        public int? MedicalCategoryId { get; set; }

        public int? BatchId { get; set; }

        public int? EducationId { get; set; }

        public DateTime? PresentRankDate { get; set; }

        public int? EmploymentTypeIdR { get; set; }

        public int? ServiceTermsIdR { get; set; }

        public int? CoreId { get; set; }

        public int? PoliceVarificationStatusR { get; set; }

        public int? PresentDistrictId { get; set; }

        public int? MaritalStatusId { get; set; }

        public DateTime? MarriageDate { get; set; }

        public int? PreviousBasicR { get; set; }

        public int? CurrentBasic { get; set; }

        public DateTime? NextIncrementDate { get; set; }

        public DateTime? ExpectedRetirementDate { get; set; }

        public DateTime? PayGradeDate { get; set; }

        public int? ReligionId { get; set; }

        [StringLength(50)]
        public string BjoNoB { get; set; }

        [StringLength(20)]
        public string RdoNo { get; set; }

        [StringLength(50)]
        public string RdoNoB { get; set; }

        public int? IsActiveStatus { get; set; }

        [StringLength(10)]
        public string MissionStatus { get; set; }

        [StringLength(50)]
        public string ClassR { get; set; }

        [StringLength(50)]
        public string CastR { get; set; }

        [StringLength(10)]
        public string GpfNo { get; set; }

        public DateTime? PostingDate { get; set; }

        public bool IsInjuredLiberation { get; set; }

        public bool IsCivil { get; set; }

        public bool IsCs { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        [StringLength(40)]
        public string JobStatus { get; set; }

        [StringLength(1000)]
        public string Remarks { get; set; }

        public bool? IsTribal { get; set; }

        public bool? IsCSTTribal { get; set; }

        public int? Gender { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual IrmsSetupUnit IrmsSetupUnit { get; set; }
    }
}
