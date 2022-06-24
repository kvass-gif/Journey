using Journey.DataAccess.Database;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        public HomeController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public IActionResult Index()
        {

            var el = applicationDbContext.Places.ToArray();
            return View(el);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}