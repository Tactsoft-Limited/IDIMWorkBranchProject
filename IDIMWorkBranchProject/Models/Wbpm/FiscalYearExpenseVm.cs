using System.ComponentModel;
using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.Wbpm;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    [Validator(typeof(FiscalYearExpenseVmValidator))]
    public class FiscalYearExpenseVm
    {


        public int FiscalYearExpenseId { get; set; }

        [DisplayName("প্রকল্প আইডি")]
        public int ADPProjectId { get; set; }

        [DisplayName("প্রকল্পের শিরোনাম")]
        public string ProjectTitle { get; set; }

        [DisplayName("আর্থবছর")]
        public int FiscalYearId { get; set; }

        [DisplayName("আর্থবছর")]
        public string FiscalYearDescription { get; set; }

        [DisplayName("মোট খরচ")]
        public float TotalExpense { get; set; }

    }
}