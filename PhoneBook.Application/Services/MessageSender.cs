using Microsoft.Extensions.Options;
using PhoneBook.Application.InterfaceServices;
using PhoneBook.Application.Models;
using System.Net;
using System.Net.Mail;

namespace PhoneBook.Application.Services
{

    public class MessageSender : IMessageSender
    {

        #region constructor

        private readonly EmailInformationModel _EmailInformationOptions;


        public MessageSender(IOptions<EmailInformationModel> emailInformationOptions)
        {
            _EmailInformationOptions = emailInformationOptions.Value;
        }

        #endregion



        public void SendEmail(string toEmail, string subject, string message, bool isMessageHtml = false)
        {
            using (var client = new SmtpClient())
            {
                var credentials = new NetworkCredential()
                {
                    UserName = _EmailInformationOptions.Address, 
                    Password = _EmailInformationOptions.Password
                };
                client.UseDefaultCredentials = false;
                client.Credentials = credentials;
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;

                using var emailMessage = new MailMessage()
                {
                    To = { new MailAddress(toEmail) },
                    From = new MailAddress("unknown.2380.unknown@gmail.com"), 
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = isMessageHtml
                };

                client.Send(emailMessage);
            }
            return;
        }
    }

}
