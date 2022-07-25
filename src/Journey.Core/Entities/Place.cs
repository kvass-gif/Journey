using Journey.Core.Identity;
using Journey.DataAccess.Common;

namespace Journey.DataAccess.Entities
{
    public class Place : BaseEntity, IAuditedEntity
    {
        public string PlaceName { get; set; }
        public string Description { get; set; }
        public string StreetAddress { get; set; }
        public int BedsCount { get; set; }
        public int Rank { get; set; }
        public float PricePerNight { get; set; }


        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
