using Journey.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace Journey.DataAccess.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Place> Places { get; set; }
    }
}
