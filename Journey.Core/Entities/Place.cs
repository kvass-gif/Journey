using Journey.Core.Identity;
using Journey.DataAccess.Common;

namespace Journey.DataAccess.Entities
{
    public class Place : BaseEntity, IAuditedEntity
    {
        public string PlaceName { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
