using System;
using System.Threading;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var main_thread = Thread.CurrentThread;
            var main_thread_id = main_thread.ManagedThreadId;

            main_thread.Name = "Главный поток!";

            //TimerMethod();
            var timer_thread = new Thread(TimerMethod);
            timer_thread.Name = "Поток часов";
            timer_thread.IsBackground = true;
            timer_thread.Start();

            var printer_thread = new Thread(PrintMessage)
            {
                IsBackground = true,
                Name = "Parameter Printer"
            };
            printer_thread.Start("Hello World!");

            //for(var i = 0; i < 10; i++)
            //{
            //    Console.WriteLine($"Главный поток {i}");
            //    Thread.Sleep(10);
            //}

            Console.WriteLine("Главный поток работу закончил!");
            Console.ReadLine();
        }

        private static void PrintMessage(object parameter)
        {
            PrintThreadInfo();

            var msg = (string)parameter;
            var thread_id = Thread.CurrentThread.ManagedThreadId;
            for(var i = 0; i < 10; i++)
            {
                Console.WriteLine($"id:{thread_id}\t{msg}");
                Thread.Sleep(10);
            }
        }

        private static void TimerMethod()
        {
            PrintThreadInfo();

            while (true)
            {
                Console.Title = DateTime.Now.ToString("HH:mm:ss.ffff");
                Thread.Sleep(100);
                //Thread.SpinWait(10);
            }
        }

        private static void PrintThreadInfo()
        {
            var thread = Thread.CurrentThread;
            Console.WriteLine($"id:{thread.ManagedThreadId}; name:{thread.Name}; priority:{thread.Priority}");
        }
    }
}
