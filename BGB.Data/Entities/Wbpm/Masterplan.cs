using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using BGB.Data.Entities.Base;

namespace BGB.Data.Entities.Wbpm
{
    [Table("Masterplan", Schema = "wbpm")]
    public class Masterplan : BaseEntity
    {


        [Key]
        public int MasterplanId { get; set; }

        public int ADPProjectId { get; set; }
        public string Title { get; set; }
        public string Document { get; set; }



        #region Navigation Property
        [ForeignKey(nameof(ADPProjectId))]
        public virtual ADPProject ADPProject { get; set; }
        #endregion
    }
}
