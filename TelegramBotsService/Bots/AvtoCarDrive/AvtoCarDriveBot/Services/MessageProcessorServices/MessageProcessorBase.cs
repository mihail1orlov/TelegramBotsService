using System.Collections.Generic;
using System.Threading.Tasks;
using LoggerCommon;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace AvtoCarDriveBot.Services.MessageProcessorServices
{
    /// <summary>
    /// Provides processing of received messages 
    /// </summary>
    public abstract class MessageProcessorBase : IMessageProcessor
    {
        // Private fields
        private long _chatId;
        private string _userName;

        /// <summary>
        /// Provides the constructor base logic
        /// </summary>
        /// <param name="logger">Provides logging</param>
        /// <param name="telegramBotClient">A client interface to use the Telegram Bot API</param>
        protected MessageProcessorBase(ILogger logger, ITelegramBotClient telegramBotClient)
        {
            Logger = logger;
            TelegramBotClient = telegramBotClient;
        }

        /// <summary>
        /// Provides logging
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// A client interface to use the Telegram Bot API
        /// </summary>
        protected ITelegramBotClient TelegramBotClient { get; }

        /// <summary>
        /// Processes the received message
        /// </summary>
        /// <param name="message">The received message</param>
        /// <param name="currentChat">The chat currentChat</param>
        /// <param name="messageType"></param>
        /// <returns>Process message</returns>
        public abstract Task Process(Message message, Chat currentChat, MessageType messageType);

        /// <summary>
        /// Sends the text message to user
        /// </summary>
        /// <param name="message">The text of the message</param>
        /// <param name="method">The method from which the message was sent.</param>
        /// <param name="keyboard">The keyboard for the user</param>
        protected async void SendMessage(string message, string method, IReplyMarkup keyboard = default)
        {
            await TelegramBotClient.SendTextMessageAsync(_chatId, message, ParseMode.Default, false, false, 0,
                keyboard);
            Logger.Info(string.Format(Resources.Resources.MessageForLogger, method, message, _userName, _chatId));
        }

        /// <summary>
        /// Sends  the photo message to user
        /// </summary>
        /// <param name="message">The text of the message</param>
        /// <param name="method">The method from which the message was sent.</param>
        /// <param name="photo">The photo for the message</param>
        /// <param name="keyboard">The keyboard for the user</param>
        protected async void SendMessage(string message, string method, string photo, IReplyMarkup keyboard = default)
        {
            await TelegramBotClient.SendPhotoAsync(_chatId, new InputOnlineFile(photo), message, ParseMode.Default,
                false, 0, keyboard);
            Logger.Info(string.Format(Resources.Resources.MessageForLogger, method, message, _userName, _chatId));
        }

        /// <summary>
        /// Sends  the photo message to user
        /// </summary>
        /// <param name="method">The method from which the message was sent.</param>
        /// <param name="photos">The photos for the message</param>
        /// /// <param name="message">The text of the message</param>
        protected async void SendMessage(string method, IEnumerable<string> photos, string message = default)
        {
            var listOfPhotos = new List<IAlbumInputMedia>();

            foreach (var photo in photos)
            {
                listOfPhotos.Add(new InputMediaPhoto(new InputMedia(photo)));
            }

            await TelegramBotClient.SendMediaGroupAsync(listOfPhotos, _chatId);
            SendMessage(message, method);
            Logger.Info(string.Format(Resources.Resources.MessageForLogger, method, Resources.Resources.Photos,
                _userName, _chatId));
        }

        /// <summary>
        /// Init the user info
        /// </summary>
        /// <param name="currentChat">The current chat</param>
        protected void UserInit(Chat currentChat)
        {
            _chatId = currentChat.Id;
            _userName = currentChat.Username ?? currentChat.FirstName;
        }
    }
}