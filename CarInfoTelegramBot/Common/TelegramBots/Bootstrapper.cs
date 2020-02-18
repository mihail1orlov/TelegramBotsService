using Autofac;

namespace TelegramBots
{
    public class Bootstrapper
    {
        public static void RegisterTypes(ContainerBuilder builder)
        {
            CarInfoTelegramBot.Bootstrapper.RegisterTypes(builder);

            builder.RegisterType<TelegramBotsFactory>().As<ITelegramBotsFactory>();
        }
    }
}