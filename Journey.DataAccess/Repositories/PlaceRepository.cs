using Journey.Core.Identity;
using Journey.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Journey.DataAccess.Contract;

namespace Journey.DataAccess.Repositories;
public class PlaceRepository : BaseRepository<Place>, IPlaceRepository
{
    public PlaceRepository(IdentityDbContext<ApplicationUser> context) : base(context) { }
}
