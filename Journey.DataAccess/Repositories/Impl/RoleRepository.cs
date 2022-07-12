using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Journey.DataAccess.Repositories.Impl
{
    public class RoleRepository : BaseRepository<IdentityRole>, IRoleRepository
    {
        public RoleRepository(IdentityDbContext<IdentityUser> context) : base(context) { }
    }
}
