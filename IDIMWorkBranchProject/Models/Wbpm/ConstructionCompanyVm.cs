using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.Wbpm;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    [Validator(typeof(ConstructionCompanyVmValidator))]
    public class ConstructionCompanyVm
    {

        public int ConstructionCompanyId { get; set; }


        [Display(Name = "প্রতিষ্ঠানের নাম (ইংরেজি)")]
        public string FirmName { get; set; }

        [Display(Name = "প্রতিষ্ঠানের নাম (বাংলা)")]
        public string FirmNameB { get; set; }

        [Display(Name = "ফোন (ইংরেজি)")]
        public string FirmContact { get; set; }

        [Display(Name = "ইমেইল (ইংরেজি)")]
        public string FirmEmail { get; set; }

        [Display(Name = "ঠিকানা (ইংরেজি)")]
        public string FirmAddress { get; set; }

        [Display(Name = "ঠিকানা (বাংলা)")]
        public string FirmAddressB { get; set; }

        [Display(Name = "হিসাব নম্বর (ইংরেজি)")]
        public string AccountNumber { get; set; }

        [Display(Name = "শাখার নাম")]
        public string BranchName { get; set; }

        [Display(Name = "স্বত্বাধিকারীর নাম (ইংরেজি)")]
        public string OwnerName { get; set; }

        [Display(Name = "স্বত্বাধিকারীর নাম (বাংলা)")]
        public string OwnerNameB { get; set; }

        [Display(Name = "পদবি (ইংরেজি)")]
        public string OwnerDesignation { get; set; }

        [Display(Name = "পদবি (বাংলা)")]
        public string OwnerDesignationB { get; set; }

        [Display(Name = "ফোন (ইংরেজি)")]
        public string OwnerPhone { get; set; }

        [Display(Name = "ইমেইল (ইংরেজি)")]
        public string OwnerEmail { get; set; }

        [Display(Name = "প্রথম সাক্ষীর নাম (ইংরেজি)")]
        public string FirstWitnessName { get; set; }

        [Display(Name = "প্রথম সাক্ষীর নাম (বাংলা)")]
        public string FirstWitnessNameB { get; set; }

        [Display(Name = "সাক্ষীর পদবি (ইংরেজি)")]
        public string FirstWitnessDesignation { get; set; }

        [Display(Name = "সাক্ষীর পদবি (বাংলা)")]
        public string FirstWitnessDesignationB { get; set; }

        [Display(Name = "দ্বিতীয় সাক্ষীর নাম (ইংরেজি)")]
        public string SecondWitnessName { get; set; }

        [Display(Name = "দ্বিতীয় সাক্ষীর নাম (বাংলা)")]
        public string SecondWitnessNameB { get; set; }

        [Display(Name = "সাক্ষীর পদবি (ইংরেজি)")]
        public string SecondWitnessDesignation { get; set; }

        [Display(Name = "সাক্ষীর পদবি (বাংলা)")]
        public string SecondWitnessDesignationB { get; set; }

        [Display(Name = "অনুমোদিত ব্যক্তির নাম (ইংরেজি)")]
        public string AuthorizedPersonName { get; set; }

        [Display(Name = "পদবি (ইংরেজি)")]
        public string AuthorizedPersonNameDesignation { get; set; }

        [Display(Name = "ফোন (ইংরেজি)")]
        public string AuthorizedPersonNamePhone { get; set; }

        [Display(Name = "লাইসেন্স নম্বর (ইংরেজি)")]
        public string LicenceNumber { get; set; }

        [Display(Name = "প্রদানকারী প্রতিষ্ঠান (ইংরেজি)")]
        public string LicensingOrganization { get; set; }

        [Display(Name = "লাইসেন্স শ্রেণী")]
        public string LicenceCategory { get; set; }

        [Display(Name = "ইস্যুর তারিখ")]
        [DataType(DataType.Date)]
        public DateTime? IssueDate { get; set; }

        [Display(Name = "মেয়াদ শেষের তারিখ")]
        [DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }

        [Display(Name = "মালিকের ছবি")]
        public string OwnerPicture { get; set; }

        [Display(Name = "অনুমোদনপত্র")]
        public string AuthorizationLetter { get; set; }

        [Display(Name = "লাইসেন্স ডকুমেন্ট")]
        public string LicenceDocument { get; set; }

        [Display(Name = "Owner Picture")]
        public HttpPostedFileBase OwnerPictureFile { get; set; }

        [Display(Name = "Authorization Letter")]
        public HttpPostedFileBase AuthorizationLetterFile { get; set; }

        [Display(Name = "Licence Document")]
        public HttpPostedFileBase LicenceDocumentFile { get; set; }
    }
}