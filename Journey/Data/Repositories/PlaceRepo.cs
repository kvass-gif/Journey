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
        public IEnumerable<Place> Places(DateTime arrivalDate, DateTime departureDate)
        {
            var places = (from a in _places
                          select a).Include(a => a.Reservations).ToArray();
            ICollection<Place> result = new List<Place>();
            foreach (var place in places)
            {
                if (place.Reservations != null)
                {
                    bool resBool = true;
                    foreach (var reservation in place.Reservations)
                    {
                        if (!(reservation.Status == Status.Canceled 
                            || reservation.Status == Status.Completed))
                        {
                            resBool = reservation.DepartureDate < arrivalDate
                                || departureDate < reservation.ArrivalDate;
                        }
                        if (resBool == false)
                        {
                            break;
                        }
                    }
                    if (resBool)
                    {
                        result.Add(place);
                    }
                }
            }
            return result;
        }
        public IQueryable<Place> Places()
        {
            var places = (from a in _places
                          orderby a.Rank
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
