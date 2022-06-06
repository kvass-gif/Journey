﻿using Journey.Entities;
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
        public IEnumerable<Reservation> ReservationsByTenantId(string accountId, string selection)
        {
            IEnumerable<Reservation> arr = _reservations.Where(a => a.AccountId == accountId)
                .Include(a => a.Place).ToArray();
            if (selection == "Current")
            {
                arr = arr.Where(a => (a.Status == Status.Waiting
                || a.Status == Status.InPlace)
                && a.ArrivalDate <= DateTime.Now.Date && a.DepartureDate >= DateTime.Now.Date);
            }
            else if (selection != "All reservations")
            {
                arr = arr.Where(a => a.Status.ToString() == selection);
            }
            return arr;
        }
        public IEnumerable<Reservation> ReservationsByParamsPlace(int placeId, string selection)
        {
            IEnumerable<Reservation> arr = _reservations.Where(a => a.PlaceId == placeId)
                .Include(a => a.Place).ToArray();

            if (selection == "Current" )
            {
                arr = arr.Where(a => (a.Status == Status.Waiting
                || a.Status == Status.InPlace)
                && a.ArrivalDate <= DateTime.Now.Date && a.DepartureDate >= DateTime.Now.Date);
            }
            else if (selection != "All reservations")
            {
                arr = arr.Where(a => a.Status.ToString() == selection);
            }
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
