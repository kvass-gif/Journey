using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Web.Controllers
{
    public class LandLordController : Controller
    {
        [Authorize(Policy = "LandLordOnly")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
