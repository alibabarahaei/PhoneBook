using PhoneBook.Application.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.Application.Models;
using Microsoft.Extensions.Options;

namespace PhoneBook.Application.Services
{

    public class MessageSender : IMessageSender
    {

        private readonly EmailInformationModel _EmailInformationOptions;


        public MessageSender(IOptions<EmailInformationModel> emailInformationOptions)
        {
            _EmailInformationOptions = emailInformationOptions.Value;
        }


        public void SendEmail(string toEmail, string subject, string message, bool isMessageHtml = false)
        {

            using (var client = new SmtpClient())
            {

                var credentials = new NetworkCredential()
                {
                    UserName = _EmailInformationOptions.Address, // without @gmail.com
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
                    From = new MailAddress("unknown.2380.unknown@gmail.com"), // with @gmail.com
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
