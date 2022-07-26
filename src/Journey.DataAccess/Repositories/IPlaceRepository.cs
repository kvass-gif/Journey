using Journey.DataAccess.Entities;

namespace Journey.DataAccess.Repositories;
public interface IPlaceRepository : IBaseRepository<Place>
{
    Task<List<Place>> GetAllByName(string name, int skipObjects, int takeObjects);
}
