using Autofac;
using CarInfoTelegramBot.Services;
using CarInfoTelegramBotDbService;

namespace CarInfoTelegramBot
{
    public class Bootstrapper
    {
        public void BootStrap(ContainerBuilder builder)
        {
            builder.RegisterType<MessageProcessor>().As<IMessageProcessor>();
            builder.RegisterType<CarInfoRepository>().As<ICarInfoRepository>();
        }
    }
}