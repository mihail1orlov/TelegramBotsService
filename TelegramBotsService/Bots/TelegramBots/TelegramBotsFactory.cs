using ServiceCommon;
using Telegram.Bot;
using TelegramBots.Services;

namespace TelegramBots
{
    public class TelegramBotsFactory : ITelegramBotsFactory
    {
        private readonly EnglishTelegramBot.Services.IEnglishMessageProcessor _englishEnglishMessageProcessor;
        private readonly CarInfoTelegramBot.Services.ICarInfoMessageProcessor _carInfoCarInfoMessageProcessor;

        public TelegramBotsFactory(
            CarInfoTelegramBot.Services.ICarInfoMessageProcessor carInfoCarInfoCarInfoMessageProcessor,
            EnglishTelegramBot.Services.IEnglishMessageProcessor englishEnglishMessageProcessor)
        {
            _englishEnglishMessageProcessor = englishEnglishMessageProcessor;
            _carInfoCarInfoMessageProcessor = carInfoCarInfoCarInfoMessageProcessor;
        }

        public IService GetCarInfoService(string token)
        {
            // todo: here you need to do refactoring, it is better to use the DI container
            return new CarInfoService(new TelegramBotClient(token), _carInfoCarInfoMessageProcessor);
        }

        public IService GetEnglishService(string token)
        {
            return new EnglishService(new TelegramBotClient(token), _englishEnglishMessageProcessor);
        }
    }
}