using Journey.DataAccess.Entities;

namespace Journey.DataAccess.Repositories;
public interface IPlaceRepository<TEntity> : IBaseRepository<Place>
{
    Task<List<TEntity>> GetAllByNamePagination(string name, int startIndex, int endIndex);
}
