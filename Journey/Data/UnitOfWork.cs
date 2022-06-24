using Journey.Data.Repositories;

namespace Journey.Data
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext app;
        private readonly AccountDbContext account;

        private CityRepo cityRepo;
        private TypeRepo typeRepo;
        private PlaceRepo placeRepo;
        private ReservationRepo reservationRepo;
        private FacilityRepo facilityRepo;
        private PhotoRepo photoRepo;
        public UnitOfWork(ApplicationDbContext app, AccountDbContext account)
        {
            this.app = app;
            this.account = account;
        }
        public CityRepo CityRepo
        {
            get
            {
                if (cityRepo == null)
                {
                    cityRepo = new CityRepo(app);
                }
                return cityRepo;
            }
        }
        public TypeRepo TypeRepo
        {
            get
            {
                if (typeRepo == null)
                {
                    typeRepo = new TypeRepo(app);
                }
                return typeRepo;
            }
        }
        public PlaceRepo PlaceRepo
        {
            get
            {
                if (placeRepo == null)
                {
                    placeRepo = new PlaceRepo(app, account);
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
                    reservationRepo = new ReservationRepo(app, account);
                }
                return reservationRepo;
            }
        }
        public FacilityRepo FacilityRepo
        {
            get
            {
                if (facilityRepo == null)
                {
                    facilityRepo = new FacilityRepo(app);
                }
                return facilityRepo;
            }
        }
        public PhotoRepo PhotoRepo
        {
            get
            {
                if (photoRepo == null)
                {
                    photoRepo = new PhotoRepo(app);
                }
                return photoRepo;
            }
        }
        public void SaveApp()
        {
            app.SaveChanges();
        }
        public void SaveAccount()
        {
            account.SaveChanges();
        }
    }
}
