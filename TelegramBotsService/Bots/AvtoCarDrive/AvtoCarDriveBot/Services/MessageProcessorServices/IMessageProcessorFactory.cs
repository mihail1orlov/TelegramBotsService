namespace AvtoCarDriveBot.Services.MessageProcessorServices
{
    /// <summary>
    /// Provides the necessary message processor
    /// </summary>
    public interface IMessageProcessorFactory
    {
        /// <summary>
        /// Gets the necessary message processor
        /// </summary>
        /// <typeparam name="T">The type of the necessary message processor</typeparam>
        /// <returns>Necessary message processor</returns>
        IMessageProcessor GetMessageProcessor<T>() where T : IMessageProcessor;
    }
}