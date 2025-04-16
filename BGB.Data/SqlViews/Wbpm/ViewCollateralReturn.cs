using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGB.Data.SqlViews.Wbpm
{
    [Table("ViewCollateralReturn", Schema = "wbpm")]
    public class ViewCollateralReturn
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CollateralReturnId { get; set; }
        public int ProjectWorkId { get; set; }
        public string LetterNo { get; set; }
        public string QuoteOne { get; set; }
        public string QuoteTwo { get; set; }
        public string QuoteThree { get; set; }
        public string ProjectWorkTitleB { get; set; }
        public decimal EstimatedCost { get; set; }
        public string EstimatedCostInWordBangla { get; set; }
        public string ConstructionCompanyName { get; set; }
        public string HeadAssistant { get; set; }
        public string ConcernedEngineer { get; set; }
        public string SectionIC { get; set; }
        public string BranchClerk { get; set; }        

    }
}
