using BotCommon;
using ServiceCommon;
using Telegram.Bot;
using TelegramBots.Services;

namespace TelegramBots
{
    public class TelegramBotsFactory : ITelegramBotsFactory
    {
        private readonly IMessageProcessor _messageProcessor;

        public TelegramBotsFactory(IMessageProcessor messageProcessor)
        {
            _messageProcessor = messageProcessor;
        }

        public IService GetCarInfoService(string token)
        {
            // todo: here you need to do refactoring, it is better to use the DI container
            return new CarInfoService(new TelegramBotClient(token), _messageProcessor);
        }

        public IService GetEnglishService(string token)
        {
            return new EnglishService(new TelegramBotClient(token), _messageProcessor);
        }
    }
}