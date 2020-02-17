using Autofac;
using CarInfoTelegramBotService.Configuration;
using CarInfoTelegramBotService.Constants;
using Microsoft.Extensions.Configuration;

namespace CarInfoTelegramBotService
{
    public class Bootstrapper
    {
        public static IContainer GetDependencyInjectionContainer()
        {
            // Create your builder.
            var builder = new ContainerBuilder();

            TelegramBots.Bootstrapper.RegisterTypes(builder);

            // Usually you're only interested in exposing the type
            // via its interface:
            builder.RegisterType<FileConstants>().As<IFileConstants>();
            builder.RegisterType<ConfigurationBuilder>().As<IConfigurationBuilder>();

            // However, if you want BOTH services (not as common)
            // you can say so:
            builder.RegisterType<CarInfoConfiguration>().AsSelf().As<ICarInfoConfiguration>();

            return builder.Build();
        }
    }
}