using MailSender.lib.Reports;
using System;
using System.Threading.Tasks;

namespace TestConsoleCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var report = new StatisticReport();

            report.SendedMessagesCount = 1000;

            report.CreatePackage("statistic.docx");

            //Console.ReadLine();
        }
    }
}
