using Journey.Data;
using Journey.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Journey.Controllers
{
    [Authorize(Policy = "LandLordOnly")]
    public class LandLordController : Controller
    {
        private readonly UnitOfWork unitOfWork;
        public LandLordController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var arr = unitOfWork.PlaceRepo.Places(accountId).ToArray();
            return View(arr);
        }
        public IActionResult RegisterObject()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterObject(Place place)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    place.AccountId = accountId;
                    unitOfWork.PlaceRepo.Add(place);
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
            return View(place);
        }
        public IActionResult Reservations(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var arr = unitOfWork.ReservationRepo.ReservationsByPlaceId((int)id);
            if (arr == null)
            {
                return NotFound();
            }
            return View(arr.ToArray());
        }
       
    }
}
