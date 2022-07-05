namespace Journey.DataAccess.Common
{
    public abstract class BaseEntity : IAuditedEntity
    {
        public long Id { get; set; }
        public string CreatedByUserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? UpdatedByUserId { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
