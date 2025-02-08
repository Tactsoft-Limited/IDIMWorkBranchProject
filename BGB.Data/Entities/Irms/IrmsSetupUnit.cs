using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Irms
{
    [Table("irms.SetupUnit")]
    public partial class IrmsSetupUnit
    {
        public IrmsSetupUnit()
        {
            this.GeneralInformations = new HashSet<GeneralInformation>();
        }

        [Key]
        public int UnitId { get; set; }

        [Required]
        [StringLength(200)]
        public string UnitName { get; set; }

        public int? SubOrganizationId { get; set; }

        [StringLength(250)]
        public string UnitCode { get; set; }

        [StringLength(250)]
        public string UnitFullName { get; set; }

        public int? CoreId { get; set; }

        public int? PlaceId { get; set; }

        [StringLength(150)]
        public string UnitNameB { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }

        public int? Israb { get; set; }

        public int? RegionId { get; set; }

        public int? SectorId { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public int UpdateNo { get; set; }




        #region Navigation Properties

        public virtual ICollection<GeneralInformation> GeneralInformations { get; set; }

        #endregion
    }
}
