using System;
using System.ServiceProcess;
using Autofac;
using CarInfoTelegramBotService.Configuration;
using CarInfoTelegramBotService.Constants;
using Microsoft.Extensions.Configuration;
using TelegramBots;

namespace CarInfoTelegramBotService
{
    public class Startup
    {
        private static IContainer Container { get; set; }
        public static void Main(string[] args)
        {
            // This is global error handler
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

            Container = GetDependencyInjectionContainer();

            // Create the scope
            // use it, then dispose of the scope.
            using var scope = Container.BeginLifetimeScope();

            // resolve your ITelegramBotsFactory
            var factory = scope.Resolve<ITelegramBotsFactory>();

            // This is configuration provider
            var config = scope.Resolve<ICarInfoConfiguration>();
            
            var svc = new MainService(new[]
            {
                factory.GetCarInfoService(config.Token)
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
            builder.RegisterType<FileConstants>().As<IFileConstants>();
            builder.RegisterType<ConfigurationBuilder>().As<IConfigurationBuilder>();
            builder.RegisterType<CarInfoConfiguration>().As<ICarInfoConfiguration>();

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