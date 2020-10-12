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
            TPLOverview.Start();

            Console.WriteLine("Главный поток работу закончил!");
            Console.ReadLine();
        }

    }
}
