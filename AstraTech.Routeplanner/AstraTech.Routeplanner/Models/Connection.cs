using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstraTech.Routeplanner.Models
{
    public class Connection
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Startstation")]
        public int FromStationId { get; set; }

        [ForeignKey("FromStationId")]
        public Station? FromStation { get; set; }

        [Required]
        [Display(Name = "Zielstation")]
        public int ToStationId { get; set; }

        [ForeignKey("ToStationId")]
        public Station? ToStation { get;set; }

        [Required]
        [Range(1, 100000)]
        [Display(Name = "Distanz")]
        public int Distance { get; set; }

    }
}
