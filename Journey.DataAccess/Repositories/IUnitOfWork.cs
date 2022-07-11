namespace Journey.DataAccess.Repositories
{
    public interface IUnitofWork
    {
        IPlaceRepository PlaceRepo { get; }
        Task SaveChangesAsync();
    }
}
