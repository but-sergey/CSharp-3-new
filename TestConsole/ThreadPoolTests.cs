using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

            for (var i = 0; i < messages.Length; i++)
            {
                var local_i = i;
                new Thread(() => ProcessMessage(messages[local_i])) { IsBackground = true }.Start();
            }

            timer.Stop();

            Console.Title = $"Обработка заняла {timer.Elapsed.TotalSeconds} c";
        }

        private static void ProcessMessage(string message)
        {
            Console.WriteLine($"Обработка сообщения {message}");
            Thread.Sleep(5000);
            Console.WriteLine($"Обработка сообщения {message} закончена");
        }
    }
}
