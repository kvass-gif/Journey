using Journey.DataAccess.Entities;

namespace Journey.DataAccess.Database;
public static class DatabaseContextSeed
{
    public static void SeedDatabase(ApplicationDbContext context)
    {
        if (context.Places.Any() == false)
        {
            context.Places.Add(new Place()
            {
                PlaceName = "place1"
            });
            context.Places.Add(new Place()
            {
                PlaceName = "place2"
            });
            context.Places.Add(new Place()
            {
                PlaceName = "place3"
            });
        }
        context.SaveChanges();
    }
}
