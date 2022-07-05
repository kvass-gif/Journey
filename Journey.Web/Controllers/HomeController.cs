using Journey.Application.Models;
using Journey.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHomeService _homeService;
        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<PlaceResponseModel> placeResponseModels = await _homeService.GetAllByListAsync();
            return View(placeResponseModels);
        }
        public async Task<IActionResult> Data(string SearchString)
        {

            return Content($"Hello {SearchString}");
        }
    }
}
