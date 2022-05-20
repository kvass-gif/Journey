using Journey.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Controllers
{
    [Authorize(Policy = "TenantOnly")]
    public class TenantController : Controller
    {
        private readonly UnitOfWork unitOfWork;

        public TenantController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            if (User.Identity == null || User.Identity.Name == null)
            {
                return Content("User.Identity == null");
            }
            string name = User.Identity.Name;
            var tenant = unitOfWork.AccountRepo.FindByNameIncludeTenant(name);
            if (tenant == null)
            {
                return Content("tenant == null");
            }
            return View(tenant);
        }
    }
}
