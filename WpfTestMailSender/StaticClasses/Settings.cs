namespace WpfTestMailServer
{
    static class Settings
    {
        public static string SmtpServer { get; set; } = "smtp.mail.ru";

        public static int SmtpPort { get; set; } = 25;

        public static string FromMail { get; set; } = "test-3005@list.ru";

        public static string ToMail { get; set; } = "but-sergey@bk.ru";

        public static string SenderName { get; set; } = "test-3005@list.ru";

        public static string SenderPassword { get; set; } = "";
    }
}
