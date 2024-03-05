namespace EnterpriceWeb.Mailutils
{
    public interface IEmailSender
    {
        Task SenderEmailAsync(string email,string subject,string message);
    }
}
