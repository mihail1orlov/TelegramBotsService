using Autofac;

namespace TelegramBots
{
    public class Bootstrapper
    {
        public static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<TelegramBotsFactory>().As<ITelegramBotsFactory>();
        }
    }
}