using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Admin
{
    [Table("admin.SlideImages")]
    public partial class SlideImage
    {
        [Key]
        public int ImageId { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string AlternateText { get; set; }

        public int? Priority { get; set; }

        public string Description { get; set; }
    }
}
