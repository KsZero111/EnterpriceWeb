namespace EnterpriceWeb.Mailutils
{
    public interface IEmailSender
    {
        Task<string> SenderEmailAsync(string email,string subject,string message);
    }
}
