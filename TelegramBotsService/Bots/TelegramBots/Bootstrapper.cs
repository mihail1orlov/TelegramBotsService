using Autofac;

namespace TelegramBots
{
    public class Bootstrapper
    {
        public void BootStrap(ContainerBuilder builder)
        {
            new CarInfoTelegramBot.Bootstrapper().BootStrap(builder);
            new EnglishTelegramBot.Bootstrapper().BootStrap(builder);
            new GitHubNotificatorBot.Bootstrapper().BootStrap(builder);
            new AvtoCarDriveBot.Bootstrapper().BootStrap(builder);
            builder.RegisterType<TelegramBotsFactory>().As<ITelegramBotsFactory>();
        }
    }
}