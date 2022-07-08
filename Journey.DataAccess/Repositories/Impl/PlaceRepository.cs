
using Journey.DataAccess.Entities;
using Journey.DataAccess.Database;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Journey.DataAccess.Repositories;
public class PlaceRepository :  BaseRepository<Place>, IPlaceRepository
{
    public PlaceRepository(IdentityDbContext<IdentityUser> context) : base(context) { }
}
