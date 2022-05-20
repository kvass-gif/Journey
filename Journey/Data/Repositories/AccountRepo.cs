using Journey.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Data.Repositories
{
    public class AccountRepo
    {
        private readonly IQueryable<Account> accounts;
        public AccountRepo(AppDbContext appDbContext)
        {
            accounts = appDbContext.Accounts;
        }
        public Account? FindByName(string name)
        {
            var tenant = (from t in accounts
                          where t.AccountName == name
                          select t);
            return tenant.SingleOrDefault();
        }
        public Account? FindByNameIncludeTenant(string name)
        {
            var tenant = (from t in accounts
                          where t.AccountName == name && t.Role == Role.Tenant
                          select t).Include(a => a.Reservations!)
                          .ThenInclude(a => a.Place);
            return tenant.SingleOrDefault();
        }
        public Account? FindByNameIncludeLandLord(string name)
        {
            var tenant = (from t in accounts
                          where t.AccountName == name && t.Role == Role.LandLord
                          select t).Include(a => a.Places!)
                          .ThenInclude(a => a.Reservations);
            return tenant.SingleOrDefault();
        }
        
    }
}
