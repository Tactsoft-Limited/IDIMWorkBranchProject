using System.ComponentModel;
using System;
using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.Wbpm;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    [Validator(typeof(ADPProjectVmValidator))]
    public class ADPProjectVm
    {
        public ADPProjectVm()
        {
            ProjectDirectorDropdown = new List<SelectListItem>();
        }

        public int ADPProjectId { get; set; }

        [DisplayName("প্রকল্পের শিরোনাম")]
        public string ProjectTitle { get; set; }

        [DisplayName("মন্ত্রনালয়/বিভাগ")]
        public string MinistryDepartment { get; set; }

        [DisplayName("প্রাক্কলিত ব্যয়")]
        public decimal EstimatedExpenses { get; set; }

        [DisplayName("প্রকল্প শুরুর তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartingDate { get; set; }

        [DisplayName("প্রকল্প সমাপ্তির তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndingDate { get; set; }

        [DisplayName("প্যাকেজ সংখ্যা")]
        public int NoOfWork { get; set; }

        [DisplayName("আর্থিক অগ্রগতি")]
        public double FinancialProgress { get; set; }

        [DisplayName("ভৌত অগ্রগতি")]
        public double PhysicalProgress { get; set; }

        [DisplayName("মন্তব্য")]
        public string Remarks { get; set; }

        public ProjectDirectorVm ProjectDirector { get; set; }
        public IEnumerable<SelectListItem> ProjectDirectorDropdown { get; set; }

    }
}