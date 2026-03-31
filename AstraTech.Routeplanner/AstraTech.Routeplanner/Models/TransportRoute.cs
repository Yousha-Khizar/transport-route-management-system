using System.ComponentModel.DataAnnotations;

namespace AstraTech.Routeplanner.Models
{
    public class TransportRoute
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Routenbezeichnung")]
        public string RouteName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Startpunkt")]
        public int StartStationId { get; set; }

        [Required]
        [Display(Name = "Zielpunkt")]
        public int DestinationStationId { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Priorität")]
        public string Priority { get; set; } = "Mittel";

        [Required]
        [StringLength(20)]
        [Display(Name = "Status")]
        public string Status { get; set; } = "Geplant";

        [Range(1, 100000)]
        [Display(Name = "Geschätzte Distanz")]
        public int EstimatedDistance { get; set; }

        [StringLength(500)]
        [Display(Name= "Bemerkung")]
        public string? Notes { get; set; }

        public Station? StartStation { get; set; }
        public Station? DestinationStation { get; set; }


    }
}
