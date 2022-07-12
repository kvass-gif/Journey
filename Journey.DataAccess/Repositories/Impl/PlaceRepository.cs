
using Journey.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Journey.DataAccess.Repositories;
public class PlaceRepository : BaseRepository<Place>, IPlaceRepository
{
    public PlaceRepository(IdentityDbContext<IdentityUser> context) : base(context) { }
}
