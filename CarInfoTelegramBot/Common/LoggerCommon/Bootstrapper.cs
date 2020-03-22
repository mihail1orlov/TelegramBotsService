using Autofac;

namespace LoggerCommon
{
    public class Bootstrapper
    {
        public void BootStrap(ContainerBuilder builder)
        {
            var instance = NLog.LogManager.GetCurrentClassLogger();
            builder.RegisterInstance(instance).As<NLog.ILogger>();
            builder.RegisterType<Logger>().As<ILogger>();
        }
    }
}