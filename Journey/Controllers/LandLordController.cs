using Journey.Data;
using Journey.Entities;
using Journey.ViewModels.LandLord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Journey.Controllers
{
    //[Authorize(Policy = "LandLordOnly")]
    public class LandLordController : Controller
    {
        private readonly UnitOfWork unitOfWork;
        private readonly LandLordViewHandler lordViewHandler;
        public LandLordController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            lordViewHandler = new LandLordViewHandler(unitOfWork);
        }
        public IActionResult Index(PlacesViewModel placesViewModel)
        {
            //var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var accountId = "6862d78a-f6a9-47fe-be77-7b7469ac6e3b";
            lordViewHandler.HandlePlacesView(placesViewModel, accountId);
            return View(placesViewModel);
        }
        public IActionResult RegisterObject()
        {
            var place = new Place();
            place = lordViewHandler.HandleModel(place);
            return View(place);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterObject(Place place)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var accountId = "6862d78a-f6a9-47fe-be77-7b7469ac6e3b";

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
            place = lordViewHandler.HandleModel(place);
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
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var place = unitOfWork.PlaceRepo.Find((int)id);
            if (place == null)
            {
                return NotFound();
            }
            place = lordViewHandler.HandleModel(place);
            return View(place);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Place place)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.PlaceRepo.Update(place);
                    unitOfWork.SaveApp();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ViewData["ErrorMessage"] =
                        "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.";
                }
            }
            place = lordViewHandler.HandleModel(place);
            return View(place);
        }
        public IActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = unitOfWork.PlaceRepo.Find((int)id);
            if (obj == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }
            return View(obj);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var obj = unitOfWork.PlaceRepo.Find(id);
            if (obj == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                unitOfWork.PlaceRepo.Remove(obj);
                unitOfWork.SaveApp();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

    }
}
