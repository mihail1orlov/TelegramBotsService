using System;
using System.ServiceProcess;
using Autofac;
using Microsoft.Extensions.Configuration;
using TelegramBots;

namespace CarInfoTelegramBotService
{
    class Startup
    {
        private static IContainer Container { get; set; }
        static void Main(string[] args)
        {
            // This is global error handler
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

            // This is configuration provider 
            var config = new ConfigurationBuilder().AddJsonFile("config.json").Build();

            Container = GetDependencyInjectionContainer();

            // Create the scope
            // use it, then dispose of the scope.
            using var scope = Container.BeginLifetimeScope();

            // resolve your ITelegramBotsFactory
            var factory = scope.Resolve<ITelegramBotsFactory>();

            //ITelegramBotsFactory factory = new TelegramBotsFactory();
            var svc = new MainService(new[]
            {
                factory.GetCarInfoService(config["token"])
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

        private static IContainer GetDependencyInjectionContainer()
        {
            // Create your builder.
            var builder = new ContainerBuilder();

            // Usually you're only interested in exposing the type
            // via its interface:
            builder.RegisterType<ConfigurationBuilder>().As<IConfigurationBuilder>();

            // However, if you want BOTH services (not as common)
            // you can say so:
            builder.RegisterType<TelegramBotsFactory>().AsSelf().As<ITelegramBotsFactory>();
            
            return builder.Build();
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            const string method = "UnhandledExceptionHandler";
            var ex = (Exception)args.ExceptionObject;
            Console.WriteLine(ex == null ? "Error!" : $"{method}\n{ex}");
        }
    }
}