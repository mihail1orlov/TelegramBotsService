using Autofac;
using TelegramBots.Services.Factories;

namespace TelegramBots
{
    public class Bootstrapper
    {
        public void BootStrap(ContainerBuilder builder)
        {
            new CarInfoTelegramBot.Bootstrapper().BootStrap(builder);
            new EnglishTelegramBot.Bootstrapper().BootStrap(builder);

            builder.RegisterType<TelegramBotsFactory>().As<ITelegramBotsFactory>();
        }
    }
}