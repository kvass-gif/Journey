using Journey.Data;
using Journey.Entities;
using Journey.ViewModels.Tenant;
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
        private readonly ReservationsViewHandler reservationsViewHandler;
        public TenantController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            reservationsViewHandler = new ReservationsViewHandler(unitOfWork);
        }
        public IActionResult Reservations(ReservationsViewModel viewModel)
        {
            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var accountId = "0da506ce-1cf7-4579-b7ad-b43b59880979";
            viewModel.AccountId = accountId;
            reservationsViewHandler.HandleReservationsView(viewModel);
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MakeReservation(Place place)
        {
            if (place.ArrivalDate == null || place.DepartureDate == null)
            {
                return Content("place.ArrivalDate == null || place.DepartureDate == null");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var reservation = new Reservation();
                    reservation.ArrivalDate = place.ArrivalDate.Value;
                    reservation.DepartureDate = place.DepartureDate.Value;
                    reservation.PlaceId = place.Id;
                    reservation.AccountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    unitOfWork.ReservationRepo.Add(reservation);
                    unitOfWork.SaveApp();
                    return RedirectToAction(nameof(Reservations));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError(string.Empty,
                       "Unable to save changes. " +
                       "Try again, and if the problem persists, " +
                       "see your system administrator.");
                }
            }
            return RedirectToAction("Details", "Home", place);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelReservation(int? id)
        {
            if (id == null)
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
            return RedirectToAction(nameof(Reservations));
        }
    }
}
