using Journey.Core.Identity;
using Journey.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Journey.DataAccess.Repositories.Impl;
public class PlaceRepository : BaseRepository<Place>, IPlaceRepository<Place>
{
    public PlaceRepository(IdentityDbContext<ApplicationUser> context) : base(context) { }

    public Task<List<Place>> GetAllByNamePagination(string name, int startIndex, int endIndex, int pageSize)
    {
        var data = from place in dbSet
                     where place.PlaceName.Contains(name)
                     select place;
        int totalPage = data.Count();
        float totalNumsize = totalPage / (float)pageSize;
        throw new NotImplementedException();
    }
}
