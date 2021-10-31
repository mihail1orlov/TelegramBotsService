using System;
using GitHubNotificatorBot.Services;
using ServiceCommon;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TelegramBots.Services
{
    /// <summary>
    /// Provides a windows service for GitHubNotificator bot
    /// </summary>
    public class GitHubNotificatorService : Exception, IService
    {
        // Private fields
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IMessageProcessor _messageProcessor;

        /// <summary>
        /// Creates instance of <see cref="GitHubNotificatorService"/>
        /// </summary>
        /// <param name="telegramBotClient"></param>
        /// <param name="messageProcessor"></param>
        public GitHubNotificatorService(ITelegramBotClient telegramBotClient,
            IMessageProcessor messageProcessor)
        {
            _telegramBotClient = telegramBotClient;
            _messageProcessor = messageProcessor;
        }

        /// <summary>
        /// Starts the windows service
        /// </summary>
        public void Start()
        {
            // todo: remove unused variable
            var user = _telegramBotClient.GetMeAsync().Result;

            _telegramBotClient.OnMessage += OnMessage;
            _telegramBotClient.StartReceiving();
        }

        /// <summary>
        /// Stops the windows service
        /// </summary>
        public void Stop()
        {
            _telegramBotClient.OnMessage -= OnMessage;
            _telegramBotClient.StopReceiving();
        }

        /// <summary>
        /// Event handler when sending a message to the telegram bot.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="messageEventArgs"><see cref="T:System.EventArgs" /> containing a <see cref="T:Telegram.Bot.Types.Message" /></param>
        private async void OnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            var text = messageEventArgs.Message.Text;
            var id = messageEventArgs.Message.Chat.Id;

            var user = messageEventArgs.Message.From;
            await _messageProcessor.Process(text, id);
        }
    }
}