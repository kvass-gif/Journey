using Journey.Data;
using Journey.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Controllers
{
    public class HomeController : Controller
    {
        private readonly UnitOfWork unitOfWork;

        public HomeController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index(DateTime? arrivalDate, DateTime? departureDate)
        {
            
            DateTime arrivalDateLocal;
            DateTime departureDateLocal;
            if (arrivalDate == null || departureDate == null)
            {
                arrivalDateLocal = DateTime.Now.AddDays(1);
                departureDateLocal = DateTime.Now.AddDays(2);
            }
            else
            {
                arrivalDateLocal = (DateTime)arrivalDate;
                departureDateLocal = (DateTime)departureDate;
            }

            IEnumerable<Place> arr = unitOfWork.PlaceRepo
                .Places(arrivalDateLocal, departureDateLocal).ToArray();

            ViewData["arrivalDate"] = arrivalDateLocal.ToString("yyyy-MM-dd");
            ViewData["departureDate"] = departureDateLocal.ToString("yyyy-MM-dd");
            return View(arr);
        }
        public IActionResult Details(int? id)
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
            return View(obj);
        }


    }
}