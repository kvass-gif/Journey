using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Journey.Entities
{
    [Index(nameof(TypeName), IsUnique = true)]
    public class PlaceType : Record
    {
        [Required]
        public string TypeName { get; set; }
        ICollection<Place> Places { get; set; }
    }
}
