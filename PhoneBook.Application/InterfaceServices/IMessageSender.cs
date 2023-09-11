namespace PhoneBook.Application.InterfaceServices
{
    public interface IMessageSender
    {
        public void SendEmail(string toEmail, string subject, string message, bool isMessageHtml = false);
    }
}
