using Journey.Data;
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
        public IActionResult Index()
        {

            var arr = unitOfWork.PlaceRepository.GetAll().ToArray();
            return View(arr);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = unitOfWork.PlaceRepository.Find((int)id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

    }
}