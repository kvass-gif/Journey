namespace Journey.DataAccess.Abstraction
{
    public interface IUnitOfWork
    {
        IPlaceRepository PlaceRepo { get; }
        IRoleRepository RoleRepo { get; }
        Task SaveChangesAsync();

    }
}
