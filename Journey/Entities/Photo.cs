using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Journey.Entities
{
    [Index(nameof(PhotoName))]
    public class Photo : Record
    {
        public string PhotoName { get; set; }
        public int PlaceId { get; set; }
        public Place? Place { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
