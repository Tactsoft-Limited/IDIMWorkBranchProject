using BGB.Data.Entities.Base;
using BGB.Data.Entities.Pm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGB.Data.Entities.Wbpm
{
    [Table("ContractAgreement", Schema = "wbpm")]
    public class ContractAgreement : BaseEntity
    {
        [Key]
        public int ContractAgreementId { get; set; }
        public int ProjectWorkId { get; set; }
        public string ContractDay { get; set; }
        public DateTime ContractDate { get; set; }
        public int ConstructionCompanyId { get; set; }
        public decimal AgreementCost { get; set; }
        public string AgreementCostInWord { get; set; }

        #region navigation
        [ForeignKey(nameof(ProjectWorkId))]
        public virtual ProjectWork ProjectWorks { get; set; }

        [ForeignKey(nameof(ConstructionCompanyId))]
        public virtual ConstructionFirm ConstructionCompanies { get; set; }
        #endregion
    }
}
