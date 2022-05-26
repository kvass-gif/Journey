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
            //var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var accountId = "a8eddd62-a9b5-4f1f-9b9c-68f8abdf0e30";

            var arr = unitOfWork.ReservationRepo.ReservationsByTenantId(accountId).ToArray();
            foreach (var item in arr)
            {
                item.Sum = (item.DepartureDate - item.ArrivalDate).Days * item.Place!.PricePerNight;
            }
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
                    reservation.AccountId = "a8eddd62-a9b5-4f1f-9b9c-68f8abdf0e30";
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelReservation(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var res = unitOfWork.ReservationRepo.FindOne((int)id);
            if (res == null)
            {
                return NotFound();
            }
            res.Status = Status.Canceled;
            unitOfWork.SaveApp();
            return RedirectToAction(nameof(Index));
        }
    }
}
