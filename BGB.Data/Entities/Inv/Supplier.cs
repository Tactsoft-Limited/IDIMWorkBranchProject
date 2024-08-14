using BGB.Data.Entities.Budget;
using BGB.Data.Entities.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BGB.Data.Entities.Pm;

namespace BGB.Data.Entities.Inv
{
    [Table("inv.Supplier")]
    public partial class Supplier
    {
        public Supplier()
        {
            SecurityDeposits = new HashSet<PmSecurityDeposit>();
        }

        public int SupplierId { get; set; }

        public int ApplicationId { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string CompanyName { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(250)]
        public string ContactPerson { get; set; }

        [StringLength(250)]
        public string ContactNumber { get; set; }

        [StringLength(250)]
        public string ContactEmail { get; set; }

        public bool Enlisted { get; set; }

        public string Comments { get; set; }

        public bool Status { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual Application Application { get; set; }

        public virtual ICollection<PmSecurityDeposit> SecurityDeposits { get; set; }
    }
}
