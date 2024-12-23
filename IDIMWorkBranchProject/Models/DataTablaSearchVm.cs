namespace IDIMWorkBranchProject.Models
{
    public class DataTablaSearchVm
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Draw { get; set; } // For synchronizing with the client-side
        public string SearchValue { get; set; }
        public string SortColumn { get; set; } // Column name to sort by
        public string SortDirection { get; set; } // Sorting direction (asc/desc)

    }
}