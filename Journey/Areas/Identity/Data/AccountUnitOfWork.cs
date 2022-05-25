using Journey.Areas.Identity.Data.Repositories;
using Journey.Data;

namespace Journey.Areas.Identity.Data
{
    public class AccountUnitOfWork
    {
        private readonly AccountDbContext context;

        private RolesRepo rolesRepo;
        public AccountUnitOfWork(AccountDbContext context)
        {
            this.context = context;
        }
        public RolesRepo RolesRepo
        {
            get
            {
                if (rolesRepo == null)
                {
                    rolesRepo = new RolesRepo(context);
                }
                return rolesRepo;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
