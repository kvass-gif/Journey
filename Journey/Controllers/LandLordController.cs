using Journey.Data;
using Journey.Entities;
using Journey.ViewModels.LandLord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Journey.Controllers
{
    [Authorize(Policy = "LandLordOnly")]
    public class LandLordController : Controller
    {
        private readonly UnitOfWork unitOfWork;
        private readonly PlacesViewHandler lordViewHandler;
        private readonly ReservationsViewHandler reservationsViewHandler;
        private readonly FilePhotoRepo _filePhotoRepo;
        public LandLordController(UnitOfWork unitOfWork, FilePhotoRepo filePhotoRepo)
        {
            this.unitOfWork = unitOfWork;
            lordViewHandler = new PlacesViewHandler(unitOfWork);
            reservationsViewHandler = new ReservationsViewHandler(unitOfWork);
            _filePhotoRepo = filePhotoRepo;
        }
        public IActionResult Index(PlacesViewModel placesViewModel)
        {
            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var accountId = "6862d78a-f6a9-47fe-be77-7b7469ac6e3b";
            lordViewHandler.HandlePlacesView(placesViewModel, accountId);

            return View(placesViewModel);
        }
        public IActionResult RegisterObject()
        {
            var place = new Place();
            place = lordViewHandler.HandleSinglePlaceModel(place);
            return View(place);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterObject(Place place)
        {
            place.Id = 0;
            if (ModelState.IsValid)
            {
                try
                {
                    var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    //var accountId = "6862d78a-f6a9-47fe-be77-7b7469ac6e3b";

                    place.AccountId = accountId;
                    unitOfWork.PlaceRepo.Add(place);
                    unitOfWork.SaveApp();
                    unitOfWork.PlaceRepo.AddFacilities(place);
                    unitOfWork.SaveApp();
                    string uniq = _filePhotoRepo.UniquePhotoName(place.Image.FileName);
                    var photo = new Photo();
                    photo.PlaceId = place.Id;
                    photo.PhotoName = uniq;
                    unitOfWork.PhotoRepo.Add(photo);
                    _filePhotoRepo.UploadFile(place.Image, uniq);
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
            place = lordViewHandler.HandleSinglePlaceModel(place);
            return View(place);
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
            place = lordViewHandler.HandleSinglePlaceModel(place);
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
            place = lordViewHandler.HandleSinglePlaceModel(place);
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
                foreach (var item in obj.Photos)
                {
                    _filePhotoRepo.DeleteFile(item.PhotoName);
                }
                unitOfWork.SaveApp();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
        public IActionResult Reservations(ReservationsViewModel model)
        {
            reservationsViewHandler.HandleReservationsView(model);
            return View(model);
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
            return RedirectToAction(nameof(Reservations), new { PlaceId = one.PlaceId });
        }
    }
}
