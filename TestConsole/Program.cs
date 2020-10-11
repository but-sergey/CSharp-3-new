﻿using System;
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

            //var printer_thread = new Thread(PrintMessage)
            //{
            //    IsBackground = true,
            //    Name = "Parameter Printer"
            //};
            //printer_thread.Start("Hello World!");

            var message = "Hello World!";
            var count = 10;

            //new Thread(() => PrintMessage(message, count)) { IsBackground = true }.Start();

            var print_tast = new PrintMessageTask(message, count);
            print_tast.Start();

            //for(var i = 0; i < 10; i++)
            //{
            //    Console.WriteLine($"Главный поток {i}");
            //    Thread.Sleep(10);
            //}

            Console.WriteLine("Главный поток работу закончил!");
            Console.ReadLine();
        }

        private static void PrintMessage(string Message, int Count)
        {
            PrintThreadInfo();

            var thread_id = Thread.CurrentThread.ManagedThreadId;
            for(var i = 0; i < Count; i++)
            {
                Console.WriteLine($"id:{thread_id}\t{Message}");
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

    class PrintMessageTask
    {
        private readonly string _Message;
        private readonly int _Count;
        private Thread _Thread;

        public PrintMessageTask(string Message, int Count)
        {
            _Message = Message;
            _Count = Count;
            _Thread = new Thread(ThreadMethod) { IsBackground = true };
        }

        public void Start()
        {
            if (_Thread?.IsAlive == false)
                _Thread?.Start();
        }

        private void ThreadMethod()
        {
            var thread_id = _Thread.ManagedThreadId;
            for(var i = 0; i < _Count; i++)
            {
                Console.WriteLine($"id:{thread_id}\t{_Message}");
                Thread.Sleep(10);
            }

            _Thread = null;
        }
    }
}
