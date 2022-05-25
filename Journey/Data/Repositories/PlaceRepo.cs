using Journey.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Data.Repositories
{
    public class PlaceRepo
    {
        private readonly DbSet<Place> _places;
        public PlaceRepo(ApplicationDbContext appDbContext)
        {
            _places = appDbContext.Places;
        }
        public IQueryable<Place> Places(string accountId)
        {
            var places = (from a in _places
                          where a.AccountId == accountId
                          orderby a.PlaceName
                          select a);
            return places;
        }
        public IQueryable<Place> Places()
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
        public void Add(Place obj)
        {
            _places.Add(obj);
        }
    }
}
