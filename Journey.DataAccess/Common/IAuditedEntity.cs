namespace Journey.DataAccess.Common
{
    public interface IAuditedEntity
    {
        string CreatedByUserId { get; set; }
        DateTime CreatedOn { get; set; }
        string? UpdatedByUserId { get; set; }
        DateTime? UpdatedOn { get; set; }
    }
}
