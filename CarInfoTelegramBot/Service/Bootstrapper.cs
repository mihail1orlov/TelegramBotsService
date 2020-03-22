using Autofac;
using Microsoft.Extensions.Configuration;
using Service.Configuration;
using Service.Constants;

namespace Service
{
    public class Bootstrapper
    {
        public void BootStrap(ContainerBuilder builder)
        {
            new LoggerCommon.Bootstrapper().BootStrap(builder);
            new TelegramBots.Bootstrapper().BootStrap(builder);

            // Usually you're only interested in exposing the type via its interface:
            builder.RegisterType<FileConstants>().As<IFileConstants>();
            builder.RegisterType<ConfigurationBuilder>().As<IConfigurationBuilder>();

            // However, if you want BOTH services (not as common)
            // you can say so:
            builder.RegisterType<CarInfoConfiguration>().AsSelf().As<ICarInfoConfiguration>();
        }
    }
}