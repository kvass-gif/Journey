using Journey.Core.Identity;
using Journey.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Journey.DataAccess.Repositories.Impl;
public class PlaceRepository : BaseRepository<Place>, IPlaceRepository
{
    public PlaceRepository(IdentityDbContext<ApplicationUser> context) : base(context) { }

    public async Task<List<Place>> GetAllByName(string name, int start, int takeObjects)
    {
        if(name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        if(start < 0)
        {
            throw new ArgumentException(nameof(start));
        }
        if (takeObjects < 0)
        {
            throw new ArgumentException(nameof(takeObjects));
        }
        var data = (from place in dbSet
                     where place.PlaceName.Contains(name)
                     orderby place.PlaceName
                     select place).Skip(start).Take(takeObjects);
        return await data.ToListAsync();
    }
}
