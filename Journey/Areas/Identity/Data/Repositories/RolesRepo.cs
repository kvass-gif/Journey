using Journey.Data;
using Microsoft.AspNetCore.Identity;

namespace Journey.Areas.Identity.Data.Repositories
{
    public class RolesRepo
    {
        private readonly AccountDbContext accountDbContext;
        public RolesRepo(AccountDbContext accountDbContext)
        {
            this.accountDbContext = accountDbContext;
        }
        public List<string> RolesArray()
        {
            var list = new List<string>();
            foreach (var item in accountDbContext.Roles.OrderBy(a => a.Name))
            {
                list.Add(item.Name);
            }
            return list;
        }
    }
}
