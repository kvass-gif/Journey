using Journey.Data.Repositories;

namespace Journey.Data
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext context;

        private PlaceRepo placeRepo;
        private ReservationRepo reservationRepo;
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }
        public PlaceRepo PlaceRepository
        {
            get
            {
                if (placeRepo == null)
                {
                    placeRepo = new PlaceRepo(context);
                }
                return placeRepo;
            }
        }
        public ReservationRepo ReservationRepo
        {
            get
            {
                if (reservationRepo == null)
                {
                    reservationRepo = new ReservationRepo(context);
                }
                return reservationRepo;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
