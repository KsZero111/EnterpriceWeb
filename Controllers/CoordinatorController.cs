using Microsoft.AspNetCore.Mvc;

namespace EnterpriceWeb.Controllers
{
    public class CoordinatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
