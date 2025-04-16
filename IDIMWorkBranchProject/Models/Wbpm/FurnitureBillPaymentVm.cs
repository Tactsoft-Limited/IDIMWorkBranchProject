using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class FurnitureBillPaymentVm
    {
        public FurnitureBillPaymentVm()
        {
            ConstructionCompanyDropdown = new List<SelectListItem>();
        }
        public int FurnitureBillPaymentId { get; set; }
        public int ProjectWorkId { get; set; }

        [DisplayName("নির্মাণ কাজের নাম ")]
        public string ProjectWorkTitleB { get; set; }
        public int ConstractorCompanyId { get; set; }

        [DisplayName(" ঠিকাদারী প্রতিষ্ঠানের নাম ")]
        public string ConstractorCompanyName { get; set; }

        [DisplayName("পরিবর্তন যোগ্য ঠিকাদারী প্রতিষ্ঠানের নাম ")]
        public int ChangedConstractorCompanyId { get; set; }

        [DisplayName("লেটার নং")]
        public string LetterNo { get; set; }

        [DisplayName("বরাত ক")]
        public string QuoteOne { get; set; }
        [DisplayName("বরাত ক এর তারিখ")]
        public DateTime? QuoteOneDate { get; set; }

        [DisplayName("বরাত খ")]
        public string QuoteTwo { get; set; }
        [DisplayName("বরাত খ এর তারিখ")]        
        public DateTime? QuoteTwoDate { get; set; }

        [DisplayName("বরাত গ")]
        public string QuoteThree { get; set; }


        [DisplayName("বরাত গ এর তারিখ")]
        public DateTime? QuoteThreeDate { get; set; }

        [DisplayName("বরাত ঘ")]
        public string QuoteFour { get; set; }

        [DisplayName("বরাত ঘ এর তারিখ")]
        public DateTime? QuoteFourDate { get; set; }

        [DisplayName("আসবাব পত্রে বরাদ্দ")]
        public decimal AllocationToFurniture { get; set; }

        [DisplayName("আসবাব পত্রে বরাদ্দ কথায় (বাংলা) ")]
        public string AllocationToFurnitureInWordB { get; set; }

        [DisplayName("বিজিবি বিবিধ ফান্ডে আছে ")]
        public decimal DepositsInFund { get; set; }

        [DisplayName("বিজিবি বিবিধ ফান্ডে জমা থাকবে ")]
        public decimal DepositedInFund { get; set; }

        [DisplayName("বিজিবি বিবিধ ফান্ডে জমা থাকবে কথায় (বাংলা) ")]
        public string DepositedInFundInWordB { get; set; }

        [DisplayName("বিল পরিশোধের পরিমাণ ")]
        public decimal PaymentAmount { get; set; }

        [DisplayName("বিল পরিশোধের পরিমাণ কথায় (বাংলা) ")]
        public string PaymentAmountInWordB { get; set; }

        [DisplayName("প্রধান সহকারী")]
        public int? HeadAssistantId { get; set; }


        [DisplayName("ইঞ্জিনিয়ার আইসি")]
        public int? ConcernedEngineerId { get; set; }

        [DisplayName("সেকশন আইসি")]
        public int? SectionICId { get; set; }

        [DisplayName("শাখা করনিক")]
        public int? BranchClerkId { get; set; }

        #region navigation property

        public IEnumerable<SelectListItem> ConstructionCompanyDropdown { get; set; }
        public IEnumerable<SelectListItem> BGBFundDropdown { get; set; }
        public IEnumerable<SelectListItem> HeadAssistantDropdown { get; set; }
        public IEnumerable<SelectListItem> ConcernedEngineerDropdown { get; set; }
        public IEnumerable<SelectListItem> SectionICTDropdown { get; set; }
        public IEnumerable<SelectListItem> BranchClerkDropdown { get; set; }
        #endregion
       
    }
}