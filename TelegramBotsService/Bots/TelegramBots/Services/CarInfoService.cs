using CarInfoTelegramBot.Services;
using CommonServices;
using ServiceCommon;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TelegramBots.Services
{
    public class CarInfoService : IService
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly ICarInfoMessageProcessor _carInfoMessageProcessor;

        public CarInfoService(ITelegramBotClient telegramBotClient,
            IMessageProcessor carInfoMessageProcessor)
        {
            _telegramBotClient = telegramBotClient;
            _carInfoMessageProcessor = carInfoMessageProcessor as ICarInfoMessageProcessor;
        }

        public void Start()
        {
            var user = _telegramBotClient.GetMeAsync().Result;

            _telegramBotClient.OnMessage += OnMessage;
            _telegramBotClient.StartReceiving();
        }

        public void Stop()
        {
            _telegramBotClient.OnMessage -= OnMessage;
            _telegramBotClient.StopReceiving();
        }

        private async void OnMessage(object sender, MessageEventArgs e)
        {
            var text = e?.Message?.Text;
            var id = e.Message.Chat.Id;

            var user = e.Message.From;
            var message = await _carInfoMessageProcessor.Process(text, id);
            await _telegramBotClient.SendTextMessageAsync(id, message)
                .ConfigureAwait(false);
        }
    }
}