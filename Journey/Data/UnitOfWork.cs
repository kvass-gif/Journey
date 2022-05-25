﻿using Journey.Data.Repositories;

namespace Journey.Data
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext app;
        private readonly AccountDbContext account;

        private PlaceRepo placeRepo;
        private ReservationRepo reservationRepo;
        public UnitOfWork(ApplicationDbContext app, AccountDbContext account)
        {
            this.app = app;
            this.account = account;
        }
        public PlaceRepo PlaceRepo
        {
            get
            {
                if (placeRepo == null)
                {
                    placeRepo = new PlaceRepo(app);
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
