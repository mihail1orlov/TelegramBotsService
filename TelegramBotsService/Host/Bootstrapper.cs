using Autofac;
using ConfigurationCommon;
using ConfigurationCommon.Constants;
using LoggerCommon;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDbCommon;
using TelegramBots.Services.Factories;

namespace Host
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
            builder.RegisterType<EnglishConfiguration>().AsSelf().As<IEnglishConfiguration>();

            builder.RegisterType<EnglishMessageProcessorFactory>().As<IMessageProcessorFactory>().AsSelf();
            builder.RegisterType<CarInfoMessageProcessorFactory>().As<IMessageProcessorFactory>().AsSelf();

            builder.Register(ctx => new EnglishMessageProcessorFactory(ctx.Resolve<IMongoClient>(),
                ctx.Resolve<IConnectionSettings>(), ctx.Resolve<ILogger>()));
            builder.Register(ctx => new CarInfoMessageProcessorFactory(ctx.Resolve<IMongoClient>(),
                ctx.Resolve<IConnectionSettings>(), ctx.Resolve<ILogger>()));
        }
    }
}