using Journey.Data;
using Journey.Entities;
using Journey.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
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
            string sortOrder,
            string currentFilter,
            string searchString,
            int selectedCity,
            int selectedType,
            int? pageNumber)
        {

            var model = modelCreator.CreateIndexView(
                arrivalDate, departureDate,
                sortOrder, currentFilter, searchString, selectedCity, selectedType, pageNumber
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