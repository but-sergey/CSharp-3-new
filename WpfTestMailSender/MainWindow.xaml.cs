using System.Threading;
using System.Windows;

namespace WpfTest
{

    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private void ComputeResultButton_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                var result = GetResultHard();
                Application.Current.Dispatcher.Invoke(() => ResultText.Text = result);
            }){ IsBackground = true }.Start();
        }

        private string GetResultHard()
        {
            for(var i = 0; i < 1000; i++)
            {
                Thread.Sleep(10);
            }
            return "Hello World";
        }
    }
}
