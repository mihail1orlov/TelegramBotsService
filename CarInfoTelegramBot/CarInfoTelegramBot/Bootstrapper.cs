using Autofac;
using CarInfoTelegramBot.Services;

namespace CarInfoTelegramBot
{
    public class Bootstrapper
    {
        public void BootStrap(ContainerBuilder builder)
        {
            builder.RegisterType<Receiver>().As<IReceiver>();
            builder.RegisterType<Transmitter>().As<ITransmitter>();
            builder.RegisterType<FileWriter>().As<IRepository>();
        }
    }
}