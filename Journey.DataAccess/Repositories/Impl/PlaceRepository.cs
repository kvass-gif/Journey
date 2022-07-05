
using Journey.DataAccess.Entities;
using Journey.DataAccess.Database;

namespace Journey.DataAccess.Repositories;
public class PlaceRepository :  BaseRepository<Place>, IPlaceRepository
{
    public PlaceRepository(JourneyWebContext context) : base(context) { }
}
