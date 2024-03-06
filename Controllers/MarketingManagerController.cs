using Microsoft.AspNetCore.Mvc;

namespace EnterpriceWeb.Controllers
{
    public class MarketingManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
