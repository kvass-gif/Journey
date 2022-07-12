using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Web.Controllers
{
    [Authorize(Policy = "LandLordOnly")]
    public class LandlordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
