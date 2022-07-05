using Microsoft.EntityFrameworkCore;

namespace Journey.DataAccess.Database;
public static class AutomatedMigration
{
    public static void Migrate(DbContext context)
    {
        if (context.Database.IsSqlServer())
        {
            context.Database.Migrate();
        }
       
    }
}
