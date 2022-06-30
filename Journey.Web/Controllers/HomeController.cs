using Journey.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPlaceService _placeService;
        public HomeController(IPlaceService placeService)
        {
            _placeService = placeService;
        }
        public async Task<IActionResult> Index()
        {
            var el = await _placeService.GetAllByListAsync();
            return View(el);
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}