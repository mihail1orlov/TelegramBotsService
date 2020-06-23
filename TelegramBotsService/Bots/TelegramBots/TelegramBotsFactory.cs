using ServiceCommon;
using Telegram.Bot;
using TelegramBots.Services;

namespace TelegramBots
{
    public class TelegramBotsFactory : ITelegramBotsFactory
    {
        private readonly EnglishTelegramBot.Services.IMessageProcessor _englishMessageProcessor;
        private readonly CarInfoTelegramBot.Services.IMessageProcessor _carInfoMessageProcessor;

        public TelegramBotsFactory(
            CarInfoTelegramBot.Services.IMessageProcessor carInfoCarInfoMessageProcessor,
            EnglishTelegramBot.Services.IMessageProcessor englishMessageProcessor)
        {
            _englishMessageProcessor = englishMessageProcessor;
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
    }
}