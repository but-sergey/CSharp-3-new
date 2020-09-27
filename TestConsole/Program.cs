using System;
using System.Net;
using System.Net.Mail;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            MailMessage mailMessage = new MailMessage(Settings.FromMail, Settings.ToMail);
            mailMessage.Subject = "Пробное письмо";
            mailMessage.Body = "Содержимое пробного письма";
            mailMessage.IsBodyHtml = false;

            SmtpClient client = new SmtpClient(Settings.SmtpServer, Settings.SmtpPort);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            var password = Settings.SenderPassword;
            if (password == string.Empty)
            {
                Console.Write("Пароль для доступа к аккаунту отправки писем: ");
                password = Console.ReadLine();
            }
            client.Credentials = new NetworkCredential(Settings.SenderName, password);

            try
            {
                client.Send(mailMessage);
                Console.WriteLine($"Письмо от {Settings.FromMail} на адрес {Settings.ToMail} отправлено.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Невозможно отправить письмо ({ex.Message})");
            }

            Console.Write("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
