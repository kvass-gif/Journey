using Journey.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace Journey.Core.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Place> Places { get; set; }
    }
}
