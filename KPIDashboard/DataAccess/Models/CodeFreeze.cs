namespace DataAccess.Models
{
    class CodeFreeze : KPI
    {
        public int FreezePeriod { get; set; }
        public int TotalFTE { get; set; }
        public int Effort { get; set; }

    }
}
