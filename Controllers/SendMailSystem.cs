using EnterpriceWeb.Mailutils;
using EnterpriceWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriceWeb.Controllers
{
    public class SendMailSystem
    {
        private readonly IEmailSender _emailSender;
        public SendMailSystem(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public async void Sendgmail(User user)
        {
            var receiver = user.us_gmail;
            var subject = "please comment at the new article";
            var message = "You have 14 days to feedback for the new article";
            await _emailSender.SenderEmailAsync(receiver, subject, message);
        }
        public async void Download()
        {
            
        }
    }
}
