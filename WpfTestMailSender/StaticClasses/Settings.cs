namespace WpfTestMailServer
{
    static class Settings
    {
        public static string SmtpServer = "smtp.mail.ru";
        //public static string SmtpServer = "smtp.yandex.ru";

        public static int SmtpPort = 25;
        //public static int SmtpPort = 465;

        //public static string FromMail = "asketemius@yandex.ru";
        public static string FromMail = "test-3005@list.ru";

        public static string ToMail = "but-sergey@bk.ru";
        //public static string ToMail = "asketemius@yandex.ru";

        public static string SenderName = "test-3005@list.ru";
        //public static string SenderName = "but-sergey@bk.ru";
        //public static string SenderName = "asketemius";

        public static string SenderPassword = "";
    }
}
