using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace CarInfoTelegramBotService
{
    public class TelegramBotService : IService
    {
        private readonly ITelegramBotClient _telegramBotClient;

        public TelegramBotService(ITelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
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
            if (text == null)
            {
                return;
            }

            var user = e.Message.From;

            var s = $"Hello! {user.FirstName} {user.LastName}.\nYou said: '{text}'";
            Console.Write(s);

            await _telegramBotClient.SendTextMessageAsync(e.Message.Chat.Id, s)
                .ConfigureAwait(false);
        }
    }
}