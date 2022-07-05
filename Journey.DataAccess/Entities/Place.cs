using Journey.DataAccess.Common;

namespace Journey.DataAccess.Entities
{
    public class Place : BaseEntity, IAuditedEntity
    {
        public string PlaceName { get; set; }
    }
}
