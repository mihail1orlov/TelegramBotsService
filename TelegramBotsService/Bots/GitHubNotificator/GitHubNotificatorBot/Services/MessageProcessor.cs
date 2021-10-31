using System.Threading.Tasks;
using LoggerCommon;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace GitHubNotificatorBot.Services
{
    /// <summary>
    /// Provides processing of received messages 
    /// </summary>
    public class MessageProcessor : IMessageProcessor
    {
        // Private fields
        private readonly ILogger _logger;
        private readonly ITelegramBotClient _telegramBotClient;
        private long _id;

        /// <summary>
        /// Creates instance of <see cref="MessageProcessor"/>
        /// </summary>
        /// <param name="logger">Provides logging</param>
        /// <param name="telegramBotClient"></param>
        public MessageProcessor(ILogger logger, ITelegramBotClient telegramBotClient)
        {
            _logger = logger;
            _telegramBotClient = telegramBotClient;
        }

        /// <summary>
        /// Processes the received message
        /// </summary>
        /// <param name="text">The received message</param>
        /// <param name="id">The chat id</param>
        /// <returns>Process message</returns>
        public async Task Process(string text, long id)
        {
            _logger.Info($"{nameof(Process)}|start");
            _id = id;

            var message = string.Empty;

            switch (text)
            {
                case "start":
                case "/start":
                    message = "I started thinking, don't distract (-_-)";
                    break;
                default:
                    message = "I don't understand you!";
                    break;
            }

            await _telegramBotClient.SendTextMessageAsync(_id, message);
            _logger.Info($"{nameof(Process)}|{nameof(message)}: {message}");
            await SendButtons();
            await Think();
            await _telegramBotClient.SendPhotoAsync(_id,
                new InputOnlineFile("https://www.ixbt.com/img/n1/news/2019/11/4/dims_large.jpg"), "Хочешь такую? Работай епт.");
            message = "Photo has been sending";
            _logger.Info($"{nameof(Process)}|{nameof(message)}: {message}");
            await Think(10000);
            await _telegramBotClient.SendPhotoAsync(_id,
                new InputOnlineFile(
                    "https://lh3.googleusercontent.com/proxy/rx6HIVZFwMR1ZjqzVEHNj_RqvXXTKFSIAFPv33raQaWxZynp5pqII9atpJEjguaF5h-yhyYL1VnpRasF27dhT1EehK2fBPVawsEeNEqjHKJziircaPevdw3LlZ-__GC3X9H0iXg9L3xgLAEdkvgS6YM"));
        }

        private async Task SendButtons()
        {
            var rkm = new ReplyKeyboardMarkup
            {
                Keyboard = new[]
                {
                    new[] {new KeyboardButton("item"), new KeyboardButton("item")},
                    new[] {new KeyboardButton("item")}
                }
            };
            rkm.ResizeKeyboard = true;
            await _telegramBotClient.SendTextMessageAsync(_id, "Text", ParseMode.Default, false, false, 0, rkm);
        }

        private async Task Think(int delay = 2000)
        {
            await Task.Delay(delay);
            var message = "I finished thinking. What are you want? ( -_-)";
            await _telegramBotClient.SendTextMessageAsync(_id, message);
            
            _logger.Info($"{nameof(Process)}|{nameof(message)}: {message}");
        }
    }
}