using System;
using System.ServiceProcess;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;

namespace CarInfoTelegramBotService
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += CurrentDomainOnUnhandledException;

            var config = new ConfigurationBuilder().AddJsonFile("config.json").Build();

            var svc = new MainService(new IService[]
            {
                new TelegramBotService(new TelegramBotClient(config["token"])),
            });

            if (Array.IndexOf(args, "console") != -1 || Array.IndexOf(args, "c") != -1)
            {
                svc.StartSvc();
                Console.WriteLine("Press a key for exit...");
                Console.ReadKey(true);
                svc.StopSvc();

            }
            else
            {
                ServiceBase.Run(svc);
            }
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            const string method = "UnhandledExceptionHandler";
            var ex = (Exception)args.ExceptionObject;
            Console.WriteLine(ex == null ? "Error!" : $"{method}\n{ex}");
        }
    }
}