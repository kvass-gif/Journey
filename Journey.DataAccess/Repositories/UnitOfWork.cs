using Journey.DataAccess.Database;
using Journey.DataAccess.Contract;

namespace Journey.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly JourneyWebContext _context;
    private IPlaceRepository placeRepo;
    private IRoleRepository roleRepo;
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
    public IRoleRepository RoleRepo
    {
        get
        {
            if (roleRepo == null)
            {
                roleRepo = new RoleRepository(_context);
            }
            return roleRepo;
        }
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

