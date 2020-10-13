using System.Threading;
using System.Windows;
using System.Windows.Threading;

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
                UpdateResultValue(result);
            }){ IsBackground = true }.Start();
        }

        private void UpdateResultValue(string Result)
        {
            if (Dispatcher.CheckAccess())
                ResultText.Text = Result;
            else
                Dispatcher.Invoke(() => UpdateResultValue(Result));
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
