using System;
using System.ServiceProcess;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CarInfoTelegramBotService.Configuration;
using Microsoft.Practices.ServiceLocation;
using TelegramBots;

namespace CarInfoTelegramBotService
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            // This is global error handler
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

            IContainer container = CreateContainer();

            var serviceLocator = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
            var factory = ServiceLocator.Current.GetInstance<ITelegramBotsFactory>();

            // This is configuration provider
            var config = ServiceLocator.Current.GetInstance<ICarInfoConfiguration>();
            
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

        private static IContainer CreateContainer()
        {
            // Create your builder.
            var builder = new ContainerBuilder();
            new Bootstrapper().BootStrap(builder);
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