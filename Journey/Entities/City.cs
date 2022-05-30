using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Journey.Entities
{
    [Index(nameof(CityName), IsUnique = true)]
    public class City : Record
    {
        [Required]
        public string  CityName { get; set; }
        public ICollection<Place> Places { get; set; }
    }
}
