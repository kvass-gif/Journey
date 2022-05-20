using Journey.Data.Repositories;

namespace Journey.Data
{
    public class UnitOfWork
    {
        private readonly AppDbContext context;

        private PlaceRepo placeRepo;
        private ReservationRepo reservationRepo;
        private AccountRepo accountRepo;

        public UnitOfWork(AppDbContext context)
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
        public AccountRepo AccountRepo
        {
            get
            {
                if (accountRepo == null)
                {
                    accountRepo = new AccountRepo(context);
                }
                return accountRepo;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
