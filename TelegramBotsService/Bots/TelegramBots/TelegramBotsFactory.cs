using AvtoCarDriveBot.Services;
using AvtoCarDriveBot.Services.AdminIdsServices;
using AvtoCarDriveBot.Services.CarServices;
using AvtoCarDriveBot.Services.MessageProcessorServices;
using LoggerCommon;
using ServiceCommon;
using Telegram.Bot;
using TelegramBots.Services;

namespace TelegramBots
{
    public class TelegramBotsFactory : ITelegramBotsFactory
    {
        private readonly EnglishTelegramBot.Services.IMessageProcessor _englishMessageProcessor;
        private readonly CarInfoTelegramBot.Services.IMessageProcessor _carInfoMessageProcessor;
        private readonly ILogger _logger;

        public TelegramBotsFactory(
            CarInfoTelegramBot.Services.IMessageProcessor carInfoCarInfoMessageProcessor,
            EnglishTelegramBot.Services.IMessageProcessor englishMessageProcessor,
            ILogger logger)
        {
            _englishMessageProcessor = englishMessageProcessor;
            _logger = logger;
            _carInfoMessageProcessor = carInfoCarInfoMessageProcessor;
        }

        public IService GetCarInfoService(string token)
        {
            // todo: here you need to do refactoring, it is better to use the DI container
            return new CarInfoService(new TelegramBotClient(token), _carInfoMessageProcessor);
        }

        public IService GetEnglishService(string token)
        {
            return new EnglishService(new TelegramBotClient(token), _englishMessageProcessor);
        }

        public IService GetGitHubNotificatorService(string token)
        {
            var telegramBotClient = new TelegramBotClient(token);
            return new GitHubNotificatorService(telegramBotClient, new GitHubNotificatorBot.Services.MessageProcessor(_logger, telegramBotClient));
        }

        public IService GetAvtoCarDriveService(string token)
        {
            var telegramBotClient = new TelegramBotClient(token);
            var adminIdsService = new AdminIdsService();
            var carService = new CarService();
            var messageProcessorFactory = new MessageProcessorFactory(_logger, telegramBotClient, adminIdsService, carService);
            var messageTypeIdentifier = new MessageTypeIdentifier();
            return new AvtoCarDriveService(new TelegramBotClient(token),
                new MessageProcessor(_logger, telegramBotClient, messageProcessorFactory, adminIdsService,
                    messageTypeIdentifier));
        }
    }
}