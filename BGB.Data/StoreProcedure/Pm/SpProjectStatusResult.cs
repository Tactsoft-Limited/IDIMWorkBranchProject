namespace BGB.Data.StoreProcedure.Pm
{
    public partial class SpProjectStatusResult
    {
        public string Projectname { get; set; }
        public int? Running { get; set; }
        public int? Not_Work { get; set; }
        public int? Slow_Work { get; set; }
        public int? Close { get; set; }
        public int? Handing_Taking_Process { get; set; }
        public int? Handing_Taking_Completed { get; set; }
    }
}
