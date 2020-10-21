using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole
{
    static class AsyncAwaitTest
    {
        private static void PrintThreadInfo(string Message = "")
        {
            var current_thread = Thread.CurrentThread;
            Console.WriteLine($"Thread id: {current_thread.ManagedThreadId}. Task id: {Task.CurrentId}. {Message}");
        }

        public static async Task StartAsync()
        {
            Console.WriteLine("Запуск асинхронного метода выполняется синхронно!!!");
            PrintThreadInfo("При входе в метод StartAsync");

            //var result_task = GetStringResultAsync();
            //var result = await result_task;
            var result = await GetStringResultRealyAsync();

            Console.WriteLine($"Был получен результат {result}");
            PrintThreadInfo("При печати результата");

            var result2 = await GetStringResultRealyAsync();

            Console.WriteLine($"Был получен результат {result2}");
            PrintThreadInfo("При печати результата");

            for(var i = 0; i < 10; i++)
            {
                var result3 = await GetStringResultRealyAsync();

                Console.WriteLine($"Был получен результат {result3}");
                PrintThreadInfo("При печати результата");
            }
        }

        private static async Task<string> GetStringResultAsync()
        {
            PrintThreadInfo("В псевдоасинхронном методе");
            return DateTime.Now.ToString();
            //return Task.FromResult(DateTime.Now.ToString()); // без async
        }

        private static Task<string> GetStringResultRealyAsync()
        {
            PrintThreadInfo("В начале асинхронного метода");
            return Task.Run(() =>
            {
                PrintThreadInfo("Внутри асинхронного метода");
                return DateTime.Now.ToString();
            });
        }

        public static async Task ProcessDataTestAsync()
        {
            var messages = Enumerable.Range(1, 500).Select(i => $"Message {i}");

            var tasks = messages.Select(msg => Task.Run(() => LowSpeedPrinter(msg)));

            Console.WriteLine(">>> Подготовка к запуску обработки сообщений...");

            var running_tasks = tasks.ToArray();
            
            Console.WriteLine(">>> Задачи созданы");

            await Task.WhenAll(running_tasks);

            Console.WriteLine(">>> Обработка всех сообщений завершена");

        }

        private static void LowSpeedPrinter(string msg)
        {
            Console.WriteLine($">>> [Thread id {Thread.CurrentThread.ManagedThreadId}] Начинаю обработку {msg}...");
            Thread.Sleep(100);
            Console.WriteLine($">>> [Thread id {Thread.CurrentThread.ManagedThreadId}] Сообщение {msg} обработано!");
        }
    }
}
