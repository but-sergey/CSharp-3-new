using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //new Thread(() =>
            //{
            //    while (true)
            //    {
            //        Console.Title = DateTime.Now.ToString();
            //        Thread.Sleep(100);
            //    }
            //})
            //{ IsBackground = true }.Start();

            //new Task(() =>
            //{
            //    while (true)
            //    {
            //        Console.WriteLine(DateTime.Now);
            //        Thread.Sleep(100);
            //    }
            //}).Start();

            //var task = new Task(() => { });

            //var factorial = new MathTask(() => Factorial(10));
            //var sum = new MathTask(() => Sum(10));

            //factorial.Start();
            //sum.Start();

            //Console.WriteLine($"Факториал {factorial.Result}, сумма {sum.Result}");

            Action<string> printer = str => Console.WriteLine($"Сообщшение [th id:{Thread.CurrentThread.ManagedThreadId}] {str}");

            printer("Hello World!");
            printer.Invoke("123");

            ////var process_control = printer.BeginInvoke("QWE", result => { Console.WriteLine("Операция печати завершена"); }, 123);

            //var worker = new BackgroundWorker();
            //worker.DoWork += (sender, args) =>
            //{
            //    var w = (BackgroundWorker)sender;
            //    w.ReportProgress(100);
            //    w.CancelAsync();
            //};
            //worker.ProgressChanged += (s, e) => Console.WriteLine($"Прогресс {e.ProgressPercentage}");
            //worker.RunWorkerCompleted += (s, e) => Console.WriteLine("Завершено");
            //worker.RunWorkerAsync();

            /* ------------------------------------------------- */

            Parallel.Invoke(
                new ParallelOptions { MaxDegreeOfParallelism = 2 },
                ParallelInvokeMethod,
                ParallelInvokeMethod,
                ParallelInvokeMethod,
                () => Console.WriteLine("Ещё один метод..."));

            Console.WriteLine("Главный поток работу закончил!");
            Console.ReadLine();
        }

        private static void ParallelInvokeMethod()
        {
            Console.WriteLine($"ThrID: {Thread.CurrentThread.ManagedThreadId} - started");
            Thread.Sleep(250);
            Console.WriteLine($"ThrId: {Thread.CurrentThread.ManagedThreadId} - finished");
        }

        private static void ParallelInvokeMethod(string msg)
        {
            Console.WriteLine($"ThrID: {Thread.CurrentThread.ManagedThreadId} - started: {msg}");
            Thread.Sleep(250);
            Console.WriteLine($"ThrId: {Thread.CurrentThread.ManagedThreadId} - finished: {msg}");
        }

        private static int Factorial(int n)
        {
            var factorial = 1;
            for (var i = 1; i <= n; i++)
                factorial *= n;
            return factorial;
        }

        private static int Sum(int n)
        {
            var sum = 0;
            for (var i = 1; i <= n; i++)
                sum += n;
            return sum;
        }
    }

    class MathTask
    {
        private readonly Thread _CalculationThread;
        private int _Result;
        private bool _IsCompleted;

        public int Result
        {
            get
            {
                if (!_IsCompleted)
                    _CalculationThread.Join();
                return _Result;
            }
        }

        public MathTask(Func<int> Calculation)
        {
            _CalculationThread = new Thread(
                () =>
                {
                    _Result = Calculation();
                    _IsCompleted = true;
                }) { IsBackground = true };
        }

        public void Start() => _CalculationThread.Start();
    }
}
