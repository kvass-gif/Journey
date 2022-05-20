using Journey.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Data.Repositories
{
    public class ReservationRepo
    {
        private readonly DbSet<Reservation> _reservations;
        public ReservationRepo(AppDbContext appDbContext)
        {
            _reservations = appDbContext.Reservations;
        }
        public IQueryable<Reservation> AllReservationsByPlace(int placeId)
        {
            var reservations = (from c in _reservations
                                where c.PlaceId == placeId
                                select c).Include(a => a.Account);
            return reservations;
        }

    }
}
