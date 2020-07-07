using Autofac;
using CarInfoDbService;
using CarInfoTelegramBot.Services;

namespace CarInfoTelegramBot
{
    public class Bootstrapper
    {
        public void BootStrap(ContainerBuilder builder)
        {
            new CarInfoDbService.Bootstrapper().BootStrap(builder);
            builder.RegisterType<CarInfoMessageProcessor>().As<ICarInfoMessageProcessor>();
            builder.RegisterType<CarInfoRepository>().As<ICarInfoRepository>();
        }
    }
}