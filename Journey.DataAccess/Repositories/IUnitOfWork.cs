namespace Journey.DataAccess.Repositories
{
    public interface IUnitOfWork
    {
        IPlaceRepository PlaceRepo { get; }
        Task SaveChangesAsync();
    }
}
