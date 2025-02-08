using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Irms
{
    [Table("irms.SetupLocation")]
    public partial class IrmsSetupLocation
    {
        public IrmsSetupLocation()
        {

        }

        [Key]
        public int LocationId { get; set; }

        [Required]
        [StringLength(50)]
        public string LocationName { get; set; }

        [StringLength(100)]
        public string LocationNameB { get; set; }

        public bool IsHillTrack { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

    }
}
