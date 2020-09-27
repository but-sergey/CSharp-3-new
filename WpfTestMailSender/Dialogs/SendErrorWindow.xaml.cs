using System.Windows;

namespace WpfTestMailServer.Dialogs
{
    public partial class SendErrorWindow : Window
    {
        public SendErrorWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
