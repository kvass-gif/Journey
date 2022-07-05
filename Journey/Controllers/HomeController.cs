using Journey.Data;
using Journey.Entities;
using Journey.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Controllers
{
    public class HomeController : Controller
    {
        private readonly UnitOfWork unitOfWork;
        private readonly HomeViewHandler modelHandler;

        public HomeController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            modelHandler = new HomeViewHandler(unitOfWork);
        }
        public IActionResult Index(HomeViewModel homeViewModel)
        {
            modelHandler.HandleIndexView(homeViewModel);
            return View(homeViewModel);
        }
        public IActionResult Details(int? id, DateTime? arrivalDate, DateTime? departureDate)
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
            
            obj.ArrivalDate = arrivalDate;
            obj.DepartureDate = departureDate;
            return View(obj);
        }
        public IActionResult About()
        {
            return View();
        }
    }
}