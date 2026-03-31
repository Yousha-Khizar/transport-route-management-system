using System.ComponentModel.DataAnnotations;


namespace AstraTech.Routeplanner.Models
{
    public class Station
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Stationsname")]
        public string Name { get; set; } = string.Empty;

        [StringLength(50)]
        [Display(Name = "Stationstyp")]
        public string? Type { get; set; }

    }
}
