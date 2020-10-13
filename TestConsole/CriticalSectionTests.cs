﻿using System;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace TestConsole
{
    static class CriticalSectionTests
    {
        public static void Start()
        {
            //LockSynchronizationTest();

            var manual_reset_event = new ManualResetEvent(false);
            var auto_reset_event = new AutoResetEvent(false);

            EventWaitHandle starter = manual_reset_event;

            for(var i = 0; i < 10; i++)
            {
                var local_i = i;
                new Thread(() =>
                {
                    Console.WriteLine($"Поток {local_i} запущен");
                    starter.WaitOne();
                    starter.Reset();
                    Console.WriteLine($"Поток {local_i} завершил свою работу");
                }).Start();
            }

            Console.WriteLine("Все потоки созданы и готовы к работе.");
            Console.ReadLine();
            starter.Set();

            Console.ReadLine();

            //var mutex1 = new Mutex(true, "Тестовый мютекс", out var created1);
            //var mutex2 = new Mutex(true, "Тестовый мютекс", out var created2);

            //mutex1.WaitOne();
            //mutex1.WaitOne();
            //mutex1.ReleaseMutex();

            //var semaphore = new Semaphore(0, 10);

            //semaphore.WaitOne();

            //semaphore.Release();
        }

        private static void LockSynchronizationTest()
        {
            var threads = new Thread[10];

            for (var i = 0; i < threads.Length; i++)
            {
                var local_i = i;
                threads[i] = new Thread(() => PrintData($"Message from thread {local_i}", 10));
            }

            for (var i = 0; i < threads.Length; i++)
                threads[i].Start();
        }

        private static readonly object __SyncRoot = new object();

        private static void PrintData(string Message, int Count)
        {
            for(var i = 0; i < Count; i++)
            {
                lock (__SyncRoot)
                {
                    Console.Write($"Thread id:{Thread.CurrentThread.ManagedThreadId}");
                    Console.Write("\t");
                    Console.Write(Message);
                    Console.WriteLine();
                }

                //Monitor.Enter(__SyncRoot);
                //try
                //{
                //    Console.Write($"Thread id:{Thread.CurrentThread.ManagedThreadId}");
                //    Console.Write("\t");
                //    Console.Write(Message);
                //    Console.WriteLine();
                //}
                //finally
                //{
                //    Monitor.Exit(__SyncRoot);
                //}

            }
        }
    }

    [Synchronization]
    public class FileLogger : ContextBoundObject
    {
        private readonly string _LogFileName;
        public FileLogger(string LogFileName)
        {
            _LogFileName = LogFileName;
        }

        public void Log(string Message)
        {
            File.WriteAllText(_LogFileName, Message);
        }
    }
}
