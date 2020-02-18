using CarInfoTelegramBot.Services;
using ServiceCommon;
using Telegram.Bot;
using TelegramBots.Services;

namespace TelegramBots
{
    public class TelegramBotsFactory : ITelegramBotsFactory
    {
        private readonly IReceiver _receiver;

        public TelegramBotsFactory(IReceiver receiver)
        {
            _receiver = receiver;
        }

        public IService GetCarInfoService(string token)
        {
            // todo: here you need to do refactoring, it is better to use the DI container
            return new CarInfoService(new TelegramBotClient(token), _receiver);
        }
    }
}