using System;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using ConfigurationCommon;
using LoggerCommon;
using TelegramBots;
using TelegramBots.Entities;
using TelegramBots.Services.Factories;

namespace Host
{
    class Startup
    {
        private static ILogger _logger;

        public static void Main(string[] args)
        {
            // This is global error handler
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;

            IContainer container = CreateContainer();

            var serviceLocator = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => serviceLocator);

            _logger = ServiceLocator.Current.GetInstance<ILogger>();
            _logger.Info($"{nameof(Startup)}|starting");

            var factory = ServiceLocator.Current.GetInstance<ITelegramBotsFactory>();

            // This is configuration provider
            var carInfoConfig = ServiceLocator.Current.GetInstance<ICarInfoConfiguration>();
            var englishConfig = ServiceLocator.Current.GetInstance<IEnglishConfiguration>();

            var svc = new MainService(new[]
            {
                factory.GetTelegramBot(carInfoConfig.Token, TelegramBot.CarInfoService),
                factory.GetTelegramBot(englishConfig.Token, TelegramBot.EnglishService)
            });

            svc.StartSvc();
            _logger.Info($"{nameof(Startup)}|started in console mode");
            Console.WriteLine("Press a key for exit...");
            Console.ReadKey(true);
            svc.StopSvc();
            _logger.Info($"{nameof(Startup)}|stoped");
            _logger.Shutdown();
        }

        private static IContainer CreateContainer()
        {
            // Create your builder.
            var builder = new ContainerBuilder();
            new Bootstrapper().BootStrap(builder);
            return builder.Build();
        }

        private static void UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            var ex = (Exception)args.ExceptionObject;
            _logger.Error(ex, nameof(UnhandledException));
        }
    }
}