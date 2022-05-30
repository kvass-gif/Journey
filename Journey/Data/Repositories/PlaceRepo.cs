using Journey.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Journey.Data.Repositories
{
    public class PlaceRepo
    {
        private readonly DbSet<Place> _places;
        private readonly DbSet<IdentityUser> _identityUsers;
        public PlaceRepo(ApplicationDbContext appDbContext, AccountDbContext accountDbContext)
        {
            _places = appDbContext.Places;
            _identityUsers = accountDbContext.Users;
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
                          orderby a.Rank descending
                          select a).Include(a => a.Reservations).ToArray();
            List<Place> result = new List<Place>();
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
            for (int i = 0; i < result.Count; i++)
            {
                var id = result.ElementAt(i).AccountId;
                result.ElementAt(i).Account = _identityUsers.SingleOrDefault(a => a.Id == id);
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
