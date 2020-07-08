using CommonServices;

namespace TelegramBots.Services.Factories
{
    /// <summary>
    /// Provides a factory that allows you to get an instance of <see cref= "IMessageProcessor"/>
    /// that is responsible for the basic functionality of the desired bot.
    /// </summary>
    public interface IMessageProcessorFactory
    {
        /// <summary>
        /// Gets the necessary MessageProcessor
        /// </summary>
        /// <returns>Necessary MessageProcessor</returns>
        IMessageProcessor GetMessageProcessor();
    }
}