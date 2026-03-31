namespace AstraTech.Routeplanner.Models
{
    public class RouteCalculationResult
    {
        public int FromStationId { get; set; }
        public int ToStationId { get; set; }

        public string StartStationName { get; set; } = string.Empty;
        public string DestinationStationName { get; set; } = string.Empty;

        public List<string> Path { get; set; } = new();
        public int TotalDistance { get; set; }
        public bool RouteFound { get; set; }
    }
}
