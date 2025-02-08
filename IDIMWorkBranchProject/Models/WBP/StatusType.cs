using System.ComponentModel.DataAnnotations;

namespace IDIMWorkBranchProject.Models.WBP
{
    public enum StatusType
    {
        Running,

        [Display(Name = "Not Work")]
        NotStart,

        [Display(Name = "Slow Work")]
        SlowWork,

        Close,

        [Display(Name = "Handing Taking Process")]
        HandingTakingProcess,

        [Display(Name = "Handing Taking Complete")]
        HandingTakingComplete
    }
}