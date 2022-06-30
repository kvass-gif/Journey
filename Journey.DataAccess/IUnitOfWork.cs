using Journey.DataAccess.Repositories;


namespace Journey.DataAccess
{
    public interface IUnitOfWork
    {
        IPlaceRepository PlaceRepo { get; }
    }
}
