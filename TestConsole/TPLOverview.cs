using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole
{
    static class TPLOverview
    {
        public static void Start()
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

            Action<string> printer = str =>
            {
                Console.WriteLine($"Сообщшение [th id:{Thread.CurrentThread.ManagedThreadId}] {str}");
                Thread.Sleep(100);
            };

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

            //Parallel.Invoke(
            //    new ParallelOptions { MaxDegreeOfParallelism = 2 },
            //    ParallelInvokeMethod,
            //    ParallelInvokeMethod,
            //    ParallelInvokeMethod,
            //    () => Console.WriteLine("Ещё один метод..."));

            //Parallel.For(0, 100, i => printer(i.ToString()));
            //Parallel.For(0, 100, new ParallelOptions { MaxDegreeOfParallelism = 2 }, i => printer(i.ToString()));
            //var result = Parallel.For(0, 100, new ParallelOptions { MaxDegreeOfParallelism = 2 }, (i, state) =>
            //{
            //    printer(i.ToString());
            //    if (i > 10) state.Break();
            //});
            //Console.WriteLine($"Выполнено {result.LowestBreakIteration} итераций");

            var messages = Enumerable.Range(1, 500).Select(i => $"Message {i}");//.ToArray();

            //Parallel.ForEach(messages, message => printer(message));
            //Parallel.ForEach(messages, new ParallelOptions { MaxDegreeOfParallelism = 2 }, message => printer(message));

            //foreach (var message in messages.Where(msg => msg.EndsWith("0")))
            //    printer(message);

            //messages
            //    .Where(msg => msg.EndsWith("0"))
            //    .ToList()
            //    .ForEach(msg => printer(msg));

            //var cancelation = new CancellationTokenSource();
            ////cancelation.Token.ThrowIfCancellationRequested();
            //var message_count = messages
            //    .AsParallel()
            //    .WithDegreeOfParallelism(2)
            //    .WithCancellation(cancelation.Token)
            //    .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
            //    .Where(msg =>
            //    {
            //        printer(msg);
            //        return msg.EndsWith("0");
            //    })
            //    .AsSequential()
            //    .Count();

            //var task = new Task(() => printer("Hello World!"));
            //task.Start();
            //var continustion_task = task.ContinueWith(t => Console.WriteLine($"Задача {t.Id} завершилась"), TaskContinuationOptions.OnlyOnRanToCompletion);
            //continustion_task.ContinueWith(t => { }, TaskContinuationOptions.OnlyOnFaulted);

            var printer_task = Task.Run(() => printer("Hello World!"));
            //var printer_task2 = Task.Factory.StartNew(obj => printer((string)obj), "Hello World!");

            //printer_task.Wait(); // не рекомендуется к использовании из-за вероятности мертвой блокировки
            //var tesult_task = new Task<int>(() =>
            //{
            //    Thread.Sleep(100);
            //    return 42;
            //});

            var result_task = Task.Run(() =>
            {
                Thread.Sleep(100);
                return 42;
            });

            var result_task2 = Task.Run(() =>
            {
                Thread.Sleep(500);
                return 13;
            });

            var result_task3 = Task.Run(() =>
            {
                Thread.Sleep(10);
                return 13;
            });

            var result = result_task.Result;

            Task.WaitAll(result_task, result_task2, result_task3);
            var index = Task.WaitAny(result_task, result_task2, result_task3);

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
}
