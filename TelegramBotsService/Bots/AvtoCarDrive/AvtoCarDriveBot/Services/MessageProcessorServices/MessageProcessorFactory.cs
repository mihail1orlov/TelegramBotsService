using AvtoCarDriveBot.Services.AdminIdsServices;
using AvtoCarDriveBot.Services.CarServices;
using LoggerCommon;
using Telegram.Bot;

namespace AvtoCarDriveBot.Services.MessageProcessorServices
{
    /// <summary>
    /// Provides the necessary message processor
    /// </summary>
    public class MessageProcessorFactory : IMessageProcessorFactory
    {
        // Private fields
        private readonly ILogger _logger;
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IAdminIdsService _adminIdsService;
        private readonly ICarService _carService;

        /// <summary>
        /// Creates instance of <see cref="MessageProcessor"/>
        /// </summary>
        /// <param name="logger">Provides logging</param>
        /// <param name="telegramBotClient">A client interface to use the Telegram Bot API</param>
        /// <param name="adminIdsService">Provides processing of the list of admins</param>
        /// <param name="carService">Provides working on the available cars</param>
        public MessageProcessorFactory(ILogger logger, ITelegramBotClient telegramBotClient,
            IAdminIdsService adminIdsService, ICarService carService)
        {
            _logger = logger;
            _telegramBotClient = telegramBotClient;
            _adminIdsService = adminIdsService;
            _carService = carService;
        }

        /// <summary>
        /// Gets the necessary message processor
        /// </summary>
        /// <typeparam name="T">The type of the necessary message processor</typeparam>
        /// <returns>Necessary message processor</returns>
        public IMessageProcessor GetMessageProcessor<T>() where T: IMessageProcessor
        {
            switch (typeof(T).Name)
            {
                case "AdminMessageProcessor":
                    return new AdminMessageProcessor(_logger, _telegramBotClient, _adminIdsService, _carService);
                case "UserMessageProcessor":
                    return new UserMessageProcessor(_logger, _telegramBotClient, _carService);
            }

            return new UserMessageProcessor(_logger, _telegramBotClient, _carService);
        }
    }
}