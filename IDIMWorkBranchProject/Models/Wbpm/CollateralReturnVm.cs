using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class CollateralReturnVm
    {
        public int CollateralReturnId { get; set; }
        [DisplayName("নির্মাণ কাজের নাম ")]
        public int ProjectWorkId { get; set; }

        [DisplayName("নির্মাণ কাজের নাম ")]
        public string ProjectWorkName { get; set; }

        [Display(Name = "লেটার নং")]
        public string LetterNo { get; set; }

        [DisplayName("বরাত ক")]
        public string QuoteOne { get; set; }

        [DisplayName("বরাত খ")]
        public string QuoteTwo { get; set; }

        [DisplayName("বরাত গ")]
        public string QuoteThree { get; set; }

        [DisplayName("বরাত ক এর তারিখ")]
        public DateTime? QuoteOneDate { get; set; }

        [DisplayName("বরাত খ এর তারিখ")]
        public DateTime? QuoteTwoDate { get; set; }

        [DisplayName("বরাত গ এর তারিখ")]
        public DateTime? QuoteThreeDate { get; set; }

        [DisplayName("প্রধান সহকারী")]
        public int? HeadAssistantId { get; set; }


        [DisplayName("ইঞ্জিনিয়ার আইসি")]
        public int? ConcernedEngineerId { get; set; }

        [DisplayName("সেকশন আইসি")]
        public int? SectionICId { get; set; }

        [DisplayName("শাখা করনিক")]
        public int? BranchClerkId { get; set; }

        #region navigation property        
        public IEnumerable<SelectListItem> HeadAssistantDropdown { get; set; }
        public IEnumerable<SelectListItem> ConcernedEngineerDropdown { get; set; }
        public IEnumerable<SelectListItem> SectionICTDropdown { get; set; }
        public IEnumerable<SelectListItem> BranchClerkDropdown { get; set; }
        #endregion

    }
}