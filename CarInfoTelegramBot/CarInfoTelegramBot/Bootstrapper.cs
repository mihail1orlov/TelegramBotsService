using Autofac;
using CarInfoTelegramBot.Services;

namespace CarInfoTelegramBot
{
    public class Bootstrapper
    {
        public static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<Receiver>().As<IReceiver>();
        }
    }
}