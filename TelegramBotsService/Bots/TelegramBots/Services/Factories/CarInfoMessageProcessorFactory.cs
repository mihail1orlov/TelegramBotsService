using CarInfoDbService;
using CarInfoTelegramBot.Services;
using CommonServices;
using LoggerCommon;
using MongoDB.Driver;
using MongoDbCommon;

namespace TelegramBots.Services.Factories
{
    /// <summary>
    /// Provides a factory that allows you to get an instance of <see cref= "CarInfoMessageProcessor"/>
    /// that is responsible for the basic functionality of the CarInfoBot.
    /// </summary>
    public class CarInfoMessageProcessorFactory : MessageProcessorFactoryBase
    {
        /// <summary>
        /// Creates instance of the necessary Message processor factory
        /// </summary>
        /// <param name="client">The client interface to MongoDB.</param>
        /// <param name="connectionSettings">The connection string to connect to the database.</param>
        /// <param name="logger">Provides logging.</param>
        public CarInfoMessageProcessorFactory(IMongoClient client, IConnectionSettings connectionSettings, ILogger logger) :
            base(client, connectionSettings, logger)
        {
        }

        /// <summary>
        /// Gets the CarInfoMessageProcessor
        /// </summary>
        /// <returns>Necessary MessageProcessor</returns>
        public override IMessageProcessor GetMessageProcessor()
        {
            return new CarInfoMessageProcessor(new CarInfoRepository(Client, ConnectionSettings), Logger);
        }
    }
}