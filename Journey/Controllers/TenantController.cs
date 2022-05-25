using Journey.Data;
using Journey.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Journey.Controllers
{
    [Authorize(Policy = "TenantOnly")]
    public class TenantController : Controller
    {
        private readonly UnitOfWork unitOfWork;
        public TenantController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var arr = unitOfWork.ReservationRepo.ReservationsByTenantId(accountId).ToArray();
            return View(arr);
        }
        public IActionResult MakeReservation(int placeId)
        {
            var reservation = new Reservation();
            reservation.ArrivalDate = DateTime.Now.AddDays(1);
            reservation.DepartureDate = DateTime.Now.AddDays(2);
            reservation.MaxDurationDays = 30;
            reservation.PlaceId = placeId;
            return View(reservation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MakeReservation(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    reservation.AccountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    unitOfWork.ReservationRepo.Add(reservation);
                    unitOfWork.SaveApp();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError(string.Empty,
                       "Unable to save changes. " +
                       "Try again, and if the problem persists, " +
                       "see your system administrator.");
                }
            }
            return View(reservation);
        }
    }
}
