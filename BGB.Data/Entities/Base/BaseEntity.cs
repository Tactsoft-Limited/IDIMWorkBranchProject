using System;

namespace BGB.Data.Entities.Base
{
    public class BaseEntity
    {
        public int? CreatedUser { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int? UpdateNo { get; set; }
    }

}