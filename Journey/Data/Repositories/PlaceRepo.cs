using Journey.Entities;
using Journey.ViewModels.Home;
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
        private void joinWithAccounts(List<Place> places)
        {
            for (int i = 0; i < places.Count; i++)
            {
                var id = places.ElementAt(i).AccountId;
                places.ElementAt(i).Account = _identityUsers.SingleOrDefault(a => a.Id == id);
            }
        }
        private List<Place> filterPlaces(IQueryable<Place> places, DateTime? arrivalDate, DateTime? departureDate)
        {
            if (arrivalDate == null || departureDate == null)
            {
                return places.ToList();
            }
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
                            resBool = reservation.DepartureDate < arrivalDate.Value
                                || departureDate.Value < reservation.ArrivalDate;
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
        public IEnumerable<Place> Places(HomeViewModel indexView)
        {
            var places = _places.AsQueryable();
            places = places.Where(a => indexView.LowerPrice <= a.PricePerNight && indexView.UpperPrice >= a.PricePerNight);
            if (indexView.SelectedCityId != 0)
            {
                places = places.Where(a => a.CityId == indexView.SelectedCityId);
            }
            if (indexView.SelectedTypeId != 0)
            {
                places = places.Where(a => a.PlaceTypeId == indexView.SelectedTypeId);
            }
            if (indexView.BedsCount != 0)
            {
                places = places.Where(a => a.BedsCount == indexView.BedsCount);
            }
            places = places.Include(a => a.City).Include(a => a.Reservations);
            List<Place> result = filterPlaces(places, indexView.ArrivalDate, indexView.DepartureDate);
            joinWithAccounts(result);
            return result;
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
