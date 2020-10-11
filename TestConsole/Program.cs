using System;
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

            var factorial = new MathTask(() => Factorial(10));
            var sum = new MathTask(() => Sum(10));

            factorial.Start();
            sum.Start();

            Console.WriteLine($"Факториал {factorial.Result}, сумма {sum.Result}");

            Console.WriteLine("Главный поток работу закончил!");
            Console.ReadLine();
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
