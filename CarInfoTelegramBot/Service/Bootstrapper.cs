using Autofac;
using CarInfoDbService;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDbCommon;
using Service.Configuration;
using Service.Constants;

namespace Service
{
    public class Bootstrapper
    {
        public void BootStrap(ContainerBuilder builder)
        {
            new TelegramBots.Bootstrapper().BootStrap(builder);

            builder.RegisterInstance(new MongoClient(
                    new MongoClientSettings
                    {
                        Server = new MongoServerAddress(
                            "localhost",
                            int.Parse("27017"))
                    }))
                .As<IMongoClient>().SingleInstance();

            builder.RegisterType<MongoDbConnectionSettings>().As<IConnectionSettings>().SingleInstance();

            // Usually you're only interested in exposing the type via its interface:
            builder.RegisterType<FileConstants>().As<IFileConstants>();
            builder.RegisterType<ConfigurationBuilder>().As<IConfigurationBuilder>();

            // However, if you want BOTH services (not as common)
            // you can say so:
            builder.RegisterType<CarInfoConfiguration>().AsSelf().As<ICarInfoConfiguration>();
        }
    }
}