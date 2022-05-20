using Journey.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            if (User.Identity == null || User.Identity.Name == null)
            {
                return Content("User.Identity == null");
            }
            string name = User.Identity.Name;
            var landLord = unitOfWork.AccountRepo.FindByNameIncludeLandLord(name);
            if (landLord == null)
            {
                return Content("landLord == null");
            }
            return View(landLord);
        }
        public IActionResult AccountReservations(int? placeId)
        {
            if (placeId == null)
            {
                return NotFound();
            }
            var obj = unitOfWork.ReservationRepo.AllReservationsByPlace((int)placeId);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj.ToArray());
        }
    }
}
