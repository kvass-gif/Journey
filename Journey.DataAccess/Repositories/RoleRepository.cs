using Journey.Core.Identity;
using Journey.DataAccess.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Journey.DataAccess.Repositories
{
    public class RoleRepository : BaseRepository<IdentityRole>, IRoleRepository
    {
        public RoleRepository(IdentityDbContext<ApplicationUser> context) : base(context) { }
    }
}
