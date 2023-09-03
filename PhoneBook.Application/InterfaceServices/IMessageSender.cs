using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Application.InterfaceServices
{
    public interface IMessageSender
    {
        public void SendEmail(string toEmail, string subject, string message, bool isMessageHtml = false);
    }
}
