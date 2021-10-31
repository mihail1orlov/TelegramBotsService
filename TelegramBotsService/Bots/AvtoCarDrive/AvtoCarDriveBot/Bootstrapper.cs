using Autofac;
using AvtoCarDriveBot.Services.MessageProcessorServices;

namespace AvtoCarDriveBot
{
    /// <summary>
    /// Responsible for the initialization of the necessary objects for AvtoCarDriveBot
    /// </summary>
    public class Bootstrapper
    {
        /// <summary>
        /// Registers the necessary types
        /// </summary>
        /// <param name="builder">Current container</param>
        public void BootStrap(ContainerBuilder builder)
        {
            builder.RegisterType<MessageProcessorFactory>().As<IMessageProcessorFactory>();
        }
    }
}