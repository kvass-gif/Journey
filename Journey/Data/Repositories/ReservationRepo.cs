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
        public IEnumerable<Reservation> ReservationsByTenantId(string accountId)
        {
            var reservations = (from a in _reservations
                                where a.AccountId == accountId
                                orderby a.Status
                                select a).Include(a => a.Place);
            
            return reservations;
        }
        public IEnumerable<Reservation> ReservationsByPlaceId(int placeId)
        {
            var arr = (from a in _reservations
                       where placeId == a.PlaceId
                       select a).Include(a => a.Place).ToArray();
            var reservations = from r in arr
                               join i in _identityUsers on r.AccountId equals i.Id
                               orderby r.DepartureDate
                               select new
                               {
                                   Reservation = r,
                                   Account = i
                               };
            var reservations2 = new List<Reservation>();
            foreach (var item in reservations)
            {
                reservations2.Add(item.Reservation);
                reservations2.Last().Account = item.Account;
            }
            return reservations2;
        }
        public Reservation? FindOne(int id)
        {
            return _reservations.SingleOrDefault(a => a.Id == id);
        }

        public void Add(Reservation obj)
        {
            _reservations.Add(obj);
        }
    }
}
