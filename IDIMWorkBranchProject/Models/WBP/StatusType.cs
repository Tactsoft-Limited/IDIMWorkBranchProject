using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;

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