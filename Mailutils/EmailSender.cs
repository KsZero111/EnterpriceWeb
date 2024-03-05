using Humanizer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Net;
using System.Net.Mail;

namespace EnterpriceWeb.Mailutils
{
    public class EmailSender : IEmailSender
    {
        public Task SenderEmailAsync(string email, string subject, string message)
        {
            string mail = "enterpriceweb04@gmail.com";
            string password = "xsufkvdvimrqxhmv";
            var client = new SmtpClient()
            {
                EnableSsl = true,
                Port = 587,
                Host = "smtp.gmail.com",
                UseDefaultCredentials =false,
                
                Credentials = new System.Net.NetworkCredential(mail,password)

            };
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(mail);
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            return client.SendMailAsync(mailMessage);
            
        }
        
    }
}
