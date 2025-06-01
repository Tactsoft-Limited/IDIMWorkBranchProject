using BGB.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Wbpm
{
    [Table("ConstructionCompany", Schema = "wbpm")]
    public class ConstructionCompany : BaseEntity
    {
        public ConstructionCompany()
        {
            ContractAgreements = new HashSet<ContractAgreement>();
        }


        [Key]
        public int ConstructionCompanyId { get; set; }

        public string FirmName { get; set; }
        public string FirmNameB { get; set; }
        public string FirmContact { get; set; }
        public string FirmEmail { get; set; }
        public string FirmAddress { get; set; }
        public string FirmAddressB { get; set; }
        public string AccountNumber { get; set; }
        public string BranchName { get; set; }

        public string OwnerName { get; set; }
        public string OwnerNameB { get; set; }
        public string OwnerDesignation { get; set; }
        public string OwnerDesignationB { get; set; }
        public string OwnerPhone { get; set; }
        public string OwnerEmail { get; set; }

        public string FirstWitnessName { get; set; }
        public string FirstWitnessNameB { get; set; }
        public string FirstWitnessDesignation { get; set; }
        public string FirstWitnessDesignationB { get; set; }

        public string SecondWitnessName { get; set; }
        public string SecondWitnessNameB { get; set; }
        public string SecondWitnessDesignation { get; set; }
        public string SecondWitnessDesignationB { get; set; }

        public string AuthorizedPersonName { get; set; }
        public string AuthorizedPersonNameDesignation { get; set; }
        public string AuthorizedPersonNamePhone { get; set; }

        public string LicenceNumber { get; set; }
        public string LicensingOrganization { get; set; }
        public string LicenceCategory { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public string OwnerPicture { get; set; }
        public string AuthorizationLetter { get; set; }
        public string LicenceDocument { get; set; }


        #region  Navigation Property
        public virtual ICollection<ContractAgreement> ContractAgreements { get; set; }
        #endregion
    }
}
