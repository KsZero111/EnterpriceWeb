using EnterpriceWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriceWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbConText _dbConText;
        public AdminController(AppDbConText appDbConText)
        {
            _dbConText = appDbConText;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
