using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Journey.DataAccess.Entities
{
    public class Place
    {
        public long PlaceId { get; set; }
        public string PlaceName { get; set; }
    }
}
