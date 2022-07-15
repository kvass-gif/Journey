namespace Journey.DataAccess.Contract
{
    public interface IUnitOfWork
    {
        IPlaceRepository PlaceRepo { get; }
        IRoleRepository RoleRepo { get; }
        Task SaveChangesAsync();

    }
}
