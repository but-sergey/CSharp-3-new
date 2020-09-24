using System.Net;
using System.Net.Mail;

namespace WpfTestMailServer
{
    internal sealed class EmailSendService
    {
        public void SendEmail(string MailSubject = "TestSubject", string MailBody = "TestBody")
        {
            using (MailMessage mailMessage = new MailMessage(Settings.FromMail, Settings.ToMail))
            {
                mailMessage.Subject = MailSubject;
                mailMessage.Body = MailBody;
                mailMessage.IsBodyHtml = false;

                using (SmtpClient client = new SmtpClient(Settings.SmtpServer, Settings.SmtpPort))
                {
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(Settings.SenderName, Settings.SenderPassword);

                    client.Send(mailMessage);
                }
            }
        }
    }
}
