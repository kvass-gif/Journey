using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Journey.Entities
{
    [Index(nameof(PlaceName), IsUnique = true)]
    public class Place : Record
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string PlaceName { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(4000)]
        public string Description { get; set; }
        public int AccountId { get; set; }
        public Account? Account { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
    }
}
