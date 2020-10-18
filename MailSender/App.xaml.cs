using MailSender.Data;
using MailSender.Data.Stores.InDB;
using MailSender.Data.Stores.InMemory;
using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using MailSender.lib.Service;
using MailSender.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Windows;

namespace MailSender
{
    public partial class App
    {
        private static IHost _Hosting;

        public static IHost Hosting => _Hosting
            ??= Host.CreateDefaultBuilder(Environment.GetCommandLineArgs())
            .ConfigureAppConfiguration(cfg => cfg
                .AddJsonFile("appconfig.json", true, true)
                .AddXmlFile("appconfig.xml", true, true)
            )
            .ConfigureLogging(log => log
                .AddConsole()
                .AddDebug()
            )
            .ConfigureServices(ConfigureServices)
            .Build();

        public static IServiceProvider Services => Hosting.Services;

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();

#if DEBUG
            services.AddTransient<IMailService, DebugMailService>();
#else
            services.AddTransient<IMailService, SmtpMailService>();
#endif

            services.AddSingleton<IEncryptorService, Rfc2898Encryptor>();

            services.AddDbContext<MailSenderDB>(opt => opt.UseSqlServer(host.Configuration.GetConnectionString("Default")));

            services.AddTransient<MailSenderDbInitilizer>();

            //services.AddSingleton<IStore<Recipient>, RecipientsStoreInMemory>();
            services.AddSingleton<IStore<Recipient>, RecipientsStoreInDB>();
            // ...
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Services.GetRequiredService<MailSenderDbInitilizer>().Initialize();
            base.OnStartup(e);
        }
    }
}
