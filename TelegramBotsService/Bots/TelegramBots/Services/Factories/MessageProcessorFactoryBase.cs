using CommonServices;
using LoggerCommon;
using MongoDB.Driver;
using MongoDbCommon;

namespace TelegramBots.Services.Factories
{
    /// <summary>
    /// Provides a factory that allows you to get an instance of <see cref= "IMessageProcessor"/>
    /// that is responsible for the basic functionality of the desired bot.
    /// </summary>
    public abstract class MessageProcessorFactoryBase : IMessageProcessorFactory
    {
        /// <summary>
        /// Creates instance of the necessary Message processor factory
        /// </summary>
        /// <param name="client">The client interface to MongoDB.</param>
        /// <param name="connectionSettings">The connection string to connect to the database.</param>
        /// <param name="logger">Provides logging.</param>
        protected MessageProcessorFactoryBase(IMongoClient client, IConnectionSettings connectionSettings, ILogger logger)
        {
            Client = client;
            ConnectionSettings = connectionSettings;
            Logger = logger;
        }

        /// <summary>
        /// The client interface to MongoDB.
        /// </summary>
        protected IMongoClient Client { get; }

        /// <summary>
        /// The connection string to connect to the database.
        /// </summary>
        protected IConnectionSettings ConnectionSettings { get; }

        /// <summary>
        /// Provides logging.
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the necessary MessageProcessor
        /// </summary>
        /// <returns>Necessary MessageProcessor</returns>
        public abstract IMessageProcessor GetMessageProcessor();
    }
}