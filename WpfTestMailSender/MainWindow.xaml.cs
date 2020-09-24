using System;
using System.Windows;
using WpfTestMailServer.Dialogs;

namespace WpfTestMailServer
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSendMail_Click(object sender, RoutedEventArgs e)
        {
            EmailSendService email = new EmailSendService();

            Settings.SenderName = txtLogin.Text;
            Settings.SenderPassword = passwordBox.Password;
            Settings.ToMail = txtTo.Text;

            try
            {
                email.SendEmail(txtSubject.Text, txtMailBody.Text);

                SendEndWindow sendEndWindow = new SendEndWindow();
                sendEndWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                SendErrorWindow sendErrorWindow = new SendErrorWindow();
                sendErrorWindow.ShowDialog();
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            txtLogin.Text = Settings.SenderName;
            passwordBox.Password = Settings.SenderPassword;
        }
    }
}
