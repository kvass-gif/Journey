using Journey.DataAccess.Database;
using Journey.DataAccess.Repositories;

namespace Journey.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private PlaceRepository placeRepo;
    public UnitOfWork(ApplicationDbContext context)
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

