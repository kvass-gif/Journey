using Journey.Core.Identity;
using Journey.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Journey.DataAccess.Repositories.Impl;
public class PlaceRepository : BaseRepository<Place>, IPlaceRepository
{
    public PlaceRepository(IdentityDbContext<ApplicationUser> context) : base(context) { }
}
