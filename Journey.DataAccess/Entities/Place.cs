using Microsoft.EntityFrameworkCore;

namespace Journey.DataAccess.Entities
{
    [Index(nameof(PlaceName))]
    public class Place
    {
        public long PlaceId { get; set; }
        public string PlaceName { get; set; }
    }
}
