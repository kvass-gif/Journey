using Journey.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Data.Repositories
{
    public class ReservationRepo
    {
        private readonly DbSet<Reservation> _reservations;
        public ReservationRepo(ApplicationDbContext appDbContext)
        {
            _reservations = appDbContext.Reservations;
        }
    }
}
