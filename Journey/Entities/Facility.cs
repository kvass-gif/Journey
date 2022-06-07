using Microsoft.EntityFrameworkCore;

namespace Journey.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Facility : Record
    {
        public string Name { get; set; }
        public ICollection<FacilityPlace> Places { get; set; }
    }
}
