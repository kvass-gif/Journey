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
        public IEnumerable<Place> Places(
            DateTime arrivalDate, DateTime departureDate,
             int lowerPrice, int upperPrice,
            int cityId, int typeId, int bedsCount)
        {
            Place[] places;
            var query = (from a in _places
                         where lowerPrice <= a.PricePerNight && upperPrice >= a.PricePerNight
                         select a);
            if (cityId != 0)
            {
                query = (from a in query
                         where a.CityId == cityId
                         select a);
            }
            if(bedsCount != 0)
            {
                query = (from a in query
                         where a.BedsCount == bedsCount
                         select a);
            }
            if (typeId != 0)
            {
                query = (from a in query
                         where a.PlaceTypeId == typeId
                         select a);
            }

            query = query.Include(a => a.City).Include(a => a.Reservations);
            List<Place> result = new List<Place>();
            foreach (var place in query)
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
                         select c).Include(a => a.PlaceType)
                         .Include(a => a.City).SingleOrDefault();
            return place;
        }
        public void Add(Place obj)
        {
            _places.Add(obj);
        }
    }
}
