using EnterpriceWeb.Mailutils;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriceWeb.Controllers
{
    public class TestController : Controller
    {
        private readonly IEmailSender _emailSender;
        public TestController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public async Task<IActionResult> Index()
        {
            var receiver = "thanhnhan20019@gmail.com";
            var subject = "boom boom test";
            var message = "how long to sell 1 billion of sesame packets";
             await _emailSender.SenderEmailAsync(receiver, subject, message);
            return View();
        }
    }
}
