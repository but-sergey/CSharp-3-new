using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace MailSender.lib
{
    public class MailSenderService
    {
        public string ServerAddress { get; set; }
        public int ServerPort { get; set; }
        public bool UseSSL { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public void SendMessage(string SenderAddress, string RecipientAddress, string Subject, string Body)
        {
        }
    }
}
