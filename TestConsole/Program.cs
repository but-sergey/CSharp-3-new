﻿using System;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThreadTests.Start();
            //CriticalSectionTests.Start();
            ThreadPoolTests.Start();

            Console.WriteLine("Главный поток работу закончил!");
            Console.ReadLine();
        }
    }
}
