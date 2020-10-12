using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //TPLOverview.Start();

            var task = AsyncAwaitTest.StartAsync();
            var process_messages_task = AsyncAwaitTest.ProcessDataTestAsync();

            Console.WriteLine("Тестовая задача запущена и мы её ждём!..");

            Task.WaitAll(task, process_messages_task);

            Console.WriteLine("Главный поток работу закончил!");
            Console.ReadLine();
        }

    }
}
