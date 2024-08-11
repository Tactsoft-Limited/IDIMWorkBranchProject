using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

public class FundReleaseVm
{
    public FundReleaseVm()
    {
      
        FiscalYearDropdown = new List<SelectListItem>();
    }
    public int FundReleaseId { get; set; }
    public Nullable<int> ProjectId { get; set; }
    [Display(Name = "Financial Year")]
      public Nullable<int> FiscalYearID { get; set; }
    [Display(Name = "Release Date")]
    public Nullable<System.DateTime> ReleasedDate { get; set; }
    [Display(Name = "Felease Amount")]
    public Nullable<double> ReleasedAmount { get; set; }
    public string Remark { get; set; }
    [Display(Name = "Project")]
    public string ProjectName { get; set; }
    public IEnumerable<SelectListItem> FiscalYearDropdown { get; set; }


}