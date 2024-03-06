using Microsoft.AspNetCore.Mvc;

namespace EnterpriceWeb.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
