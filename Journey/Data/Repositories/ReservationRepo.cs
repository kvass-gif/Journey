using Journey.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Journey.Data.Repositories
{
    public class ReservationRepo
    {
        private readonly DbSet<Reservation> _reservations;
        private readonly DbSet<IdentityUser> _identityUsers;

        public ReservationRepo(ApplicationDbContext appDbContext, AccountDbContext accountDbContext)
        {
            _reservations = appDbContext.Reservations;
            _identityUsers = accountDbContext.Users;
        }
        public IQueryable<Reservation> ReservationsByTenantId(string accountId)
        {
            var reservations = (from a in _reservations
                                where a.AccountId == accountId
                                orderby a.Place!.PlaceName
                                select a).Include(a => a.Place);
            return reservations;
        }
        public IQueryable<Reservation> ReservationsByPlaceId(int placeId)
        {
            var reservations = (from a in _identityUsers
                                join r in _reservations on a.Id equals r.AccountId
                                where r.PlaceId == placeId
                                select new Reservation()
                                {
                                    ArrivalDate = r.ArrivalDate,
                                    DepartureDate = r.DepartureDate,
                                    IsArrived = r.IsArrived,
                                    PlaceId = r.PlaceId,
                                    Place = r.Place,
                                    AccountId = r.AccountId,
                                    Account = a
                                });
            return reservations;
        }

        public void Add(Reservation obj)
        {
            _reservations.Add(obj);
        }
    }
}
