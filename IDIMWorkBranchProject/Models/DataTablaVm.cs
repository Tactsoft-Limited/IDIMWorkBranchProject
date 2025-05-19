namespace IDIMWorkBranchProject.Models
{
    public class DataTablaVm
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Draw { get; set; }
        public string SearchValue { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }

    }
}