using Journey.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Data.Repositories
{
    public class PlaceRepo
    {
        private readonly DbSet<Place> _places;
        public PlaceRepo(AppDbContext appDbContext)
        {
            _places = appDbContext.Places;
        }
        public IQueryable<Place> GetAll()
        {
            var places = (from a in _places
                          orderby a.PlaceName
                          select a);
            return places;
        }

        public Place? Find(int id)
        {
            var place = (from c in _places
                         where c.Id == id
                         select c).SingleOrDefault();
            return place;
        }
    }
}
