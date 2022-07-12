using Journey.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Journey.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHomeService _homeService;
        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> PlacesByName(string placeName)
        {
            var placeResponseModels = await _homeService.GetAllByListAsync(placeName);
            return Json(JsonSerializer.Serialize(placeResponseModels));
        }
        public async Task<IActionResult> AllPlaces()
        {
            var placeResponseModels = await _homeService.GetAllByListAsync();
            return Json(JsonSerializer.Serialize(placeResponseModels));
        }
    }
}
