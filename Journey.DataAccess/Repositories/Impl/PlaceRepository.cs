
using Journey.DataAccess.Entities;
using Journey.DataAccess.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Journey.DataAccess.Repositories;
public class PlaceRepository : BaseRepository<Place>, IPlaceRepository
{
    public PlaceRepository(IdentityDbContext<ApplicationUser> context) : base(context) { }
}
