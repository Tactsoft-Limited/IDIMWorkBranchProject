using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    public class CurrentBillCheckVm
    {
        public int CurrentBillCheckId { get; set; }

        [DisplayName("নির্মাণ কাজ")]
        public int? ProjectWorkId { get; set; }

        [DisplayName("নির্মাণ কাজের নাম")]
        public string ProjectWorkTitleB { get; set; }

        [DisplayName("চলতি বিল")]
        public int? ContractorCompanyPaymentId { get; set; }

        [DisplayName("লেটার নং")]
        public string LetterNo { get; set; }
        [DisplayName("চলতি বিল চেকের তারিখ")]
        public DateTime CheckDate { get; set; }

        [DisplayName("চলতি বিল চেকের তারিখ বাংলা")]
        public string CheckDateB { get; set; }

        [DisplayName("নোটশিট তারিখ")]
        public DateTime NoteSheetDate { get; set; }

        [DisplayName("অফিসার")]
        public int? OfficerId { get; set; }

        [DisplayName("অফিসার")]
        public string officerName { get; set; }

        public IEnumerable<SelectListItem> OfficersDropdown { get; set; }
    }
}