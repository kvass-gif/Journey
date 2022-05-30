using Journey.Data;
using Journey.Entities;
using Journey.ViewModels.LandLord;
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
        private readonly LandLordViewCreator modelCreator;
        public LandLordController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            modelCreator = new LandLordViewCreator(unitOfWork);
        }
        public IActionResult Index()
        {
            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var arr = unitOfWork.PlaceRepo.Places(accountId).ToArray();
            return View(arr);
        }
        public IActionResult RegisterObject()
        {
            var model = modelCreator.GetViewModel();
            return View(model);
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
            var model = modelCreator.GetViewModel();
            return View(place);
        }
        public IActionResult Reservations(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var list = unitOfWork.ReservationRepo.ReservationsByPlaceId((int)id);
            foreach (var item in list)
            {
                item.Sum = (item.DepartureDate - item.ArrivalDate).Days * item.Place!.PricePerNight;
            }
            foreach (var res in list)
            {
                var dic = new Dictionary<int, string>();
                foreach (var stat in Enum.GetValues(typeof(Status)))
                {
                    if (
                        (Status)stat == Status.Waiting
                        ||
                        res.IsPaid && res.ArrivalDate < DateTime.Now
                        && (((Status)stat == Status.Completed)
                        || ((Status)stat == Status.InPlace))
                        ||
                        !res.IsPaid
                        && res.ArrivalDate >= DateTime.Now
                        && ((Status)stat == Status.Canceled)
                        )
                    {
                        dic.Add((int)stat, ((Status)stat).ToString());
                    }
                }
                res.StatusDictionary = dic;
            }
            if (list == null)
            {
                return NotFound();
            }
            return View(list);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MakePayment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var one = unitOfWork.ReservationRepo.FindOne((int)id);
            if (one == null)
            {
                return NotFound();
            }
            one.IsPaid = true;
            unitOfWork.SaveApp();
            return RedirectToAction(nameof(Reservations), new { id = one.PlaceId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeStatus(int? id, int select)
        {
            if (id == null)
            {
                return NotFound();
            }
            var one = unitOfWork.ReservationRepo.FindOne((int)id);
            if (one == null)
            {
                return NotFound();
            }
            one.Status = (Status)select;
            unitOfWork.SaveApp();
            return RedirectToAction(nameof(Reservations), new { id = one.PlaceId });
        }

    }
}
