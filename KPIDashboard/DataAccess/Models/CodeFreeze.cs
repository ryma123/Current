namespace DataAccess.Models
{
    class CodeFreeze : KPI
    {
        public string FreezePeriod { get; set; }
        public string TotalFTE { get; set; }
        public string Effort { get; set; }
        public string EffortPercentage { get; set; }
    }
}
