using System;

namespace BGB.Data.Entities.Base
{
    public class BaseEntity
    {
        /// <summary>
        /// ID of the user who created the record
        /// </summary>
        public int CreatedUser { get; set; }

        /// <summary>
        /// Date and time when the record was created
        /// </summary>
        public DateTime CreatedDateTime { get; set; }

        /// <summary>
        /// ID of the user who last updated the record (nullable)
        /// </summary>
        public int? UpdatedUser { get; set; }

        /// <summary>
        /// Date and time when the record was last updated (nullable)
        /// </summary>
        public DateTime? UpdatedDateTime { get; set; }


        /// <summary>
        /// Number of times the record has been updated
        /// </summary>
        public int UpdateNo { get; set; }
    }
}
