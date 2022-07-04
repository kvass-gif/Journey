using Journey.DataAccess.Database;
using Journey.DataAccess.Repositories;

namespace Journey.DataAccess.Repositories.Impl;

public class UnitOfWork : IUnitOfWork
{
    private readonly JourneyWebContext _context;
    private PlaceRepository placeRepo;
    public UnitOfWork(JourneyWebContext context)
    {
        _context = context;
    }
    public IPlaceRepository PlaceRepo
    {
        get
        {
            if (placeRepo == null)
            {
                placeRepo = new PlaceRepository(_context);
            }
            return placeRepo;
        }
    }
}

