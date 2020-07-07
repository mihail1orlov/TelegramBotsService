using Autofac;
using EnglishDbService;
using EnglishTelegramBot.Services;

namespace EnglishTelegramBot
{
    public class Bootstrapper
    {
        public void BootStrap(ContainerBuilder builder)
        {
            new EnglishDbService.Bootstrapper().BootStrap(builder);
            builder.RegisterType<EnglishMessageProcessor>().As<IEnglishMessageProcessor>();
            builder.RegisterType<EnglishRepository>().As<IEnglishRepository>();
        }
    }
}