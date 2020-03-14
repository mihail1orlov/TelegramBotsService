using CarInfoCommon.Models;
using CarInfoTelegramBot.Services;
using ServiceCommon;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace TelegramBots.Services
{
    public class CarInfoService : IService
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IMessageProcessor _messageProcessor;

        public CarInfoService(ITelegramBotClient telegramBotClient,
            IMessageProcessor messageProcessor)
        {
            _telegramBotClient = telegramBotClient;
            _messageProcessor = messageProcessor;
        }

        public void Start()
        {
            User user = _telegramBotClient.GetMeAsync().Result;

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
            if (text == null)
            {
                string answer = nameof(answer);
                await _telegramBotClient.SendTextMessageAsync(e.Message.Chat.Id, answer)
                    .ConfigureAwait(false);
            }

            string s;

            if (string.Equals(text, "start"))
            {
                // todo: fake
                var carInfo = _messageProcessor.Load("72B0DF11A044482EB1568BFA289E6800");
                s = "Mileage: " + carInfo.Mileage;
            } 
            else if (int.TryParse(text, out var distance))
            {
                _messageProcessor.Save(new CarInfo(distance));
                s = "Your data was save";
            }
            else
            {
                s = "Error!\nInvalid input format";
            }

            User user = e.Message.From;

            //var s = $"Hello! {user.FirstName} {user.LastName}.\nYou said: '{text}'";
            //Console.Write(s);

            await _telegramBotClient.SendTextMessageAsync(e.Message.Chat.Id, s)
                .ConfigureAwait(false);
        }
    }
}