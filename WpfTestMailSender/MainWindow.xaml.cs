using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace WpfTest
{

    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private async void OnOpenFileClick(object sender, RoutedEventArgs e)
        {
            await Task.Yield();

            var dialog = new OpenFileDialog
            {
                Title = "Выбор файла для чтения",
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() != true) return;

            //var count = dict.Count;
            //Result.Text = $"Число слов {dict.Count}";
            //Result.Dispatcher.Invoke(() => Result.Text = $"Число слов {dict.Count}");

            StartAction.IsEnabled = false;
            CancelAction.IsEnabled = true;

            _ReadingFileCancelation = new CancellationTokenSource();

            var cancel = _ReadingFileCancelation.Token;

            IProgress<double> progress = new Progress<double>(p => ProgressInfo.Value = p);

            try
            {
                var count = await GetWordsCountAsync(dialog.FileName, progress, cancel);
                Result.Text = $"Число слов {count}";
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("Операция чтения файла была отменена");
                Result.Text = "---";
                progress.Report(0);
            }

            StartAction.IsEnabled = true;
            CancelAction.IsEnabled = false;
        }

        private static async Task<int> GetWordsCountAsync(string FileName, IProgress<double> Progress = null, CancellationToken Cancel = default)
        {
            var dict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            Cancel.ThrowIfCancellationRequested();
            using (var reader = new StreamReader(FileName))
            {
                while (!reader.EndOfStream)
                {
                    Cancel.ThrowIfCancellationRequested();
                    var line = await reader.ReadLineAsync().ConfigureAwait(false);
                    var words = line.Split(' ');
                    //Thread.Sleep(100);
                    await Task.Delay(1);

                    foreach (var word in words)
                        if (dict.ContainsKey(word))
                            dict[word]++;
                        else
                            dict.Add(word, 1);

                    Progress?.Report(reader.BaseStream.Position / (double)reader.BaseStream.Length);
                }
            }

            return dict.Count;
        }

        private CancellationTokenSource _ReadingFileCancelation;

        private void OnCancelReadingClick(object sender, RoutedEventArgs e)
        {
            _ReadingFileCancelation?.Cancel();
        }
    }
}
