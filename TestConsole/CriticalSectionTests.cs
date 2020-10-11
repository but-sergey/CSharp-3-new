using System;
using System.Threading;

namespace TestConsole
{
    static class CriticalSectionTests
    {
        public static void Start()
        {
            LockSynchronizationTest();
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
                //lock (__SyncRoot)
                //{
                //    Console.Write($"Thread id:{Thread.CurrentThread.ManagedThreadId}");
                //    Console.Write("\t");
                //    Console.Write(Message);
                //    Console.WriteLine();
                //}

                Monitor.Enter(__SyncRoot);
                try
                {
                    Console.Write($"Thread id:{Thread.CurrentThread.ManagedThreadId}");
                    Console.Write("\t");
                    Console.Write(Message);
                    Console.WriteLine();
                }
                finally
                {
                    Monitor.Exit(__SyncRoot);
                }

            }
        }
    }
}
