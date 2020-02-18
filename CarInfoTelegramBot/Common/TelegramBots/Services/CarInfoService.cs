using System;
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
        private readonly IReceiver _receiver;

        public CarInfoService(ITelegramBotClient telegramBotClient,
            IReceiver receiver)
        {
            _telegramBotClient = telegramBotClient;
            _receiver = receiver;
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
                return;
            }   

            string s;
            if (int.TryParse(text, out var distance) && _receiver.Message(new CarInfo(distance)))
            {
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