
using Journey.DataAccess.Entities;
using Journey.DataAccess.Database;

namespace Journey.DataAccess.Repositories.Impl;

public class CityRepository : BaseRepository<Place>, ICityRepository
{
    public CityRepository(ApplicationDbContext context) : base(context) { }
}
