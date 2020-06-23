using Autofac;
using MongoDB.Driver;
using MongoDbCommon;

namespace EnglishDbService
{
    public class Bootstrapper
    {
        public void BootStrap(ContainerBuilder builder)
        {
            builder.RegisterInstance(new MongoClient(
                    new MongoClientSettings
                    {
                        Server = new MongoServerAddress(
                            "localhost",
                            int.Parse("27017"))
                    }))
                .As<IMongoClient>().SingleInstance();

            builder.RegisterType<EnglishMongoDbConnectionSettings>().As<IConnectionSettings>().SingleInstance();
        }
    }
}