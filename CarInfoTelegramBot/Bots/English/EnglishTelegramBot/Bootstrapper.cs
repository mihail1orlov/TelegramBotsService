using Autofac;
using BotCommon;
using EnglishDbService;
using EnglishTelegramBot.Services;

namespace EnglishTelegramBot
{
    public class Bootstrapper
    {
        public void BootStrap(ContainerBuilder builder)
        {
            new EnglishDbService.Bootstrapper().BootStrap(builder);
            builder.RegisterType<MessageProcessor>().As<IMessageProcessor>();
            builder.RegisterType<EnglishRepository>().As<IEnglishRepository>();
        }
    }
}