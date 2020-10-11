using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace TestConsole
{
    static class ThreadPoolTests
    {
        public static void Start()
        {
            var messages = Enumerable.Range(1, 1000)
                .Select(i => $"Message {i}")
                .ToArray();

            var timer = Stopwatch.StartNew();

            //ThreadPool.GetAvailableThreads(out var available_worker_threads, out var available_completion_threads);
            //ThreadPool.GetMinThreads(out var min_worker_threads, out var min_completion_threads);
            //ThreadPool.GetMaxThreads(out var max_worker_threads, out var max_completion_threads);

            //ThreadPool.SetMinThreads(4, 4);
            //ThreadPool.SetMaxThreads(16, 16);

            for (var i = 0; i < messages.Length; i++)
            {
                //var local_i = i;
                //new Thread(() => ProcessMessage(messages[local_i])) { IsBackground = true }.Start();
                ThreadPool.QueueUserWorkItem(o => ProcessMessage((string)o),  messages[i]);
            }

            timer.Stop();

            Console.Title = $"Обработка заняла {timer.Elapsed.TotalSeconds} c";
        }

        private static void ProcessMessage(string message)
        {
            Console.WriteLine($"Обработка сообщения {message}");
            //Thread.Sleep(5000);
            Console.WriteLine($"Обработка сообщения {message} закончена");
        }
    }
}
