using Journey.Data;
using Journey.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Controllers
{
    public class HomeController : Controller
    {
        private readonly UnitOfWork unitOfWork;
        private readonly HomeViewCreator modelCreator;

        public HomeController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            modelCreator = new HomeViewCreator(unitOfWork);
        }
        public IActionResult Index(
            DateTime? arrivalDate, DateTime? departureDate,
            int? lowerPrice, int? upperPrice,
            string sortOrder,
            string currentFilter,
            string searchString,
            int selectedCity,
            int selectedType,
            int bedsCount,
            int? pageNumber
            )
        {

            var model = modelCreator.CreateIndexView(
                arrivalDate, departureDate,
                lowerPrice, upperPrice,
                sortOrder, currentFilter, searchString, selectedCity, selectedType, bedsCount,
                pageNumber
                );
            return View(model);
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